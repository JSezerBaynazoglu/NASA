namespace MarsStation.Model
{
    public  class MoveManager
    {
        readonly IMoveCommand _M;
        readonly Mover @obj;
        public MoveManager(ref IMovable point)
        {
            _M = new MoveCommand(point);
            @obj = new Mover(_M);
        }
        public void MoveTo(Direction direction)
        {
            @obj.Move(direction);
        
        }


    }
}