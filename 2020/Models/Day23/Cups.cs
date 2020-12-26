using System.Collections.Generic;
using System.Linq;

namespace _2020.Models.Day23
{
    public class Cups
    {
        public Cup First { get; }
        public Cup CupOne { get; }
        public Dictionary<int, Cup> Map { get; }

        public Cups(IEnumerable<int> list)
        {
            Map = new Dictionary<int, Cup>();
            First = new Cup { Value = list.First() };
            Map.Add(First.Value, First);

            var current = First;
            foreach (var label in list.Skip(1))
            {
                var cup = new Cup { Value = label };
                Map.Add(cup.Value, cup);

                if (cup.Value == 1) CupOne = cup;
                current.Next = cup;
                current = cup;
            }

            current.Next = First;
        }

        public IEnumerable<Cup> OrderFromCupOne()
        {
            var current = CupOne.Next;

            do
            {
                yield return current;
                current = current.Next;
            } while (current != CupOne);
        }
    }
}