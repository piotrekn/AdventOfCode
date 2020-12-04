using _2020.Days;
using Xunit;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public class Day4Tests : BaseDayTests<Day4>
    {
        public Day4Tests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, new Day4())
        {
        }

        [Theory]
        [FileData("TestCases/Day4_TestCase1.txt")]
        public void Part1(string fileContent)
        {
            var result = Day.Part1(fileContent);

            TestOutputHelper.WriteLine($"answer for part 1 is: {result}");
        }

        [Theory]
        [FileData("TestCases/Day4_TestCase1.txt")]
        public void Part2(string fileContent)
        {
            var result = Day.Part2(fileContent);

            Assert.Equal(194, result);

            TestOutputHelper.WriteLine($"answer for part 2 is: {result}");
        }

        [Theory]
        [FileData("TestCases/Day4_Example.txt")]
        public void Part1Example(string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.Equal(2, result);

            TestOutputHelper.WriteLine($"answer for part 1 is: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day4_TestCasePack.txt")]
        public void Part1_pack(string testCaseName, string expectedResult, string _, string fileContent)
        {
            var result = Day.Part1(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }

        [Theory]
        [BulkFileData("TestCases/Day4_TestCasePack.txt")]
        public void Part2_Pack(string testCaseName, string _, string expectedResult, string fileContent)
        {
            var result = Day.Part2(fileContent);

            Assert.True(expectedResult == result.ToString(), $"\"{testCaseName}\" test case expected: {expectedResult}, actual: {result}");
        }
    }
}
