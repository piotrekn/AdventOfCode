using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Models.Day18
{
    [DebuggerDisplay("{ToString()}")]
    public class Node
    {
        public NodeType Type { get; }
        public decimal Value { get; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(NodeType type)
        {
            Type = type;
        }
        public Node(NodeType type, decimal value) : this(type)
        {
            Type = type;
            Value = value;
        }

        public void SetPrevious(Node node)
        {
            Previous = node;
            if (node != null)
                node.Next = this;
        }

        public void SetNext(Node node)
        {
            Next = node;
            if (node != null)
                node.Previous = this;
        }

        public static List<Node> Parse(string expression)
        {
            var nodes = new List<Node>();

            var startNode = new Node(NodeType.Empty);
            nodes.Add(startNode);
            for (var index = 0; index < expression.Length; index++)
            {
                var match = Regex.Match(expression.Substring(index), @"^(?:(\d+)|(\+)|(\*)|(\()|(\))|( ))");
                var nodeString = match.Groups.Values.Skip(1).Single(x => x.Success);
                switch (nodeString.Name)
                {
                    case "1":
                        nodes.Add(CreateNode(NodeType.Number, nodes.Last(), int.Parse(nodeString.Value)));
                        break;
                    case "2":
                        nodes.Add(CreateNode(NodeType.Add, nodes.Last()));
                        break;
                    case "3":
                        nodes.Add(CreateNode(NodeType.Multiply, nodes.Last()));
                        break;
                    case "4":
                        nodes.Add(CreateNode(NodeType.ParenthesisOpen, nodes.Last()));
                        break;
                    case "5":
                        nodes.Add(CreateNode(NodeType.ParenthesisClose, nodes.Last()));
                        break;
                    case "6":
                        break;
                    default: throw new Exception("Unexpected expression!");
                }
            }

            return nodes;
        }

        public static Node CreateNode(NodeType nodeType, Node lastNode, decimal value = 0)
        {
            var newNode = new Node(nodeType, value);
            newNode.SetPrevious(lastNode);
            return newNode;
        }

        public override string ToString()
        {
            return Type switch
            {
                NodeType.Empty => string.Empty,
                NodeType.Number => Value.ToString(CultureInfo.InvariantCulture),
                NodeType.Add => "+",
                NodeType.Multiply => "*",
                NodeType.ParenthesisOpen => "(",
                NodeType.ParenthesisClose => ")",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}