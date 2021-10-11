namespace MarsStation.Model
{
    public class Point
    {
        int x;
        int y;
        public Point(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

    }
}