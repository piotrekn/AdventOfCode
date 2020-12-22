using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _2020.Models
{
    public class Tile
    {
        private static readonly List<Point> AdjacentPoints = new List<Point> { new Point(-1, 0), new Point(0, -1), new Point(1, 0), new Point(0, 1) };
        private static readonly Dictionary<Point, Direction> DirectionAdjacentPointsMap =
            new Dictionary<Point, Direction>
            {
                {new Point(-1, 0), Direction.West},
                {new Point(0, -1), Direction.South},
                {new Point(1, 0), Direction.East},
                {new Point(0, 1), Direction.North}
            };

        private readonly string[] _tile;

        public Point Point { get; private set; }

        public string Year { get; }
        public TileBorder[] Borders { get; }
        public List<TileBorder> UnusedBorders { get; }
        public List<(Point, Direction)> AdjacentUnusedPoints { get; private set; }

        public Tile(string year, string[] tile)
        {
            _tile = tile;
            Year = year;
            Borders = GetBorders();
            UnusedBorders = new List<TileBorder>(Borders);
            AdjacentUnusedPoints = new List<(Point, Direction)>();
        }

        public void PlaceTile(Point point)
        {
            Point = point;
            AdjacentUnusedPoints = AdjacentPoints
                .Select(x => (point: new Point(point.X + x.X, point.Y + x.Y), direction: DirectionAdjacentPointsMap[x]))
                .ToList();
        }

        public TileBorder[] GetBorders()
        {
            var directions = new List<Direction> { Direction.North, Direction.South, Direction.East, Direction.West }
                .Select(GetBorder)
                .ToList();
            var reversedDirections = directions
                .Select(x => new TileBorder { Value = string.Join(string.Empty, x.Value.Reverse()), Direction = x.Direction, IsFlipped = true })
                .ToArray();
            directions.AddRange(reversedDirections);

            return directions.ToArray();
        }

        public TileBorder GetBorder(Direction direction)
        {
            return direction switch
            {
                Direction.North => new TileBorder { Direction = direction, Value = _tile[0] },
                Direction.South => new TileBorder { Direction = direction, Value = _tile.Last() },
                Direction.East => new TileBorder { Value = string.Join(string.Empty, _tile.Select(x => x.Last())), Direction = direction },
                Direction.West => new TileBorder { Value = string.Join(string.Empty, _tile.Select(x => x[0])), Direction = direction },
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unknown direction")
            };
        }

        public void MarkBorderAsUsed(string border)
        {
            foreach (var tileBorder in Borders)
            {
                if (tileBorder.Value == border)
                {
                    UnusedBorders.Add(tileBorder);
                }
            }
        }
        public void MarkBorderAsUsed(TileBorder border)
        {
            foreach (var tileBorder in Borders)
            {
                if (tileBorder.Value == border.Value)
                {
                    UnusedBorders.Add(tileBorder);
                }
            }
        }
        public void MarkBorderAsUsed(Direction direction)
        {
            foreach (var tileBorder in Borders)
            {
                if (tileBorder.Direction == direction)
                {
                    UnusedBorders.Add(tileBorder);
                }
            }
        }

        public int FindMatches(IEnumerable<Tile> tiles)
        {
            return tiles.Count(t => t.Year != Year && t.Borders.Any(s => Borders.Any(b => b.Value == s.Value)));
        }
    }

    public struct TileBorder
    {
        public string Value;
        public Direction Direction;
        public bool IsFlipped;
    }
}