using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day16 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var lines = ParseFileContent(input);

            return Do(lines, true);
        }

        private static long Do(string[] lines, bool part1)
        {
            var validNumbers = new List<(string, HashSet<int>)>();
            var yourTicket = new List<int>();

            string line;
            var i = 0;
            // field definitions
            while (i < lines.Length)
            {
                if (GetNotEmptyLine(i, lines, out line))
                {
                    i++;
                    continue;
                }

                if (line.StartsWith("your ticket:"))
                    break;

                var value = Regex.Match(line, @"([^:]+): (\d+-\d+) or (\d+-\d+)");
                validNumbers
                    .Add(
                        (
                            value.Groups[1].Value,
                            new HashSet<int>()
                                .AddIntRange(value.Groups[2].Value)
                                .AddIntRange(value.Groups[3].Value)
                        )
                    );
                i++;
            }

            i++;
            while (i < lines.Length)
            {
                if (GetNotEmptyLine(i, lines, out line))
                {
                    i++;
                    continue;
                }

                if (line.StartsWith("nearby tickets:"))
                    break;

                yourTicket = lines[i].Split(',').Select(int.Parse).ToList();
                i++;
            }


            i++;

            var tickets = new List<List<int>>();
            while (i < lines.Length)
            {
                if (GetNotEmptyLine(i, lines, out line))
                {
                    i++;
                    continue;
                }

                tickets.Add(line.Split(',').Select(int.Parse).ToList());

                i++;
            }

            if (part1)
            {
                var allValidNumbers = validNumbers.SelectMany(x => x.Item2).ToHashSet();
                var invalidNumbers = tickets
                    .SelectMany(x => x)
                    .Where(x => !allValidNumbers.Contains(x))
                    .Select(Convert.ToInt64)
                    .ToList();
                return invalidNumbers.Sum();
            }

            var validTickets = tickets
                .Where(x => x.All(n => validNumbers.Any(v => v.Item2.Contains(n))))
                .ToList();

            var ordered = new List<List<int>>();
            for (var index = 0; index < validNumbers.Count; index++)
            {
                var (_, hashSet) = validNumbers[index];
                var match = validTickets
                    .Select((x, numberIndex) => (number: x[index], numberIndex))
                    .Where(x => hashSet.Contains(x.number))
                    .Select(x => x.numberIndex)
                    .ToList();

                ordered.Add(match);
            }

            foreach (var orderedElement in ordered.OrderBy(x => x.Count))
            {
                if (!orderedElement.Any())
                    continue;
                var firstIndex = orderedElement.FirstOrDefault();
                ordered.ForEach(x =>
                {
                    if (x != orderedElement)
                        x.Remove(firstIndex);
                });
            }

            var fieldsOrder = ordered
                .Where(x => x.Any())
                .Select(x => x.First())
                .Where(x => validNumbers[x].Item1.StartsWith("departure"))
                .Select(x => yourTicket[x])
                .Select(Convert.ToInt64)
                .ToList();

            return fieldsOrder.Any()
                ? fieldsOrder.Aggregate((a, b) => a * b)
                : 0;
        }

        private static bool GetNotEmptyLine(int i, string[] lines, out string line)
        {
            line = lines[i];
            return string.IsNullOrWhiteSpace(line);
        }

        public long Part2(string input)
        {
            var lines = ParseFileContent(input);
            return Do(lines, false);
        }
    }

    internal static class Day16Extensions
    {
        public static HashSet<int> AddIntRange(this HashSet<int> target, string rangeString)
        {
            var numbers = rangeString.Split('-').Select(int.Parse).ToList();
            target.AddRange(Enumerable.Range(numbers[0], numbers[1] - numbers[0] + 1));
            return target;
        }
    }
}