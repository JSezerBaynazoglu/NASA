using System.Collections.Generic;
namespace MarsStation.Model
{
    public class Shape
    {
        readonly int height;
        readonly int weight;
        private readonly IList<Robot> robotList;
        public Shape(int h, int w)
        {
            this.height = h;
            this.weight = w;
            this.robotList = new List<Robot>();
        }
        public IList<Robot> RobotList { get => robotList;}
        public void AddRobot(Robot r)
        {
            this.RobotList.Add(r);
        }
    }
}