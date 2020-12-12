using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;

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
        private Point _waypoint;

        private Point _currentPosition;
        public Point CurrentPosition => _currentPosition;
        public Point CurrentVector { get; private set; }

        public static Point North => new Point(0, 1);
        public static Point South => new Point(0, -1);
        public static Point East => new Point(1, 0);
        public static Point West => new Point(-1, 0);

        public Ship()
        {
            CurrentVector = East;
            _currentPosition = new Point(0, 0);
        }

        public Ship(Point waypoint) : this()
        {
            _waypoint = waypoint;
        }

        public void Move(string step)
        {
            var offset = int.Parse(step.Substring(1, step.Length - 1));
            switch (step[0])
            {
                case 'N':
                    if (_waypoint.IsEmpty)
                        _currentPosition.Offset(North.X * offset, North.Y * offset);
                    else
                        _waypoint.Offset(0, offset);
                    break;
                case 'S':
                    if (_waypoint.IsEmpty)
                        _currentPosition.Offset(South.X * offset, South.Y * offset);
                    else
                        _waypoint.Offset(0, -offset);
                    break;
                case 'W':
                    if (_waypoint.IsEmpty)
                        _currentPosition.Offset(West.X * offset, West.Y * offset);
                    else
                        _waypoint.Offset(-offset, 0);
                    break;
                case 'E':
                    if (_waypoint.IsEmpty)
                        _currentPosition.Offset(East.X * offset, East.Y * offset);
                    else
                        _waypoint.Offset(offset, 0);
                    break;
                case 'R':
                    if (_waypoint.IsEmpty)
                        CurrentVector = CurrentVector.Rotate(-offset);
                    else
                        _waypoint = _waypoint.Rotate(-offset);
                    break;
                case 'L':
                    if (_waypoint.IsEmpty)
                        CurrentVector = CurrentVector.Rotate(offset);
                    else
                        _waypoint = _waypoint.Rotate(offset);
                    break;
                case 'F':
                    if (_waypoint.IsEmpty)
                        _currentPosition.Offset(CurrentVector.X * offset, CurrentVector.Y * offset);
                    else
                        _currentPosition.Offset(_waypoint.X * offset, _waypoint.Y * offset);
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