namespace MarsStation.Model
{
    public interface IMoveCommand
    {
        void Execute(Direction direction);
    }
    public class Mover
    {
        internal readonly IMoveCommand _cmnd;
        public Mover(IMoveCommand command)
        {
            _cmnd = command;   
        }
        public void Move(Direction direction)
        {
            _cmnd.Execute(direction);
        }
    }
    public interface IMovable
    {
        public void Move(Direction direction);
    }
    public class RobotPoint : IMovable
    {
        internal protected Point myPoint;
        public Point Point { get => myPoint;}
        public RobotPoint(int x , int  y)
        {
            myPoint = new Point(x,y);
        }

        public void Move(Direction direction)
        {
            switch(direction.DirectionKey)
            {
                case "E":
                    myPoint.X += 1;
                    break;
                case "W":
                    myPoint.X -= 1;
                    break;
                case "N":
                    myPoint.Y += 1;
                    break;
                case "S":
                    myPoint.Y -= 1;
                    break;
            }
        }
    }
    public class MoveCommand : IMoveCommand
    {
        private readonly IMovable _obj;
        public MoveCommand(IMovable obj)
        {
            _obj = obj;
        }
        public void Execute(Direction direction)
        {
            _obj.Move(direction);
        }
    }
}