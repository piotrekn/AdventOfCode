using _2020.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day4 : IDay<int, string>
    {
        public int Part1(string input)
        {
            var passports = Map(input);

            return passports.Count(x => x.Validate());
        }

        public int Part2(string input)
        {
            var passports = Map(input);
            return passports.Count(x => x.Validate() && x.ValidateFormats());
        }

        private static IEnumerable<Passport> Map(string input)
        {

            var batches = input
                .Split("\r\n\r\n")
                .Select(x => x.Split('\r', '\n', ' '))
                .ToList();

            return batches
                .Select(x => new Passport(x.Where(s => !string.IsNullOrWhiteSpace(s))))
                .ToList();
        }
    }
}
