using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day14Tests : BaseDayTests<Day14>
    {
        public Day14Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day14())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day14_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day14_TestCasePack.txt","example2")]
        [BulkFileData("TestCases/Day14_TestCasePack.txt", "puzzleinput")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(fileContent);
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
