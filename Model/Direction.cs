namespace MarsStation.Model
{
    public class Direction
    {
        public string DirectionKey { get; set; }
        internal Direction Left { get; set; }
        internal Direction Right { get; set; }
        public Direction(string key)
        {
            DirectionKey = key;
        }

    }
}