using System;
using System.Collections.Generic;
using _2020.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Days
{
    public class Day19 : DayBase, IDay<long, string>
    {
        private const int MagicNumber = 5;
        public long Part1(string input)
        {
            var (rules, words) = ParseInputData(input);

            var knownRulesList = Do(rules);
            var ruleExpression = knownRulesList["0"].RegexCompacted;
            var wordsMatch = words.Where(x => Regex.IsMatch(x, ruleExpression)).ToList();

            return wordsMatch.Count;
        }

        private (List<Rule> rules, List<string> words) ParseInputData(string input)
        {
            var lines = ParseFileContent(input);
            var index = 0;
            var rules = new List<Rule>();
            for (; index < lines.Length; index++)
            {
                var line = lines[index];
                if (string.IsNullOrWhiteSpace(line))
                    break;

                var rule = line.Split(": ");
                rules.Add(new Rule(rule[0], string.Join(string.Empty, rule[1].ToCharArray().Where(x => x != '"'))));
            }

            var words = new List<string>();
            for (; index < lines.Length; index++)
            {
                var line = lines[index];

                words.Add(line);
            }

            return (rules, words);
        }

        private static (Dictionary<string, Rule> knownRulesList, Dictionary<string, Rule> notResolvedList) GetDicts(List<Rule> rules2)
        {
            var knownRulesList = rules2.Where(x => !x.UnknownRules.Any()).ToDictionary(x => x.RuleId, x => x);
            var notResolvedList = rules2.Where(x => x.UnknownRules.Any()).ToDictionary(x => x.RuleId, x => x);
            return (knownRulesList, notResolvedList);
        }


        public long Part2(string input)
        {
            var (rules, words) = ParseInputData(input);
            var replaces = new Dictionary<string, string>
            {
                {"8","42 | 42 8"},
                {"11","42 31 | 42 11 31"}
            };
            foreach (var rule in rules)
            {
                if (replaces.ContainsKey(rule.RuleId))
                    rule.SetExpression(replaces[rule.RuleId]);
                if (!replaces.Any())
                    break;
            }

            var knownRulesList = Do(rules);

            var ruleExpression = knownRulesList["0"].RegexCompacted;
            var wordsMatch = words.Where(x => Regex.IsMatch(x, ruleExpression)).ToList();

            return wordsMatch.Count;
        }

        private static Dictionary<string, Rule> Do(List<Rule> rules)
        {
            var (knownRulesList, notResolvedList) = GetDicts(rules);
            var lastKnownRulesListCount = knownRulesList.Count;
            var loopTimes = 0;
            while (!knownRulesList.ContainsKey("0"))
            {
                foreach (var notResolvedRule in notResolvedList.Values)
                {
                    foreach (var unknownRuleId in notResolvedRule.UnknownRules)
                    {
                        if (!knownRulesList.ContainsKey(unknownRuleId))
                            continue;
                        var knownRule = knownRulesList[unknownRuleId];
                        notResolvedRule.ReplaceExpression(unknownRuleId, knownRule.ExpressionCompacted);
                    }

                    (knownRulesList, notResolvedList) = GetDicts(rules);
                }

                if (lastKnownRulesListCount == knownRulesList.Count)
                {
                    loopTimes++;
                }

                if (loopTimes >= 5)
                {
                    // deal with loops
                    foreach (var notResolvedRule in notResolvedList.Values.OrderByDescending(x => x.RuleId))
                    {
                        foreach (var unknownRuleId in notResolvedRule.UnknownRules)
                        {
                            if (notResolvedRule.RuleId == "0")
                            {
                                if (!knownRulesList.ContainsKey(unknownRuleId))
                                    continue;
                                var knownRule = knownRulesList[unknownRuleId];
                                notResolvedRule.ReplaceExpression(unknownRuleId, knownRule.ExpressionCompacted);
                                continue;
                            }

                            if (unknownRuleId != notResolvedRule.RuleId)
                                throw new Exception("It is not a loop! Process corrupted");


                            for (var i = 0; i < MagicNumber; i++)
                            {
                                var expression = Regex.Replace(notResolvedRule.ExpressionCompacted, notResolvedRule.RuleId,
                                    $"(?:{notResolvedRule.ExpressionCompacted})");
                                notResolvedRule.SetExpression(expression);
                            }

                            var rid = Regex.Replace(notResolvedRule.ExpressionCompacted, notResolvedRule.RuleId,
                                string.Empty);
                            notResolvedRule.SetExpression(rid);
                        }

                        (knownRulesList, _) = GetDicts(rules);
                    }

                    break;
                }

                lastKnownRulesListCount = knownRulesList.Count;
            }

            return knownRulesList;
        }
    }
}