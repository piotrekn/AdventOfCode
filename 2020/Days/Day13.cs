using System;
using System.Linq;

namespace _2020.Days
{
    public class Day13 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var lines = ParseFileContent(input);
            var timestamp = int.Parse(lines[0]);
            var busIds = lines[1]
                .Split(',')
                .Where(x => x != "x")
                .Select(int.Parse);

            var times = busIds.ToArray();
            var timeWindow = times.Select(x =>
            {
                var time = x;
                while (time < timestamp)
                {
                    time += x;
                }

                return (x, time);
            });

            var myBus = timeWindow.OrderBy(x => x.time).First();
            return myBus.x * (myBus.time - timestamp);
        }

        public long Part2(string input)
        {
            var lines = ParseFileContent(input);
            var busIds = lines[1]
                .Split(',')
                .Select((x, i) => (busId: x, offset: i))
                .Where(x => x.busId != "x")
                .Select(x => (busId: long.Parse(x.busId), x.offset));

            var orderedBusIds = busIds
                .OrderByDescending(t => t.busId)
                .ToList();

            var firstBus = orderedBusIds.First();
            var timestamp = firstBus.busId - firstBus.offset;
            var period = firstBus.busId;
            for (var busIndex = 1; busIndex <= orderedBusIds.Count; busIndex++)
            {
                while (orderedBusIds.Take(busIndex).Any(t => (timestamp + t.offset) % t.busId != 0))
                {
                    timestamp += period;
                }

                period = orderedBusIds
                    .Take(busIndex)
                    .Select(t => t.busId)
                    .Aggregate(Lcm);
            }

            return timestamp;
        }

        // copy paste
        private static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }

        // copy paste
        private static long Gcd(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            // Pull out remainders.
            for (; ; )
            {
                var remainder = a % b;
                if (remainder == 0) return b;
                b = remainder;
            };
        }
    }
}