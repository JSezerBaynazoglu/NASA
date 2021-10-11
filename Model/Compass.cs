using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsStation.Model
{
    class Compass
    {
        internal IList<Direction> compassDirections;
        public Compass()
        {
            Direction east = new Direction("E");
            Direction north = new Direction("N");
            Direction west = new Direction("W");
            Direction south = new Direction("S");

            compassDirections = new List<Direction>();
            east.Right = south;
            north.Right = east;
            west.Right = north;
            south.Right = west;
            
            east.Left = north;
            north.Left = west;
            west.Left = south;
            south.Left = east;
            compassDirections.Add(east);
            compassDirections.Add(north);
            compassDirections.Add(west);
            compassDirections.Add(south);
        }
        public Direction  GetDirection(string key)
        {
            return compassDirections.FirstOrDefault(x=>x.DirectionKey.Equals(key));
        }

    }
}
