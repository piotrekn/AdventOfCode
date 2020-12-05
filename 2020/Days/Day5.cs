using System;
using _2020.Models;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day5 : IDay<int, string[]>
    {
        public int Part1(string[] input)
        {
            var seats = input.Select(x =>
            {
                var binary = x
                    .Replace('F', '0')
                    .Replace('L', '0')
                    .Replace('R', '1')
                    .Replace('B', '1');

                var row = Convert.ToInt32(binary.Substring(0, 7), 2);
                var column = Convert.ToInt32(binary.Substring(7, 3), 2);


                return new
                {
                    row,
                    column,
                    seatId = row * 8 + column
                };
            })
                .ToList();

            return seats.Max(x => x.seatId);
        }

        public int Part2(string[] input)
        {
            var passports = input.Select(x =>
            {
                var binary = x
                    .Replace('F', '0')
                    .Replace('L', '0')
                    .Replace('R', '1')
                    .Replace('B', '1');

                var row = Convert.ToInt32(binary.Substring(0, 7), 2);
                var column = Convert.ToInt32(binary.Substring(7, 3), 2);


                return new
                {
                    row,
                    column,
                    seatId = row * 8 + column
                };
            })
                .ToList();

            var orderedSeatIds = passports.Select(x => x.seatId).OrderBy(x => x).ToList();
            var mySeatId = orderedSeatIds.FirstOrDefault();

            return orderedSeatIds.Skip(1).First(x => x != ++mySeatId) - 1;
        }
    }
}
