using System;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day8 : IDay<int, string[]>
    {
        public int Part1(string[] input)
        {
            var (accumulatorValue, _) = GetAccumulatorValue(Map(input));

            return accumulatorValue;
        }

        public int Part2(string[] input)
        {
            var lines = Map(input);

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];
                var old = line.Instruction;
                line.Instruction = old switch
                {
                    "jmp" => "nop",
                    "nop" => "jmp",
                    _ => old
                };
                // try the input
                var (accumulatorValue, loopDetected) = GetAccumulatorValue(lines);
                if (!loopDetected)
                    return accumulatorValue;
                line.Instruction = old;
            }

            throw new Exception("Cannot fix the data");
        }

        private static (int accumulatorValue, bool loopDetected) GetAccumulatorValue(IReadOnlyList<Line> lines)
        {
            var accumulatorValue = 0;
            var isLoopDetected = false;

            var loopDetector = new HashSet<int>();
            var i = 0;
            while (i < lines.Count)
            {
                if (loopDetector.Contains(i))
                {
                    isLoopDetected = true;
                    break;
                }

                loopDetector.Add(i);
                var line = lines[i];
                switch (line.Instruction)
                {
                    case "acc":
                        accumulatorValue += line.Value;
                        i++;
                        break;
                    case "jmp":
                        i += line.Value;
                        break;
                    case "nop":
                        i++;
                        break;
                    default: throw new Exception($"Unknown instruction- {line.Instruction}");
                }

            }

            return (accumulatorValue, isLoopDetected);
        }

        private static Line[] Map(string[] input)
        {
            return input.Select(x =>
            {
                var lineSplit = x.Split(' ');
                var instruction = lineSplit[0];
                var value = int.Parse(lineSplit[1]);

                return new Line(instruction, value);
            }).ToArray();
        }

        private class Line
        {
            public string Instruction { get; set; }
            public int Value { get; }

            public Line(string instruction, int value)
            {
                Instruction = instruction;
                Value = value;
            }
        }
    }
}
