namespace ConsoleGame
{
    public class Vector2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2D(int x = 0, int y = 0)
        {
            Set(x, y);
        }

        public bool Equals(int x, int y) => (x == X && y == Y);

        public void Set(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}