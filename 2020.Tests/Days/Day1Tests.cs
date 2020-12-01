using _2020.Days;
using System;
using Xunit;

namespace _2020.Tests
{
    public class Day1Tests
    {
        [Theory]
        [FileData("TestCases/Day1_TestCase1.txt")]
        public void Part1(string lines)
        {
            var day1 = new Day1();
            Console.WriteLine($"answer for part 1 is: {day1.FixMyExpanse(lines.Split(Environment.NewLine))}");
        }

        [Theory]
        [FileData("TestCases/Day1_TestCase1.txt")]
        public void Part2(string lines)
        {
            var day1 = new Day1();
            var result = day1.FixMyExpanseWithTreeNumbers(lines.Split(Environment.NewLine));
            Assert.NotEqual(0, result);
            Assert.True(result > 989824, "is not greater than 989824");
            Assert.True(true, $"answer for day 1 part 2 is: {result}");
        }
    }
}
