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
                var newWeight = findWeightForADifferentMST(edge.Item1, edge.Item2, mst);
                var delta = graph.getWeight(edge.Item1, edge.Item2) - newWeight;
                if (delta < minDelta) 
                {
                    bestEdge = edge;
                    minDelta = delta;
                }
            }
            if (bestEdge == null) 
            {
                return null;
            }

            return new Tuple<char, char, int>(bestEdge.Item1, bestEdge.Item2, graph.getWeight(bestEdge.Item1, bestEdge.Item2) - minDelta);

        }

        private static int findWeightForADifferentMST(char n1, char n2, WeightedUndirectedGraph<char, int> mst)
        {
            Queue<Node> queue = [];
            queue.Enqueue(new Node(n1,int.MinValue));
            HashSet<char> visited = new HashSet<char>();
            int change = int.MinValue;
            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                if (curr.value == n2)
                {
                    change = Math.Max(change, curr.weight);
                }
                foreach (var n in mst.getNeighbors(curr.value))
                {
                    if (visited.Contains(n)) 
                    {
                        continue;
                    }
                    var next = new Node(n, Math.Max(curr.weight, mst.getWeight(curr.value, n)));
                    queue.Enqueue(next);
                }
                visited.Add(curr.value);
            }

            return change - 1;
        }



        private class Node 
        {
            public char value { get; }
            public int weight { get; set; }
            public Node(char v, int weight)
            {
                this.value = v;
                this.weight = weight;
            }
        }
    }
}
