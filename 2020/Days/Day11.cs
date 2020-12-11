using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _2020.Days
{
    /// <summary>
    /// Not ideal, but working solution
    /// </summary>
    public class Day11 : DayBase, IDay<long>
    {
        private static readonly List<Point> SearchVectors = new List<Point> { new Point(-1, 0), new Point(-1, -1), new Point(0, -1), new Point(1, -1), new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1) };

        public long Part1(string[] input)
        {
            return OccupiedSeatCount(input);
        }

        private static int OccupiedSeatCount(string[] input)
        {
            var oldState = input.Select(row =>
                string.Join(string.Empty, row.Select(col => col.IsFree() ? CharExtensions.Occupied : col))).ToArray();

            var lastOccupiedSeatCount = 0;
            var occupiedSeatCount = -1;
            while (occupiedSeatCount != lastOccupiedSeatCount)
            {
                occupiedSeatCount = lastOccupiedSeatCount;
                var newState = oldState.Select((row, y) => string.Join(string.Empty, row.Select((col, x) =>
                        {
                            if (col.IsFloor())
                                return col;

                            var count = Count(oldState, y - 1, x - 1);
                            count += new[] { row.ElementAtOrDefault(x - 1), row.ElementAtOrDefault(x + 1) }
                                .Count(c => c.IsOccupied());
                            count += Count(oldState, y + 1, x - 1);


                            if (col.IsFree() && count == 0)
                                return CharExtensions.Occupied;

                            return col.IsOccupied() && count >= 4 ? CharExtensions.Free : col;
                        }
                    ).ToArray())
                ).ToArray();
                oldState = newState;

                lastOccupiedSeatCount = oldState.Select(row => row.Count(c => c.IsOccupied())).Sum();
            }

            return occupiedSeatCount;
        }

        private static int Count(string[] oldState, int y, int x)
        {
            if (y < 0)
                return 0;
            var length = 3;
            if (x < 0)
            {
                length = 2;
                x = 0;
            }

            return (oldState.ElementAtOrDefault(y) ?? string.Empty)
                .Skip(x)
                .Take(length)
                .Count(c => c.IsOccupied());
        }

        public long Part2(string[] input)
        {
            return OccupiedSeatCountPart2(input);
        }

        private static int OccupiedSeatCountPart2(string[] input)
        {
            var map = new Dictionary<Point, char>();
            for (var y = 0; y < input.Length; y++)
            {
                var row = input[y];
                for (var x = 0; x < row.Length; x++)
                {
                    map.Add(new Point(x, y), row[x] == CharExtensions.Free ? CharExtensions.Occupied : row[x]);
                }
            }

            var allPoints = map.Keys;

            var lastOccupiedSeatCount = 0;
            var occupiedSeatCount = -1;
            while (occupiedSeatCount != lastOccupiedSeatCount)
            {
                occupiedSeatCount = lastOccupiedSeatCount;
                var newMap = new Dictionary<Point, char>();
                foreach (var currentPoint in allPoints)
                {
                    var currentSeat = map[currentPoint];
                    if (currentSeat.IsFloor())
                        newMap[currentPoint] = currentSeat;

                    var seatsCount = CountEm(currentPoint, map);

                    if (currentSeat.IsFree() && seatsCount == 0)
                    {
                        newMap[currentPoint] = CharExtensions.Occupied;
                        continue;
                    }

                    newMap[currentPoint] = currentSeat.IsOccupied() && seatsCount >= 5 ? CharExtensions.Free : currentSeat;
                }

                lastOccupiedSeatCount = newMap.Values.Count(x => x.IsOccupied());
                map = newMap;
            }

            return occupiedSeatCount;
        }

        private static int CountEm(Point currentPoint, Dictionary<Point, char> map)
        {
            var seats = new List<char>();
            foreach (var searchVector in SearchVectors)
            {
                char? found = null;
                var point = new Point(currentPoint.X, currentPoint.Y);
                point.Offset(searchVector.X, searchVector.Y);
                while (found == null)
                {
                    if (!map.TryGetValue(point, out var character))
                        break;
                    switch (character)
                    {
                        case CharExtensions.Floor:
                            point.Offset(searchVector.X, searchVector.Y);
                            continue;
                        case CharExtensions.Free:
                        case CharExtensions.Occupied:
                            found = character;
                            break;
                        default:
                            throw new Exception($"What is that? {character}");
                    }
                }
                if (found != null)
                    seats.Add(found.Value);
            }

            return seats.Count(x => x.IsOccupied());
        }
    }

    internal static class CharExtensions
    {
        public const char Occupied = '#';
        public const char Free = 'L';
        public const char Floor = '.';

        internal static bool IsOccupied(this char c)
        {
            return c == Occupied;
        }

        internal static bool IsFloor(this char c)
        {
            return c == Floor;
        }

        internal static bool IsFree(this char c)
        {
            return c == Free;
        }
    }
}