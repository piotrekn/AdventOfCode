using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace _2020.Tests
{
    /// <summary>
    /// Attribute for bulk reading test cases from file
    /// </summary>
    public class BulkFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly string _testName;

        /// <summary>
        /// Load data from a flat file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        public BulkFileDataAttribute(string filePath)
            : this(filePath, null) { }

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        /// <param name="testName">The name of the property on the JSON file that contains the data for the test</param>
        public BulkFileDataAttribute(string filePath, string testName)
        {
            _filePath = filePath;
            _testName = testName;
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }

            // Load the file
            var fileContent = File.ReadAllText(_filePath);

            //if (string.IsNullOrEmpty(_testName))
            //{
            //    //whole file is the data
            //    return new[] { new[] { fileContent } };
            //}

            var match = Regex.Matches(fileContent, "@==([^=]+)===(?:(?:\r\n)|(?:\n))([^@]*)");

            var testCases = match.Select(ParseParameters);

            if (string.IsNullOrEmpty(_testName))
            {
                //whole file is the data
                return testCases;
            }

            return new[] { testCases.FirstOrDefault(x => x.FirstOrDefault() == _testName) };
        }

        private static string[] ParseParameters(Match match)
        {
            var parameters = match.Groups.Values.ElementAt(1).Value.Split('|').ToList();
            var testCase = match.Groups.Values.ElementAt(2).Value.TrimEnd();

            parameters.Add(testCase);
            return parameters.ToArray();
        }
    }
}
