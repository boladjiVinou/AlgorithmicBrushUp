using skiena.datastructures.graph;
using skiena.datastructures.graph.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter6
{
    public class Chapter6
    {
        /*
         6.8
         */
        public static Tuple<char, char, int>? getMinimalChangeToGetDifferentMST(WeightedUndirectedGraph<char, int> graph) 
        {
            var mst = graph.kruskalMinimumSpanningTree();
            int minDelta = int.MaxValue;
            Tuple<char, char>? bestEdge = null;
            
            foreach (var edge in graph.getEdges().Where(e => !mst.areNodeConnected(e.Item1, e.Item2))) 
            {
                mst.connect(edge.Item1, edge.Item2, int.MaxValue);
                var newWeight = findWeightForADifferentMST(edge.Item1, edge.Item2, mst);
                var delta = graph.getWeight(edge.Item1, edge.Item2) - newWeight;
                if (delta < minDelta) 
                {
                    bestEdge = edge;
                }
                mst.disconnect(edge.Item1, edge.Item2);
            }
            if (bestEdge == null) 
            {
                return null;
            }

            return new Tuple<char, char, int>(bestEdge.Item1, bestEdge.Item2, graph.getWeight(bestEdge.Item1, bestEdge.Item2) - minDelta);

        }

        public static int findWeightForADifferentMST(char n1, char n2, WeightedUndirectedGraph<char, int> mst)
        {
            mst.connect(n1,n2, int.MinValue);

            Stack<Node> stack = [];
            stack.Push(new Node(n1,null));
            HashSet<char> visited = [];
            while (stack.Count > 0)
            {
                if (stack.Peek().value == n1 && visited.Contains(n1)) 
                {
                    break;
                }
                var curr = stack.Pop();
                if (visited.Contains(curr.value)) 
                {
                    continue;
                }
                foreach (var n in mst.getNeighbors(curr.value))
                {
                    stack.Push(new Node(n,curr));
                }
                visited.Add(curr.value);
            }

            int change = int.MinValue;
            if (stack.Count > 0 && stack.Peek().value == n1) 
            {
                var curr = stack.Pop();
                while (curr != null) 
                {
                    if (curr.prev != null) 
                    {
                        change = Math.Max(change, mst.getWeight(curr.prev.value, curr.value));
                    }
                    curr = curr.prev;
                }
            }

            mst.disconnect(n1, n2);
            return change - 1;

        }

        private class Node 
        {
            public char value { get; }
            public Node? prev { get; }
            public Node(char v, Node? parent)
            {
                this.prev = parent;
                this.value = v;
            }
        }
    }
}
