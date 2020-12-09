using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day9Tests : BaseDayTests<Day9>
    {
        public Day9Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day9())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day9_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string preamble, string _, string fileContent)
        {
            var parameter = int.Parse(preamble);
            var result = Day.Part1(Day.ParseFileContent(fileContent, long.Parse), parameter);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day9_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string preamble, string expectedResult, string fileContent)
        {
            var parameter = int.Parse(preamble);
            var result = Day.Part2(Day.ParseFileContent(fileContent, long.Parse), parameter);
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
