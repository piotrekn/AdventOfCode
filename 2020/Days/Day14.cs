using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day14 : DayBase, IDay<long, string>
    {
        public long Part1(string input)
        {
            var lines = ParseFileContent(input);
            var finalDictionary = GoOver(lines);
            return finalDictionary.Sum(x => x.Value);
        }

        private static Dictionary<long, long> GoOver(string[] lines, bool isPartTwo = false)
        {
            var mask = string.Empty;
            var finalDictionary = new Dictionary<long, long>();
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split('=')[1].Trim();
                    continue;
                }

                var match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                var memValue = long.Parse(match.Groups[1].Value);
                var value = long.Parse(match.Groups[2].Value);

                if (isPartTwo)
                {
                    var valueString = Convert.ToString(memValue, 2).PadLeft(36, '0');
                    var maskedValues = GetMaskedPart2(mask, valueString);
                    foreach (var maskedValue in maskedValues)
                    {
                        finalDictionary[Convert.ToInt64(maskedValue, 2)] = value;
                    }
                }
                else
                {
                    var valueString = Convert.ToString(value, 2).PadLeft(36, '0');
                    finalDictionary[memValue] = Convert.ToInt64(GetMasked(mask, valueString), 2);
                }
            }

            return finalDictionary;
        }
        private static string GetMasked(string mask, string stringValue)
        {
            var newValue = string.Join(string.Empty, mask.Select((x, i) =>
            {
                if (x == 'X')
                {
                    return stringValue.Length > i ? stringValue[i] : '0';
                }

                return x;
            }));
            return newValue;
        }

        public long Part2(string input)
        {
            var lines = ParseFileContent(input);
            var finalDictionary = GoOver(lines, true);
            return finalDictionary.Sum(x => x.Value);
        }

        private static HashSet<string> GetMaskedPart2(string mask, string stringValue, string partialValue = null)
        {
            var list = new HashSet<string>();
            var builder = new StringBuilder(partialValue);
            for (var i = partialValue?.Length ?? 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case 'X':
                        builder.Append('1');
                        list.AddRange(GetMaskedPart2(mask, stringValue, builder.ToString()));

                        builder[i] = '0';
                        list.AddRange(GetMaskedPart2(mask, stringValue, builder.ToString()));
                        return list;
                    case '1':
                        builder.Append('1');
                        break;
                    case '0':
                        builder.Append(stringValue[i]);
                        break;
                }
            }

            return new HashSet<string> { builder.ToString() };
        }

    }

    public static class HashSetExtension
    {
        public static HashSet<T> AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                hashSet.Add(element);
            }

            return hashSet;
        }
    }
}