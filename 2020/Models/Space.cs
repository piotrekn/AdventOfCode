using System;
using System.Collections.Generic;
using System.Linq;
using _2020.Days;
using _2020.Extensions;

namespace _2020.Models
{
    public class Space
    {
        public Dictionary<Vector, bool> AllCubes { get; set; }

        private readonly HashSet<Vector> _searchVectors3D;
        private readonly HashSet<Vector> _searchVectors;

        public Space(string[] sliceZero)
        {
            var slice = sliceZero
                .Select((line, y) =>
                {
                    return line.Select((sign, x) => new Pocket(sign, new Vector(x, y, 0, 0))).ToArray();
                })
                .ToArray();
            AllCubes = slice.SelectMany(x => x).ToDictionary(x => x.Position, x => x.IsActive);
            _searchVectors3D = GetSearchVectors3D();
            _searchVectors = GetSearchVectors();
        }

        public HashSet<Vector> GetSearchVectors3D()
        {
            var searchVectors = new HashSet<Vector>();
            foreach (var x in Enumerable.Range(-1, 3))
            {
                foreach (var y in Enumerable.Range(-1, 3))
                {
                    foreach (var z in Enumerable.Range(-1, 3))
                    {
                        searchVectors.Add(new Vector(x, y, z, 0));
                    }
                }
            }
            searchVectors.Remove(new Vector(0, 0, 0, 0));
            return searchVectors;
        }

        public HashSet<Vector> GetSearchVectors()
        {
            var searchVectors3d = GetSearchVectors3D();
            var searchVectors = searchVectors3d.Select(x => x.Offset(new Vector(0, 0, 0, -1))).ToHashSet();
            searchVectors.AddRange(searchVectors3d);
            searchVectors.AddRange(searchVectors3d.Select(x => x.Offset(new Vector(0, 0, 0, 1))));
            searchVectors.Add(new Vector(0, 0, 0, 1));
            searchVectors.Add(new Vector(0, 0, 0, -1));

            return searchVectors;
        }

        public long CountEm(Vector currentPoint, out HashSet<Vector> outOfBounds)
        {
            var countActives = 0;
            outOfBounds = new HashSet<Vector>();
            foreach (var searchVector in _searchVectors3D)
            {
                var neighbourPoint = currentPoint.Offset(searchVector);
                if (!AllCubes.TryGetValue(neighbourPoint, out var isCubeActive))
                {
                    outOfBounds.Add(neighbourPoint);
                    continue;
                }

                if (isCubeActive)
                    countActives++;
            }

            return countActives;
        }
        public long CountEm4D(Vector currentPoint, out HashSet<Vector> outOfBounds)
        {
            var countActives = 0;
            outOfBounds = new HashSet<Vector>();
            foreach (var searchVector in _searchVectors)
            {
                var neighbourPoint = currentPoint.Offset(searchVector);
                if (!AllCubes.TryGetValue(neighbourPoint, out var isCubeActive))
                {
                    outOfBounds.Add(neighbourPoint);
                    continue;
                }

                if (isCubeActive)
                    countActives++;
            }

            return countActives;
        }

        public string ToDebugString()
        {
            var orderedSlices = CubeToString().OrderBy(x => x.z).ToArray();
            return string.Join(
                Environment.NewLine + Environment.NewLine,
                orderedSlices.Select(x => $"z={x.z};w={x.z}{Environment.NewLine}{x.slice}")
                );
        }

        public (int z, int w, string slice)[] CubeToString()
        {
            var xes = AllCubes.Select(x => x.Key.X).ToList();
            var yes = AllCubes.Select(x => x.Key.Y).ToList();

            var minX = xes.Min();
            var minY = yes.Min();

            var vectorOffset = new Vector(minX < 0 ? -minX : 0, minY < 0 ? -minY : 0, 0, 0);
            var deltaX = xes.Max() - minX + 1;
            var allCubes = AllCubes.ToDictionary(x => x.Key.Offset(vectorOffset), y => y.Value);

            var cubesOrdered = allCubes
                .GroupBy(x => x.Key.W)
                .SelectMany(x => x.GroupBy(g => g.Key.Z)
                        .Select(g => (
                            z: x.Key,
                            w: g.Key,
                            slice: g.Select(v => new Pocket(v.Value, v.Key)).ToArray()
                        ))
                    )
                .ToArray();

            return cubesOrdered
                .Select(s => (s.z, s.w, slice: ToString(s.slice, deltaX)))
                .ToArray();
        }

        private static string ToString(IEnumerable<Pocket> argSlice, int xMax)
        {
            var map = new List<string>();
            var sliceOrdered = argSlice
                .OrderBy(v => v.Position.Y)
                .ThenBy(v => v.Position.X)
                .ToArray();
            var rowNumber = 0;
            var i = 0;
            var row = new string('.', xMax).ToCharArray();
            while (i < sliceOrdered.Length)
            {
                var pocket = sliceOrdered[i];
                if (rowNumber == pocket.Position.Y)
                {
                    row[pocket.Position.X] = pocket.Sign;
                    i++;
                    continue;
                }

                map.Add(string.Join(string.Empty, row));
                row = new string('.', xMax).ToCharArray();
                rowNumber++;
            }
            map.Add(string.Join(string.Empty, row));

            return string.Join(Environment.NewLine, map);
        }
    }
}