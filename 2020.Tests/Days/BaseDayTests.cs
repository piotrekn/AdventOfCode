using System;
using Xunit.Abstractions;

namespace _2020.Tests.Days
{
    public abstract class BaseDayTests<T>
    {
        protected readonly ITestOutputHelper TestOutputHelper;
        protected readonly T Day;

        protected BaseDayTests(ITestOutputHelper testOutputHelper, T day)
        {
            TestOutputHelper = testOutputHelper;
            Day = day;
        }

        protected static string[] ParseFileContent(string fileContent)
        {
            return fileContent.Split(Environment.NewLine);
        }
    }
}
