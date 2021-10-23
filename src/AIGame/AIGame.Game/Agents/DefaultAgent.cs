using System;

namespace AIGame.Game
{
    public sealed class DefaultAgent : Agent
    {
        public override AgentAction GetAction(GameState gameState)
        {
            throw new InvalidOperationException("This agent can not generate actions and is used for internal purposes only.");
        }
    }
}
