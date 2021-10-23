using System;

namespace AIGame.Game
{
    internal class DummyAgent : Agent
    {
        public override AgentAction GetAction(GameState gameState)
        {
            throw new InvalidOperationException("The dummy agent can not be used to get an action.");
        }
    }
}
