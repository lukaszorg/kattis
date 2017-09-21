using System;
using System.IO;
using KattisSolution.IO;

namespace KattisSolution
{
    public enum Gate
    {
        And,
        Or
    }

    public class Node
    {
        public bool IsLeaf;
        public bool LeafValue;
        public bool Changable;
        public Gate Type;
    }

    public class NodeRef
    {
        public int Index;
        public Node[] NodeList;

        public NodeRef(Node[] nodeList)
        {
            Index = 0;
            NodeList = nodeList;
        }

        public NodeRef(int index, Node[] nodeList)
        {
            Index = index;
            NodeList = nodeList;
        }

        public NodeRef LeftChild()
        {
            var newIndex = 1 + 2 * Index;
            return newIndex < NodeList.Length ?  new NodeRef(newIndex, NodeList) : null ;
        }

        public NodeRef RightChild()
        {
            var newIndex = 2 + 2 * Index;
            return newIndex < NodeList.Length ?  new NodeRef(newIndex, NodeList) : null;
        }

        public bool Eval()
        {
            var node = NodeList[Index];
            if (node.IsLeaf)
                return node.LeafValue;
            if (node.Type == Gate.And)
                return LeftChild().Eval() && RightChild().Eval();

            return LeftChild().Eval() || RightChild().Eval();
        }

        public int MinChanges(bool desired)
        {
            var curValue = Eval();
            if (curValue == desired)
                return 0;

            var node = NodeList[Index];
            if (node.IsLeaf)
                return int.MinValue;

            var leftVal = LeftChild().Eval();
            var rightVal = RightChild().Eval();

            if (!node.Changable)
            {
                if (!desired && node.Type == Gate.Or && leftVal && rightVal)
                    return Add(LeftChild().MinChanges(false), RightChild().MinChanges(false));

                if (!desired && node.Type == Gate.Or && !leftVal && rightVal)
                    return RightChild().MinChanges(false);

                if (!desired && node.Type == Gate.Or && leftVal && !rightVal)
                    return LeftChild().MinChanges(false);

                if (desired && node.Type == Gate.Or && !leftVal && !rightVal)
                    return Min(LeftChild().MinChanges(true), RightChild().MinChanges(true));

                if (desired && node.Type == Gate.And && !leftVal && !rightVal)
                    return Add(LeftChild().MinChanges(true),  RightChild().MinChanges(true));

                if (desired && node.Type == Gate.And && leftVal && !rightVal)
                    return RightChild().MinChanges(true);

                if (desired && node.Type == Gate.And && !leftVal && rightVal)
                    return LeftChild().MinChanges(true);

                if (!desired && node.Type == Gate.And && leftVal && rightVal)
                    return Min(LeftChild().MinChanges(false), RightChild().MinChanges(false));
            }
            else
            {
                if (!desired && node.Type == Gate.Or && (!leftVal || !rightVal))
                    return 1;

                if (desired && node.Type == Gate.Or && !leftVal && !rightVal)
                    return Min(LeftChild().MinChanges(true), RightChild().MinChanges(true));

                if (!desired && node.Type == Gate.Or && leftVal && rightVal)
                    return Add(1, Min(LeftChild().MinChanges(false), RightChild().MinChanges(false)));

                if (desired && node.Type == Gate.And && (leftVal || rightVal))
                    return 1;

                if (!desired && node.Type == Gate.And && leftVal && rightVal)
                    return Min(LeftChild().MinChanges(false), RightChild().MinChanges(false));

                if (desired && node.Type == Gate.And && !leftVal && !rightVal)
                    return Add(1, Min(LeftChild().MinChanges(true), RightChild().MinChanges(true)));
            }

            return int.MinValue;

        }

        private int Add(int a, int b)
        {
            if(a == int.MinValue || b == int.MinValue)
                return int.MinValue;

            return a + b;
        }

        private int Min(int a, int b)
        {
            if (a == int.MinValue)
                return b;
            if (b == int.MinValue)
                return a;

            return Math.Min(a, b);
        }
}

    internal class Program
    {
        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            IScanner scanner = new OptimizedPositiveIntReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var n = scanner.NextInt();

            for (int i = 0; i < n; i++)
            {
                var nodeCount = scanner.NextInt();
                var desired = scanner.NextInt() != 0;
                var nodes = new Node[nodeCount];

                for (int j = 0; j < (nodeCount-1)/2; j++)
                {
                    nodes[j] = new Node{Type = scanner.NextInt() == 1 ? Gate.And : Gate.Or, Changable = scanner.NextInt()!=0} ;
                }

                for (int j = (nodeCount - 1) / 2; j < nodeCount; j++)
                {
                    nodes[j] = new Node { IsLeaf = true, LeafValue = scanner.NextInt() != 0 };
                }

                var tree = new NodeRef(nodes);

                var result = tree.MinChanges(desired);

                writer.Write("Case #" + (i+1) + ": " + (result == int.MinValue ? "IMPOSSIBLE" : result.ToString()) + "\n");


            }
            writer.Flush();
        }

    }
}