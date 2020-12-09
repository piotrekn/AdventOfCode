using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _2020.Models;

namespace _2020.Days
{
    public class Day7 : DayBase, IDay<int, string[]>
    {
        private const string MyBag = "shiny gold";
        private const string MainRegex = "(.+) bags contain (.+)";
        private const string InnerRegex = @"(?:(\d{1,2}) (.+) bags*)|no other bags";

        public int Part1(string[] input)
        {
            // Parse data
            var rules = ParseData(input);

            // construct relations
            var (_, parents) = RelationTable(rules);

            // calculate answer
            const string myBag = "shiny gold";

            var containers = new HashSet<string>();
            var bags = new Queue<string>(parents.Get(myBag).Children.Keys);
            while (bags.Any())
            {
                var bagName = bags.Dequeue();
                var parentBags = parents.Get(bagName);
                if (parentBags.Children.Any())
                {
                    foreach (var (key, _) in parentBags.Children)
                    {
                        bags.Enqueue(key);
                        containers.Add(key);
                    }
                }

                containers.Add(bagName);
            }

            return containers.Count;
        }
        public int Part2(string[] input)
        {
            // Parse data
            var rules = ParseData(input);

            // construct relations
            var (children, _) = RelationTable(rules);

            // calculate answer
            return RecursiveCalculation(children.Get(MyBag), 1, children) - 1;
        }

        private static int RecursiveCalculation(Relation relation, int multiply, RelationTable table)
        {
            Console.Write($"{multiply}");
            var sum = 0;
            if (!relation.Children.Any()) return multiply;

            Console.Write("*(1+");
            foreach (var (key, value) in relation.Children)
            {
                sum += RecursiveCalculation(table[key], value, table);
            }
            Console.Write(")");

            return multiply * (sum + 1);
        }

        private static (RelationTable children, RelationTable parents) RelationTable(IEnumerable<KeyValuePair<string, (int, string)[]>> rules)
        {
            var children = new RelationTable();
            var parents = new RelationTable();
            foreach (var (key, value) in rules)
            {
                children.AddRelations(key, value);
                foreach (var container in value)
                {
                    parents.AddRelations(container.Item2, new[] { (1, key) });
                }
            }

            return (children, parents);
        }

        private static List<KeyValuePair<string, (int, string)[]>> ParseData(IEnumerable<string> input)
        {
            return input.Select(x =>
            {
                var match = Regex.Match(x, MainRegex);
                var container = match.Groups[1].Value;
                var contains = match.Groups[2].Value
                    .TrimEnd('.')
                    .Split(", ")
                    .Select(s =>
                    {
                        var innerMatch = Regex.Match(s, InnerRegex);
                        if (!innerMatch.Success)
                            throw new Exception("Something went wrong with the parser");

                        return string.IsNullOrEmpty(innerMatch.Groups[1].Value) ? null : innerMatch;
                    })
                    .Where(regex => regex != null)
                    .Select(regex => (
                        int.Parse(regex.Groups[1].Value),
                        regex.Groups[2].Value
                    ))
                    .ToArray();

                return new KeyValuePair<string, (int, string)[]>(container, contains);
            }).ToList();
        }
    }
}
