using System;
using System.Linq;

namespace _2020.Days
{
    public abstract class DayBase
    {
        public virtual string[] ParseFileContent(string fileContent, Func<string, string> formatting = null)
        {
            return ParseFileContent<string>(fileContent, formatting ?? (x => x));
        }

        public virtual T[] ParseFileContent<T>(string fileContent, Func<string, T> formatting)
        {
            var lines = fileContent.Split(Environment.NewLine);
            return lines.Select(formatting).ToArray();
        }
    }
}
