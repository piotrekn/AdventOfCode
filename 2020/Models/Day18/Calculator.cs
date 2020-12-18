using System;
using System.Collections.Generic;
using System.Linq;

namespace _2020.Models.Day18
{
    public class Expression
    {
        private List<Node> _tokens;

        public Expression(string expression)
        {
            _tokens = Node.Parse(expression);
        }

        public decimal CalculateStrat1()
        {
            var startNode = _tokens.First();
            var current = _tokens.First();
            var history = new List<string> { string.Join(string.Empty, _tokens) };

            while (_tokens.Count > 1 && current.Next != null)
            {
                current = ProcessNode(current.Next, startNode);

                _tokens = ToList(startNode);
                var listString = string.Join(string.Empty, _tokens);
                history.Add(listString);
            }

            return startNode.Next.Value;
        }

        public decimal CalculateStrategy2()
        {
            var startNode = _tokens.First();
            var current = _tokens.First();
            var history = new List<string> { string.Join(string.Empty, _tokens) };
            var length = -1;

            // first parenthesis
            var priorities = _tokens.Where(x => x.Type == NodeType.ParenthesisClose).ToList();
            foreach (var priority in priorities)
            {
                var open = priority;
                while (open.Type != NodeType.ParenthesisOpen)
                    open = open.Previous.Type == NodeType.Add ? ProcessNode(open.Previous, priority) : open.Previous;

                open = priority;
                while (open.Type != NodeType.ParenthesisOpen)
                    open = ProcessNode(open.Previous, priority);

                ProcessNode(priority, priority);
            }
            _tokens = ToList(startNode);

            // then addition
            while (_tokens.Count != length)
            {
                foreach (var node in _tokens
                    .Where(x => x.Type == NodeType.Add))
                {
                    ProcessNode(node, startNode);
                }
                _tokens = ToList(startNode);

                var listString = string.Join(string.Empty, _tokens);
                history.Add(listString);
                length = _tokens.Count;
            }

            // than everything else
            while (_tokens.Count > 1 && current.Next != null)
            {
                current = ProcessNode(current.Next, startNode);

                _tokens = ToList(startNode);
                var listString = string.Join(string.Empty, _tokens);
                history.Add(listString);
            }

            return startNode.Next.Value;
        }

        private static Node ProcessNode(Node current, Node startNode)
        {
            switch (current.Type)
            {
                case NodeType.Add:
                    current = Add(current);
                    break;
                case NodeType.Multiply:
                    current = Multiply(current);
                    break;
                case NodeType.ParenthesisOpen:
                    break;
                case NodeType.ParenthesisClose:
                    current = ParenthesisOpen(current) ? startNode : current;
                    break;
                case NodeType.Number:
                case NodeType.Empty:
                default:
                    break;
            }

            return current;
        }

        private static Node Add(Node current)
        {
            if (current.Type != NodeType.Add)
                return current;
            return CalculateOperator(current.Previous, current.Next, (a, b) => a + b) ?? current;
        }
        private static Node Multiply(Node current)
        {
            if (current.Type != NodeType.Multiply)
                return current;
            return CalculateOperator(current.Previous, current.Next, (a, b) => a * b) ?? current;
        }
        private static bool ParenthesisOpen(Node current)
        {
            if (current.Type != NodeType.ParenthesisClose)
                return false;

            var number = current.Previous;
            var opened = current.Previous.Previous;
            if (number.Type != NodeType.Number || opened.Type != NodeType.ParenthesisOpen)
                return false;

            number.SetNext(current.Next);
            number.SetPrevious(opened.Previous);
            return true;
        }

        private static List<Node> ToList(Node node)
        {
            var list = new List<Node> { node };

            while (list.First().Previous != null)
                list.Insert(0, list.First().Previous);

            while (list.Last().Next != null)
                list.Add(list.Last().Next);

            return list;
        }

        private static Node CalculateOperator(Node a, Node b, Func<decimal, decimal, decimal> function)
        {
            if (a.Type != NodeType.Number || b.Type != NodeType.Number)
                return null;
            var newNode = Node.CreateNode(NodeType.Number, a.Previous, function(a.Value, b.Value));
            newNode.SetNext(b.Next);
            return newNode;
        }
    }
}
