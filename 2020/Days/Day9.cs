using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Days
{
    public class Day9 : DayBase, IDayWithParameter<long, long[], int>
    {
        public long Part1(long[] input, int preambleCount)
        {
            var preambleDictionary = new HashSet<long>(input.Take(preambleCount));
            var preamble = new Queue<long>(input.Take(preambleCount));
            var numbers = new Queue<long>(input.Skip(preambleCount).ToArray());
            while (numbers.Count > 0)
            {
                var currentNumber = numbers.Dequeue();

                if (preamble.All(x => !preambleDictionary.Contains(currentNumber - x)))
                {
                    return currentNumber;
                }

                // move preamble
                preambleDictionary.Remove(preamble.Dequeue());
                preambleDictionary.Add(currentNumber);
                preamble.Enqueue(currentNumber);
            }

            throw new Exception("Cannot find the result");
        }

        public long Part2(long[] input, int preambleCount)
        {
            var theInvalidNumber = Part1(input, preambleCount);
            var startIndex = 0;
            var currentIndex = 0;

            long sum = 0;
            while (currentIndex < input.Length)
            {
                sum += input[currentIndex];
                while (sum > theInvalidNumber)
                    sum -= input[startIndex++];
                if (sum == theInvalidNumber && currentIndex - startIndex > 0)
                    break;

                currentIndex++;
            }

            if (currentIndex - startIndex <= 0)
                throw new Exception("Cannot find the result");

            var contiguousSet = input.Skip(startIndex).Take(currentIndex - startIndex + 1).ToArray();
            return contiguousSet.Min() + contiguousSet.Max();
        }
    }
}
