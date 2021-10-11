namespace MarsStation.Model
{
    public interface ITurnCommand
    { 
    void Execute();
    }
    public interface ITurnable
    {
        void TurnRight();
        void TurnLeft();
    }
    public class Turner
    {
        readonly ITurnCommand _lCommnad;
        readonly ITurnCommand _rCommand;
        public Turner(ITurnCommand r , ITurnCommand l)
        {
            _rCommand = r;
            _lCommnad = l;
        }
        public void Right()
        {
            _rCommand.Execute();
        }
        public void Left()
        {
            _lCommnad.Execute();
        }
    }
    public class RobotDireciton : ITurnable
    {
        readonly Compass compass;
        internal protected Direction myDirection;
        public  Direction Direction { get => myDirection; }
        public RobotDireciton(string firstDirection)
        {
            compass = new Compass();
            this.myDirection = compass.GetDirection(firstDirection);
        }
        public void TurnLeft()
        {
            myDirection = myDirection.Left;
        }
        public void TurnRight()
        {
            myDirection = myDirection.Right;
        }
    }
    public class RightCommand : ITurnCommand
    {
        private readonly ITurnable _obj;
        public RightCommand(ITurnable obj)
        {
            _obj = obj;
        }
        public void Execute()
        {
            _obj.TurnRight();
        }
    }
    public class LeftCommand : ITurnCommand
    {
        private readonly ITurnable _obj;
        public LeftCommand(ITurnable obj)
        {
            _obj = obj;
        }
        public void Execute()
        {
            _obj.TurnLeft();
        }
    }
}
