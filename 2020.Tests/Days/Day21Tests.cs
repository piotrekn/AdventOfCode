using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day21Tests : BaseDayTests<Day21>
    {
        public Day21Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day21())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day21_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day21_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(fileContent);
            Assert.True(expectedResult == result, $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
