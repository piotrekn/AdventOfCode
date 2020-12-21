using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day21 : DayBase
    {
        public long Part1(string input)
        {
            var candidates = Solve(input).Where(x => !x.allergens.Any()).ToList();

            return candidates.Sum(x => x.recepiesCount.Value);
        }

        private List<(string ingredient, List<string> allergens, ValueHolder<int> recepiesCount)> Solve(string input)
        {
            var lines = ParseFileContent(input);
            var recipes = lines.Select(x =>
            {
                var rex = Regex.Match(x, @"^(?'ingredients'[^(]+) \(contains (?'allergens'[^)]+)\)$");
                return (allergens: rex.Groups["allergens"].Value.Split(", "),
                    ingredients: rex.Groups["ingredients"].Value.Split(' '));
            })
                .ToList();

            var allIngredients = new HashSet<string>();
            var allAllergens = new HashSet<string>();
            foreach (var (allergens, ingredients) in recipes)
            {
                allIngredients.AddRange(ingredients);
                allAllergens.AddRange(allergens);
            }

            var ingredientDict = allIngredients
                .Select(x => (ingredient: x, allergens: allAllergens.ToList(), recepiesCount: new ValueHolder<int>(0)))
                .ToList();

            foreach (var recipe in recipes)
            {
                foreach (var ingredientEntry in ingredientDict)
                {
                    if (recipe.ingredients.Contains(ingredientEntry.ingredient))
                    {
                        ingredientEntry.recepiesCount.Value++;
                        continue;
                    }

                    foreach (var recipeAllergen in recipe.allergens)
                        ingredientEntry.allergens.Remove(recipeAllergen);
                }
            }

            return ingredientDict;
        }

        public string Part2(string input)
        {
            var ingredientDict = Solve(input).Where(x => x.allergens.Any()).ToList();

            var queue = new Queue<(string ingredient, List<string> allergens)>();
            ingredientDict.ForEach(x => queue.Enqueue((x.ingredient, x.allergens)));
            var known = new Dictionary<string, string>();
            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                if (element.allergens.Count == 1)
                {
                    known.Add(element.allergens.First(), element.ingredient);
                    continue;
                }

                queue.Enqueue(element);
                for (var index = 0; index < element.allergens.Count; index++)
                {
                    var allergen = element.allergens[index];
                    if (!known.ContainsKey(allergen)) continue;
                    element.allergens.Remove(allergen);
                    index--;
                }
            }

            var ingredientsInOrderOfAllergens = known
                .OrderBy(x => x.Key) // allergen
                .Select(x => x.Value); //ingredient

            return string.Join(',', ingredientsInOrderOfAllergens);
        }
    }

    internal class ValueHolder<T>
    {
        public T Value { get; set; }

        public ValueHolder(T value)
        {
            Value = value;
        }
    }
}