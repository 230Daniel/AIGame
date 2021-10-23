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
        private string _record;

        public ReplayRecorderService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void RecordInitialGameState(GameState gameState)
        {
            var replay = new ReplayModel
            {
                InitialGameState = _mapper.Map<GameStateModel>(gameState),
                Turns = new()
            };
            _record = JsonSerializer.Serialize(replay);
        }

        public void RecordTurn(Turn turn)
        {
            var replay = JsonSerializer.Deserialize<ReplayModel>(_record);
            replay.Turns.Add(_mapper.Map<TurnModel>(turn));
            _record = JsonSerializer.Serialize(replay);
        }

        public void RecordFinalGameState(GameState gameState)
        {
            var replay = JsonSerializer.Deserialize<ReplayModel>(_record);
            replay.FinalGameState = _mapper.Map<GameStateModel>(gameState);
            _record = JsonSerializer.Serialize(replay);
        }

        public void Save()
        {
            var replay = JsonSerializer.Deserialize<ReplayModel>(_record);
            _record = JsonSerializer.Serialize(replay, new JsonSerializerOptions{WriteIndented = true});
            Console.WriteLine(_record);
        }
    }
}
