namespace AIGame.Game
{
    public abstract class AgentAction
    {
        public Agent Agent { get; }

        protected AgentAction(Agent agent)
        {
            Agent = agent;
        }
        
        internal abstract void Execute(GameState gameState);
    }
}
