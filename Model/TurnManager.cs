namespace MarsStation.Model
{
    public class TurnManager
    {
        readonly ITurnCommand _R;
        readonly ITurnCommand _L;
        readonly Turner @obj;
        public TurnManager(ref ITurnable direction)
        {
            _R = new RightCommand(direction);
            _L = new LeftCommand(direction);
            @obj = new Turner(_R, _L);
        }
        public void  Turn(string directing)
        {
            if (directing.Equals("R"))
                @obj.Right();
            if (directing.Equals("L"))
                @obj.Left();
            else 
            {
                //artabilir
            }
        }
    }
}