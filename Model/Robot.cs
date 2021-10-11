namespace MarsStation.Model
{
    interface IRobot
    {
        public string GetInfo();
        public void DoSomeThings(string command);

        
        
    }
    public class Robot :IRobot
    {
        private readonly IMovable point;
        private readonly MoveManager mover;

        private readonly ITurnable direction;
        private readonly TurnManager turner;

        public Robot(int firstX, int firstY, string firstDirection)
        {
            direction = new RobotDireciton(firstDirection);
            turner = new TurnManager(ref direction);
            point= new RobotPoint(firstX,firstY);
            mover = new MoveManager(ref point);
        }

        internal Compass Compass { get; } = new Compass();

        public void DoSomeThings(string command)
        {
            switch (command)
            {
                case "R":
                case "L":
                    turner.Turn(command);
                    break;
                case "M":
                    mover.MoveTo(((RobotDireciton)this.direction).Direction);
                    break;
            }
            
        }
        public string GetInfo()
        {
         return ((RobotPoint)this.point).Point.X.ToString() + " " +
                ((RobotPoint)this.point).Point.Y.ToString() + " " +
                ((RobotDireciton)this.direction).Direction.DirectionKey;
        }
    }
}
