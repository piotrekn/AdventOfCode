using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day5Tests : BaseDayTests<Day5>
    {
        public Day5Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day5())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day5_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(ParseFileContent(fileContent));

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day5_TestCasePack.txt", "mydata")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(ParseFileContent(fileContent));

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
