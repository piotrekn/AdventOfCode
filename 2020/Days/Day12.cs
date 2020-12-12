using System;
using System.Drawing;
using _2020.Models;

namespace _2020.Days
{
    public class Day12 : DayBase, IDay<long>
    {
        public long Part1(string[] input)
        {
            var ship = new Ship();
            foreach (var step in input)
            {
                ship.Move(step);
            }

            return Math.Abs(ship.CurrentPosition.X) + Math.Abs(ship.CurrentPosition.Y);
        }

        public long Part2(string[] input)
        {
            var ship = new Ship(new Point(10, 1));
            foreach (var step in input)
            {
                ship.Move(step);
            }

            return Math.Abs(ship.CurrentPosition.X) + Math.Abs(ship.CurrentPosition.Y);
        }
    }
}