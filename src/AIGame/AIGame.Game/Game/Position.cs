namespace AIGame.Game
{
    public class Position
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Offset(Direction direction, int offset)
        {
            return direction switch
            {
                Direction.Up => new(X, Y + offset),
                Direction.Down => new(X, Y - offset),
                Direction.Left => new(X - offset, Y),
                Direction.Right => new(X + offset, Y),
                _ => new(X, Y)
            };
        }
    }
}
