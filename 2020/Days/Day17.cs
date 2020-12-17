using System.Collections.Generic;
using System.Linq;
using _2020.Models;

namespace _2020.Days
{
    public class Day17 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var space = new Space(ParseFileContent(input));
            Do(space);

            return space.AllCubes.Values.Count(x => x);
        }

        public long Part2(string input)
        {
            var space = new Space(ParseFileContent(input));
            Do(space, true);

            return space.AllCubes.Values.Count(x => x);
        }

        private static List<string> Do(Space cube, bool part2 = false)
        {
            var history = new List<string>();

            var cycleCount = 1;
            while (cycleCount <= 6)
            {
#if DEBUG
                history.Add(cube.ToDebugString());
#endif

                var newMap = new Dictionary<Vector, bool>();
                var boundaryPoints = new HashSet<Vector>();
                foreach (var currentPoint in cube.AllCubes.Keys)
                {
                    var isCubeActive = cube.AllCubes[currentPoint];
                    HashSet<Vector> newPoints;
                    var activeCount = part2
                        ? cube.CountEm4D(currentPoint, out newPoints)
                        : cube.CountEm(currentPoint, out newPoints);
                    boundaryPoints.AddRange(newPoints);

                    if (isCubeActive)
                        newMap[currentPoint] = activeCount == 2 || activeCount == 3;
                    else
                        newMap[currentPoint] = activeCount == 3;
                }

                foreach (var currentPoint in boundaryPoints)
                {
                    var activeCount = part2
                        ? cube.CountEm4D(currentPoint, out _)
                        : cube.CountEm(currentPoint, out _);
                    if (activeCount == 3)
                        newMap[currentPoint] = true;
                }


                cube.AllCubes = newMap;
                cycleCount++;

            }

            return history;
        }
    }
}