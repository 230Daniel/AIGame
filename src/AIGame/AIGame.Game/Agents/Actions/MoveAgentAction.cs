using System;

namespace AIGame.Game
{
    public sealed class MoveAgentAction : AgentAction
    {
        public Direction Direction { get; }

        public MoveAgentAction(Agent agent, Direction direction) : base(agent)
        {
            Direction = direction;
        }

        internal override void Execute(GameState gameState)
        {
            switch (Direction)
            {
                case Direction.Up:
                    Agent.Position.Y++;
                    break;
                case Direction.Down:
                    Agent.Position.Y--;
                    break;
                case Direction.Left:
                    Agent.Position.X--;
                    break;
                case Direction.Right:
                    Agent.Position.X++;
                    break;
            }
        }
    }
}
