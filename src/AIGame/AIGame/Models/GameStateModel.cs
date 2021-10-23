using System.Collections.Generic;

namespace AIGame.Models
{
    internal class GameStateModel
    {
        public int TurnNumber { get; set; }
        public List<AgentModel> Agents { get; set; }
    }
}
