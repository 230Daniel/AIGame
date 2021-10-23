using System;
using System.Collections.Generic;
using System.Linq;

namespace AIGame.Game
{
    public class GameState
    {
        public int TurnNumber { get; private set; }
        public List<Agent> Agents { get; }

        public GameState()
        {
            Agents = new(){ new DefaultAgent { Id = 0, Position = new(0, 0) } };
        }

        internal void TakeTurn(Turn turn)
        {
            foreach (var agentAction in turn.AgentActions)
            {
                agentAction.Execute(this);
            }

            TurnNumber++;
        }
    }
}
