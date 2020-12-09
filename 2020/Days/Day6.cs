using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day6 : DayBase, IDay<int, string>
    {
        public int Part1(string input)
        {
            var batches = Map(input);

            var counts = batches
                .Select(x =>
                {
                    var set = new HashSet<char>(x);
                    set.RemoveWhere(char.IsWhiteSpace);
                    return set.Count;
                });

            return counts.Sum();
        }

        public int Part2(string input)
        {
            var batches = Map(input);

            var counts = batches
                .Select(b =>
                {
                    var peopleCount = b.Split("\r\n").Length;

                    return ToDictionary(b).Values.Count((x) => x == peopleCount);

                }).ToList();

            return counts.Sum();
        }

        private static IEnumerable<string> Map(string input)
        {
            return input.Split("\r\n\r\n");
        }

        private static Dictionary<char, int> ToDictionary(string input)
        {
            var result = new Dictionary<char, int>();
            foreach (var character in input)
            {
                if (result.ContainsKey(character))
                    result[character] += 1;
                else
                    result[character] = 1;
            }

            return result;
        }
    }
}
