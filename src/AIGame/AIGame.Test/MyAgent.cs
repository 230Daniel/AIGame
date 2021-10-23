using AIGame.Game;

namespace AIGame.Test
{
    public class MyAgent : Agent
    {
        public override AgentAction GetAction(GameState gameState)
        {
            return gameState.TurnNumber % 2 == 0 ? Move(Direction.Up) : Move(Direction.Right);
        }
    }
}
