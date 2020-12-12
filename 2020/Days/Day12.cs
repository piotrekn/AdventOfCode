using System;
using System.Drawing;

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

    public class Ship
    {
        private readonly bool _hasNoWaypoint;
        private Point _currentVector;
        private Point _currentPosition;
        public Point CurrentPosition => _currentPosition;

        public static Point North => new Point(0, 1);
        public static Point South => new Point(0, -1);
        public static Point East => new Point(1, 0);
        public static Point West => new Point(-1, 0);

        public Ship()
        {
            _hasNoWaypoint = true;
            _currentVector = East;
            _currentPosition = new Point(0, 0);
        }

        public Ship(Point waypoint) : this()
        {
            _hasNoWaypoint = false;
            _currentVector = waypoint;
        }

        public void Move(string step)
        {
            var offset = int.Parse(step[1..]);
            switch (step[0])
            {
                case 'N':
                    if (_hasNoWaypoint)
                        _currentPosition.Offset(North.X * offset, North.Y * offset);
                    else
                        _currentVector.Offset(0, offset);
                    break;
                case 'S':
                    if (_hasNoWaypoint)
                        _currentPosition.Offset(South.X * offset, South.Y * offset);
                    else
                        _currentVector.Offset(0, -offset);
                    break;
                case 'W':
                    if (_hasNoWaypoint)
                        _currentPosition.Offset(West.X * offset, West.Y * offset);
                    else
                        _currentVector.Offset(-offset, 0);
                    break;
                case 'E':
                    if (_hasNoWaypoint)
                        _currentPosition.Offset(East.X * offset, East.Y * offset);
                    else
                        _currentVector.Offset(offset, 0);
                    break;
                case 'R':
                    _currentVector = _currentVector.Rotate(-offset);
                    break;
                case 'L':
                    _currentVector = _currentVector.Rotate(offset);
                    break;
                case 'F':
                    _currentPosition.Offset(_currentVector.X * offset, _currentVector.Y * offset);
                    break;
            }
        }

    }

    public static class PointExtensions
    {
        private const double DegToRad = Math.PI / 180;

        public static Point Rotate(this Point v, int degrees)
        {
            return v.RotateRadians(degrees * DegToRad);
        }

        public static Point RotateRadians(this Point v, double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new Point(Convert.ToInt32(ca * v.X - sa * v.Y), Convert.ToInt32(sa * v.X + ca * v.Y));
        }
    }
}