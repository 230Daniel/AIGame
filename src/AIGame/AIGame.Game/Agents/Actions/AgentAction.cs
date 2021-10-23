namespace AIGame.Game
{
    public abstract class AgentAction
    {
        public int AgentId { get; }
        
        protected AgentAction(int agentId)
        {
            AgentId = agentId;
        }

        internal abstract void Execute(GameState gameState);
    }
}
