using _2020.Models;
using System.Linq;
using _2020.Models.Day18;

namespace _2020.Days
{
    public class Day18 : DayBase, IDay<decimal, string>
    {
        public decimal Part1(string input)
        {
            var lines = ParseFileContent(input, (line) => new Expression(line));
            var values = lines.Select(x => x.CalculateStrat1()).ToList();

            return values.Sum();
        }

        public decimal Part2(string input)
        {
            var lines = ParseFileContent(input, (line) => new Expression(line));
            var values = lines.Select(x => x.CalculateStrategy2()).ToList();

            return values.Sum();
        }
    }
}