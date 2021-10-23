using System;
using System.Collections.Generic;
using System.Text.Json;
using AIGame.Game;
using AIGame.Models;
using AutoMapper;

namespace AIGame.Services
{
    internal class ReplayRecorderService : IReplayRecorderService
    {
        private readonly IMapper _mapper;
        
        private GameStateModel _initialGameState;
        private List<TurnModel> _turns;
        private GameStateModel _finalGameState;

        public ReplayRecorderService(IMapper mapper)
        {
            _mapper = mapper;
            _turns = new();
        }

        public void RecordInitialGameState(GameState gameState)
        {
            _initialGameState = _mapper.Map<GameStateModel>(gameState);
        }

        public void RecordTurn(Turn turn)
        {
            _turns.Add(_mapper.Map<TurnModel>(turn));
        }

        public void RecordFinalGameState(GameState gameState)
        {
            _finalGameState = _mapper.Map<GameStateModel>(gameState);
        }

        public void Save()
        {
            var replay = new ReplayModel
            {
                InitialGameState = _initialGameState,
                Turns = _turns.ToArray(),
                FinalGameState = _finalGameState
            };

            var json = JsonSerializer.Serialize(replay, new JsonSerializerOptions {WriteIndented = true});
            Console.Write(json);
        }
    }
}
