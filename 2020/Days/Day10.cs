using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day10 : DayBase, IDay<long, int[]>
    {
        public long Part1(int[] input)
        {
            var differenceOfJolts = new ConcurrentDictionary<int, int>();
            var orderedJolts = input.OrderBy(x => x).ToList();
            orderedJolts.Add(orderedJolts.Max() + 3);
            var lastJolt = 0;
            foreach (var jolt in orderedJolts)
            {
                var difference = jolt - lastJolt;
                differenceOfJolts.AddOrUpdate(difference, x => 1, (key, value) => value + 1);
                lastJolt = jolt;
            }

            return differenceOfJolts[1] * differenceOfJolts[3];
        }

        public long Part2(int[] input)
        {
            var orderedJolts = input
                .OrderBy(x => x)
                .ToList();
            orderedJolts.Insert(0, 0);
            orderedJolts.Add(orderedJolts.Last() + 3);

            var cache = new Dictionary<int, long> { { orderedJolts.Count - 2, 1 } };

            return CountPermutations(orderedJolts, cache);
        }

        private static long CountPermutations(IReadOnlyList<int> orderedJolts, IDictionary<int, long> cache, int i = 0)
        {
            var permutationsCount = 0L;
            var startPoint = i;

            if (cache.ContainsKey(startPoint))
                return cache[startPoint];

            for (var n = 1; n <= 3; n++)
            {
                if ((startPoint + n < orderedJolts.Count) && (orderedJolts[startPoint + n] - orderedJolts[startPoint] <= 3))
                    permutationsCount += CountPermutations(orderedJolts, cache, startPoint + n); ;
            }

            if (!cache.ContainsKey(startPoint))
                cache[startPoint] = permutationsCount;

            return permutationsCount;
        }
    }
}