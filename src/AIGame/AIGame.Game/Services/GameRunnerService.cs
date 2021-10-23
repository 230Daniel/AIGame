using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AIGame.Game
{
    internal class GameRunnerService : BackgroundService
    {
        private readonly ILogger<GameRunnerService> _logger;
        private readonly ITurnGeneratorService _turnGeneratorService;
        private readonly IReplayRecorderService _replayRecorderService;

        public GameRunnerService(ILogger<GameRunnerService> logger, ITurnGeneratorService turnGeneratorService, IReplayRecorderService replayRecorderService = null)
        {
            _logger = logger;
            _turnGeneratorService = turnGeneratorService;
            _replayRecorderService = replayRecorderService;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Run(RunGame, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Exception thrown while running the game");
            }
        }

        public GameState RunGame()
        {
            var gameState = new GameState();
            _replayRecorderService?.RecordInitialGameState(gameState);
            
            for (var i = 0; i < 10; i++)
            {
                var turn = _turnGeneratorService.GetTurn(gameState);
                gameState.TakeTurn(turn);
                _replayRecorderService?.RecordTurn(turn);
            }

            _replayRecorderService?.RecordFinalGameState(gameState);
            _replayRecorderService?.Save();
            return gameState;
        }
    }
}
