using _2020.Models.Day23;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day23 : DayBase, IDay<string, string>
    {
        public string Part1(string input)
        {
            var numbers = new LinkedList<int>(input.ToCharArray().Select(x => int.Parse(x.ToString())));
            var cups = new Cups(numbers);
            PlayIt(cups, 100);

            return string.Join("", cups.OrderFromCupOne().Select(x => x.Value));
        }

        private static void PlayIt(Cups cups, int loopLimit)
        {
            var maxValue = cups.Map.Values.Select(x => x.Value).Max();
            var currentNode = cups.First;
            for (var i = 1; i <= loopLimit; i++)
            {
                var pickupStart = currentNode.Next;
                var pickupEnd = pickupStart.Next.Next;
                var handValues = new HashSet<int>
                {
                    currentNode.Next.Value,
                    currentNode.Next.Next.Value,
                    currentNode.Next.Next.Next.Value
                };

                var nextLabel = currentNode.Value;
                do
                {
                    nextLabel--;
                    nextLabel = nextLabel == 0 ? maxValue : nextLabel;
                }
                while (handValues.Contains(nextLabel));

                var destination = cups.Map[nextLabel];
                MoveCups(currentNode, destination, pickupStart, pickupEnd);

                currentNode = currentNode.Next;
            }
        }

        private static void MoveCups(Cup currentNode, Cup destination, Cup pickupStart, Cup pickupEnd)
        {
            // 123456
            currentNode.Next = pickupEnd.Next;      // 1xxx56

            var tempNext = destination.Next;
            destination.Next = pickupStart;
            pickupEnd.Next = tempNext;              // 152346
        }

        public string Part2(string input)
        {
            var inputNumbers = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

            var allNumbers = inputNumbers.Union(Enumerable.Range(inputNumbers.Max() + 1, 1000000 - inputNumbers.Max()));
            var numbers = new LinkedList<int>(allNumbers);
            var cups = new Cups(numbers);

            PlayIt(cups, 10000000);

            var part2Order = cups.OrderFromCupOne().Take(2).Select(x => Convert.ToInt64(x.Value)).ToList();

            return part2Order.Aggregate((a, b) => a * b).ToString();
        }
    }
}