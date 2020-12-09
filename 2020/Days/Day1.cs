using System;
using System.Linq;

namespace _2020.Days
{
    public class Day1 : DayBase, IDay<long>
    {
        public long Part1(string[] input)
        {
            var numbers = input
                .Where(x => x.Length <= 4)
                .Select(int.Parse)
                .ToDictionary(x => x, x => x);

            var theNumbers = numbers.Keys.Where(x => numbers.ContainsKey(2020 - x)).ToList();
            if (theNumbers.Count != 2)
                throw new Exception($"Found {theNumbers.Count} matching numbers found");

            return theNumbers[0] * theNumbers[1];
        }

        public long Part2(string[] input)
        {
            var numbers = input
                .Where(x => x.Length <= 4)
                .Select(int.Parse)
                .ToDictionary(x => x, x => x);

            // brute fore it!
            foreach (var i in numbers.Keys)
            {
                var _2020_minus_i = 2020 - i;
                foreach (var i2 in numbers.Keys)
                {
                    var _2020_minus_i_minus_i2 = _2020_minus_i - i2;
                    if (_2020_minus_i_minus_i2 < 0)
                        continue;

                    if (numbers.ContainsKey(_2020_minus_i_minus_i2))
                        return i * i2 * numbers[_2020_minus_i_minus_i2];
                }
            }
            return 0;
        }
    }
}
