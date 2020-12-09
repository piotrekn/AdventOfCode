using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day7Tests : BaseDayTests<Day7>
    {
        public Day7Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day7())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day7_TestCasePack.txt", "example1")]
        [BulkFileData("TestCases/Day7_TestCasePack.txt", "puzzleinput")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(Day.ParseFileContent(fileContent));

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day7_TestCasePack.txt", "example2")]
        [BulkFileData("TestCases/Day7_TestCasePack.txt", "puzzleinput")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(Day.ParseFileContent(fileContent));
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
