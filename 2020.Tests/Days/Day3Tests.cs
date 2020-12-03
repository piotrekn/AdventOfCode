using _2020.Days;
using System;
using System.Drawing;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day3Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day3Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [FileData("TestCases/Day3_TestCase1.txt")]
        public void Part1(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();
            var part1 = day.Part1(lines);

            _testOutputHelper.WriteLine($"answer for part 1 is: {part1}");
        }

        [Theory]
        [FileData("TestCases/Day3_TestCase2.txt")]
        public void Part1_case2(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();
            var part1 = day.Part1(lines);

            Assert.Equal(1, part1);

            _testOutputHelper.WriteLine($"answer for part 1 is: {part1}");
        }

        [Theory]
        [FileData("TestCases/Day3_TestCase1.txt")]
        public void Part2(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();
            var part2 = day.Part2(lines);

            Assert.True(part2 > 1566542571, $"{part2} is too low");

            _testOutputHelper.WriteLine($"answer for part 2 is: {part2}");
        }

        [Theory]
        [FileData("TestCases/Day3_TestCase3.txt")]
        public void Part1_case3(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();
            var part1 = day.Part1(lines);

            Assert.Equal(7, part1);
        }

        [Theory]
        [FileData("TestCases/Day3_TestCase3.txt")]
        public void Part2_TC3(string fileContent)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();
            var part2 = day.Part2(lines);

            Assert.Equal(336, part2);
        }

        [Theory]
        [InlineData(".......\r\n...#...\r\n......#", new[] { 0, 0, 3, 1 })]
        [InlineData(".......\r\n...#...\r\n......#", new[] { 3, 1, 6, 2 })]
        [InlineData("...\r\n#..", new[] { 0, 0, 0, 1 })]
        [InlineData(".......\r\n...X...\r\n......X", new[] { 0, 0, 3, 1 }, false)]
        [InlineData(".......\r\n...X...\r\n......X", new[] { 3, 1, 6, 2 }, false)]
        [InlineData("...\r\nX..", new[] { 0, 0, 0, 1 }, false)]
        public void Move_Works(string fileContent, int[] coordinates, bool isTree = true)
        {
            var lines = fileContent.Split(Environment.NewLine);
            var day = new Day3();

            var current = new Point(coordinates[0], coordinates[1]);
            var next = new Point(coordinates[2], coordinates[3]);
            var moveResult = day.MoveCount(lines, current, new Point(3, 1));

            Assert.Equal(next, moveResult.position);
            Assert.Equal(isTree, moveResult.count == 1);
        }
    }
}
