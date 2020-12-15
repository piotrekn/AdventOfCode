using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day15 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var numbers = input.Split(',').Select(int.Parse).ToList();

            return Play(numbers, 2020);
        }

        private static long Play(IReadOnlyCollection<int> numbers, int iterations)
        {
            var dict = numbers
                .Select((x, i) => (key: x, value: i + 1))
                .ToDictionary(x => x.key, x => (lastIndex: x.value, lastBeforeIndex: (int?)null));


            var lastNumberSpoken = numbers.Last();
            for (var i = numbers.Count + 1; i <= iterations; i++)
            {
                var (lastIndex, lastBeforeIndex) = dict[lastNumberSpoken];
                var newNumberSpoken = lastBeforeIndex == null ? 0 : lastIndex - lastBeforeIndex ?? 0;

                dict[newNumberSpoken] = dict.ContainsKey(newNumberSpoken)
                    ? dict[newNumberSpoken] = (i, dict[newNumberSpoken].lastIndex)
                    : dict[newNumberSpoken] = (i, null);

                lastNumberSpoken = newNumberSpoken;
            }

            return lastNumberSpoken;
        }

        public long Part2(string input)
        {
            var numbers = input.Split(',').Select(int.Parse).ToList();

            return Play(numbers, 30000000);
        }
    }
}