using _2020.Days;
using _2020.Tests.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day24Tests : BaseDayTests<Day24>
    {
        public Day24Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day24())
        {
        }

        [Theory]
        [BulkFileData("TestCases/Day24_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [TheoryRunnablenDebugOnly]
        [BulkFileData("TestCases/Day24_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(fileContent);
            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
