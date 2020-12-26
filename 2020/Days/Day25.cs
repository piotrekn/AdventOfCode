using System;

namespace _2020.Days
{
    public class Day25 : DayBase, IDay<long, string>
    {
        private const long CardReminderNumber = 20201227;

        public long Part1(string input)
        {
            var publicKeys = ParseFileContent(input, long.Parse);
            var cardPublicKey = publicKeys[0];
            var doorPublicKey = publicKeys[1];

            return FindLopSize(doorPublicKey, cardPublicKey).cardKey;
        }

        public (long loopSize, long cardKey) FindLopSize(long doorPublicKey, long cardPublicKey)
        {
            var cardKey = 1L;
            var current = 1L;
            var loopSize = 0;
            while (current != doorPublicKey)
            {
                current = (current * 7) % CardReminderNumber;
                cardKey = (cardKey * cardPublicKey) % CardReminderNumber;
                loopSize++;
            }
            return (loopSize, cardKey);
        }

        public long Part2(string input)
        {
            throw new NotImplementedException("I ain't doing it... yet. Need more stars");
        }
    }
}