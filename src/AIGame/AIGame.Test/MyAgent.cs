using AIGame.Game;

namespace AIGame.Test
{
    public class MyAgent : Agent
    {
        public override AgentAction GetAction(GameState gameState)
        {
            return Move(Direction.Up);
        }
    }
}
