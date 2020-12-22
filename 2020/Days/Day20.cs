using _2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day20 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var tiles = ParseInputData(input);

            var corners = tiles.Where(t => t.FindMatches(tiles) == 2).ToList();

            return corners.Aggregate(1L, (i, v) => i * int.Parse(v.Year));
        }

        public long Part2(string input)
        {
            throw new NotImplementedException("I ain't doing it");
        }

        private List<Tile> ParseInputData(string input)
        {
            var regexParse = Regex.Matches(input, @"Tile (\d+):([^T]+)");
            var tiles = regexParse
                .Select(x => new Tile(x.Groups[1].Value, ParseFileContent(x.Groups[2].Value).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray()))
                .ToList();

            return tiles;
        }
    }
}