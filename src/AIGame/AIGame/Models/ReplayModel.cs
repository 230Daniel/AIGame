using System.Collections.Generic;
using AIGame.Game;

namespace AIGame.Models
{
    internal class ReplayModel
    {
        public GameStateModel InitialGameState { get; set; }
        public List<TurnModel> Turns { get; set; }
        public GameStateModel FinalGameState { get; set; }
    }
}
