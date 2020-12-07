using System.Collections.Generic;

namespace _2020.Models
{
    public class Relation
    {
        public string Color { get; set; }
        public Dictionary<string, int> Parents { get; set; }
        public Dictionary<string, int> Children { get; set; }

        public Relation(string color)
        {
            Color = color;
            Children = new Dictionary<string, int>();
            Parents = new Dictionary<string, int>();
        }
    }
}