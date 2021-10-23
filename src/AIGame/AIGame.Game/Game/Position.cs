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
    }
}
