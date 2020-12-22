using _2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day22 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var game = ParseInputData(input);

            var p1 = game[0];
            var p2 = game[1];
            var winner = p1;
            while (p1.cards.Any() && p2.cards.Any())
            {
                var p1Card = p1.cards.Dequeue();
                var p2Card = p2.cards.Dequeue();
                if (p1Card == p2Card)
                    throw new NotImplementedException("I new it!");

                if (p1Card > p2Card)
                {
                    winner = p1;
                    winner.cards.Enqueue(p1Card);
                    winner.cards.Enqueue(p2Card);
                }
                else
                {
                    winner = p2;
                    winner.cards.Enqueue(p2Card);
                    winner.cards.Enqueue(p1Card);
                }
            }

            return winner.cards.Reverse().Select((x, i) => (i + 1) * x).Sum();
        }

        public long Part2(string input)
        {
            throw new NotImplementedException("I ain't doing it");
        }

        private List<(string player, Queue<int> cards)> ParseInputData(string input)
        {
            var regexParse = Regex.Matches(input, @"(?'player'Player \d):(?'cards'[^P]+)");
            return regexParse
                .Select(x => (player: x.Groups["player"].Value, cards: ParseFileContent(x.Groups["cards"].Value).Where(s => !string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToList()))
                .Select(x =>
                {
                    var cardDeck = new Queue<int>();
                    x.cards.ForEach(x => cardDeck.Enqueue(x));
                    return (x.player, cards: cardDeck);
                })
                .ToList();

        }
    }
}