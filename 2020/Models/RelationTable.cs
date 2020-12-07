using System.Collections.Generic;

namespace _2020.Models
{
    public class RelationTable : Dictionary<string, Relation>
    {
        /// <summary>
        /// Like dictionary but with ContainsKey check
        /// </summary>
        /// <returns>returns value if exists, and null of either value is null or don't exists</returns>
        public RelationTable AddRelations(string id, (int, string)[] ruleValues)
        {
            var entity = Get(id);
            foreach (var (count, color) in ruleValues)
            {
                entity.Children.Add(color, count);
            }

            return this;
        }

        /// <summary>
        /// Like dictionary but with ContainsKey check
        /// </summary>
        /// <returns>returns value if exists, and null of either value is null or don't exists</returns>
        public Relation Get(string key)
        {
            if (ContainsKey(key))
                return this[key];

            var newValue = new Relation(key);
            Add(key, newValue);
            return newValue;
        }
    }
}