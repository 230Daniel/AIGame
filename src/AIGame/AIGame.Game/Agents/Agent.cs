namespace AIGame.Game
{
    public abstract class Agent
    {
        public int Id { get; internal set; }
        public Position Position { get; internal set; }

        public abstract AgentAction GetAction(GameState gameState);

        protected MovingAgentAction Move(Direction direction) => new(Id, direction);
    }
}
