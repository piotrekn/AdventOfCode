using _2020.Days;
using _2020.Models;
using _2020.Models.Day18;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day18Tests : BaseDayTests<Day18>
    {
        public Day18Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day18())
        {
        }

        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 71L)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51L)]
        [InlineData("2 * 3 + (4 * 5)", 26L)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437L)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240L)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632L)]
        public void Part1_Calculates_AsExpectedResult(string testCase, long expectedResult)
        {
            var expression = new Expression(testCase);
            var result = expression.CalculateStrat1();
            Assert.True(result == expectedResult, $"test case expected: {expectedResult}, actual: {result}");
        }
        [Theory]
        [InlineData("1 + 2 * 3 + 4 * 5 + 6", 231L)]
        [InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51L)]
        [InlineData("2 * 3 + (4 * 5)", 46L)]
        [InlineData("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445L)]
        [InlineData("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060L)]
        [InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340L)]
        public void Part2_Calculates_AsExpectedResult(string testCase, long expectedResult)
        {
            var expression = new Expression(testCase);
            var result = expression.CalculateStrategy2();
            Assert.True(result == expectedResult, $"test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day18_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day18_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(fileContent);
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
