using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Models
{

    [DebuggerDisplay("{RuleId}: {Expression} {UnknownRules.Count}")]
    public class Rule
    {
        public string RuleId { get; }
        public string Expression { get; set; }
        public string ExpressionCompacted { get; set; }
        public string RegexCompacted { get; set; }
        public List<string> ExpressionParsed { get; set; }
        public List<string> UnknownRules { get; private set; }

        public Rule(string ruleId, string expression)
        {
            RuleId = ruleId;
            SetExpression(expression);
        }

        public void SetExpression(string value)
        {
            Expression = value;
            ExpressionParsed = value.Split(' ').ToList();
            ExpressionCompacted = string.Join(string.Empty, ExpressionParsed);
            RegexCompacted = $"^{ExpressionCompacted}$";
            UnknownRules = GetUnknownRules();
        }

        public void ReplaceExpression(string from, string to)
        {
            if (to.Contains('|'))
                to = $"(?:{to})";

            for (var i = 0; i < ExpressionParsed.Count; i++)
            {
                if (ExpressionParsed[i] == from)
                    ExpressionParsed[i] = to;
            }

            Expression = string.Join(" ", ExpressionParsed);
            ExpressionCompacted = string.Join(string.Empty, ExpressionParsed);
            RegexCompacted = $"^{ExpressionCompacted}$";
            UnknownRules = GetUnknownRules();
        }

        private List<string> GetUnknownRules()
        {
            var matches = Regex.Matches(Expression, @"\d+");
            return matches.Select(x => x.Value).ToList();
        }
    }
}
