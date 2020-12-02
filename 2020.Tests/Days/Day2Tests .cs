using _2020.Days;
using System;
using Xunit;

namespace _2020.Tests
{
    public class Day2Tests
    {
        [Theory]
        [FileData("TestCases/Day2_TestCase1.txt")]
        public void Part1(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day2();
            var part1 = day.Part1(lines);

            Console.WriteLine($"answer for part 1 is: {part1}");
        }

        [Theory]
        [FileData("TestCases/Day2_TestCase1.txt")]
        public void Part2(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day2();
            var part1 = day.Part2(lines);

            Console.WriteLine($"answer for part 2 is: {part1}");
        }
    }
}
