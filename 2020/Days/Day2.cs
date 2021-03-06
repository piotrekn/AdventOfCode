﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day2 : DayBase, IDay<long>
    {

        public long Part1(string[] input)
        {
            return PartBody(input, ValidationLogicPartOne);
        }

        public long Part2(string[] input)
        {
            return PartBody(input, ValidationLogicPartTwo);
        }

        private static int PartBody(IEnumerable<string> input, Func<Match, bool> validationLogic)
        {
            var regex = new Regex(@"(\d\d*)-(\d\d*) (.): (.+)");
            var validPasswords = input
                .Select(x => regex.Match(x))
                .Where(validationLogic)
                .Select(x => x.Groups[4].Value);

            return validPasswords.Count();
        }

        private static bool ValidationLogicPartOne(Match match)
        {
            var (min, max, letter, password) = ParseLine(match);

            var occurrences = password.Count(x => x == letter);
            return occurrences >= min && occurrences <= max;
        }

        private static bool ValidationLogicPartTwo(Match match)
        {
            var (min, max, letter, password) = ParseLine(match);

            var occurrences = Convert.ToInt32(password.ElementAtOrDefault(min - 1) == letter) + Convert.ToInt32(password.ElementAtOrDefault(max - 1) == letter);
            return occurrences == 1;
        }

        private static (int min, int max, char letter, string password) ParseLine(Match match)
        {
            var min = int.Parse(match.Groups[1].Value);
            var max = int.Parse(match.Groups[2].Value);
            var letter = match.Groups[3].Value.FirstOrDefault();
            var password = match.Groups[4].Value;

            return (min, max, letter, password);
        }
    }
}
