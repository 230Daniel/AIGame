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

        public GameRunnerService(ILogger<GameRunnerService> logger, ITurnGeneratorService turnGeneratorService)
        {
            _logger = logger;
            _turnGeneratorService = turnGeneratorService;
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

        private void RunGame()
        {
            var gameState = new GameState();

            for (var i = 0; i < 100; i++)
            {
                var turn = _turnGeneratorService.GetTurn(gameState);
                gameState.TakeTurn(turn);
                _logger.LogInformation($"The agent is at {gameState.Agents[0].Position.X}, {gameState.Agents[0].Position.Y}");
            }
        }
    }
}
