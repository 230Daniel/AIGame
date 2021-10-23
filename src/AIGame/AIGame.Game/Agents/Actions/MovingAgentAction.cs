using System.Linq;

namespace AIGame.Game
{
    public class MovingAgentAction : AgentAction
    {
        public Direction Direction { get; }
        public Position NewPosition { get; protected set; }

        internal MovingAgentAction(int agentId, Direction direction) : base(agentId)
        {
            Direction = direction;
        }

        internal override void Execute(GameState gameState)
        {
            var agent = gameState.Agents.First(x => x.Id == AgentId);
            agent.Position = agent.Position.Offset(Direction, 1);
            NewPosition = agent.Position;
        }
    }
}
