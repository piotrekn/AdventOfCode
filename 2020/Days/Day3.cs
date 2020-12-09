using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _2020.Days
{
    public class Day3 : DayBase, IDay<long>
    {
        private const char TreeChar = '#';

        public long Part1(string[] input)
        {
            var slopePoint = new Point(3, 1);
            return TreesCount(input, slopePoint);
        }

        private int TreesCount(string[] input, Point slopePoint)
        {
            var treesCount = 0;
            var points = new HashSet<Point>();
            var startPoint = new Point(0, 0);

            while (startPoint.Y < input.Length - 1)
            {
                var (position, count) = MoveCount(input, startPoint, slopePoint);

                startPoint = position;
                treesCount += count;
                if (!points.Add(startPoint))
                    throw new Exception($"Loop detected! on {startPoint}");
            }

            return treesCount;
        }

        public long Part2(string[] input)
        {
            var slopes = new (int x, int y)[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };


            var slopePoints = slopes.Select(startPoint => new Point(startPoint.x, startPoint.y)).ToList();
            var treeCount = slopePoints.Select(slopePoint => TreesCount(input, slopePoint)).ToList();
            return treeCount.Aggregate(1, (result, x) => result * x);
        }

        public (Point position, int count) MoveCount(string[] input, Point currentPosition, Point slopePoint)
        {
            var newPoint = new Point((currentPosition.X + slopePoint.X) % input[0].Length, currentPosition.Y + slopePoint.Y);
            return (newPoint, input[newPoint.Y][newPoint.X] == TreeChar ? 1 : 0);
        }
    }
}
