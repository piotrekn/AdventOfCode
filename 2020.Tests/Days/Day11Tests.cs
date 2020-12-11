using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day11Tests : BaseDayTests<Day11>
    {
        public Day11Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day11())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day11_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(Day.ParseFileContent(fileContent));

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day11_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(Day.ParseFileContent(fileContent));
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
