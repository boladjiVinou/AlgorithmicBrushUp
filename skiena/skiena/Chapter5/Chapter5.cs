using skiena.datastructures;
using skiena.datastructures.graph;
using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter5
{
    public class Chapter5
    {
        /*
         5-5: No the graph is not necessarly bipartite ex: triangle
         */
        public static int computeChromaticNumber(Graph<int> graph) 
        {
            return graph.computeChromaticNumberGreedy();
        }
        /*
         5-6
        a) v being the root of a tree of n-1 other nodes, the other nodes being leaves
        b) v being the root of a linked list of n-1 nodes
        c) v being the root of a binary tree, having done the dfs of the left sub tree of v
         */

        /*
         5.7
        We can reconstruct tree with inorder + (post or pre order)
        if the values are distinct
        We cannot reconstruct an unique tree with only pre and post order
        A
         \
          B
         /
        C

        A
       /
       B
        \
         C
        both have as pre order : ABC and post order CBA but they are different
         */
        public static MyBST<int> reconstructTreeWithPreOrderAndInOrder(List<int> preOrderVisit, List<int> inOrderVisit) 
        {
            return MyBST<int>.buildFromPreOrderVisitAndInOrderVisit(preOrderVisit, inOrderVisit);
        }
        public static MyBST<int> reconstructTreeWithPostOrderAndInOrder(List<int> postOrderVisit, List<int> inOrderVisit)
        {
            return MyBST<int>.buildFromPostOrderVisitAndInOrderVisit(postOrderVisit, inOrderVisit);
        }
        /*
         * 5.9, 5.10
         */
        public static long evaluateExpression(MyBT<string> tree) 
        {
            Stack<long> resStack = new Stack<long>();
            Dictionary<string, Action<long>> functionByCode = new Dictionary<string, Action<long>>
            {
                { "+", (v) =>  resStack.Push(resStack.Pop() +v)},
                { "-", (v) => resStack.Push(resStack.Pop() - v)},
                { "*", (v) => resStack.Push(resStack.Pop() * v) },
                { "/", (v) => resStack.Push(resStack.Pop() / v) }
            }; 
            foreach (string data in tree.postOrderIteration()) 
            {
                if (long.TryParse(data, out long parameter))
                {
                    resStack.Push(parameter);
                }
                else 
                {
                    functionByCode[data](resStack.Pop());
                }
            }
            return resStack.Any() ? resStack.Single():0;
        }
        /*
         5.11
        return a graph of connected triangle indexes
        Complexity O(n * 6) n being nb of triangles
         */
        public static Graph<int> createDualGraph(List<int[]> triangles) 
        {
            Graph<int> graph = new Graph<int>();
            Dictionary<CustomTuple<int>,int> prevTriangleLinkedToEdge = [];
            for (int idx = 0; idx<triangles.Count; idx++) 
            {
                int[] triangle = triangles[idx];
                for (int i = 0; i < triangle.Length; i++)
                {
                    var otherEdges = enumarateNeighbors(triangle, i).Select(x =>new CustomTuple<int>(triangle[i],x)).ToList();

                    foreach(var edge in otherEdges) 
                    {
                        if (prevTriangleLinkedToEdge.ContainsKey(edge) && prevTriangleLinkedToEdge[edge] != idx)
                        {
                            graph.biDirectionalConnect(prevTriangleLinkedToEdge[edge], idx);
                        }
                        if (!prevTriangleLinkedToEdge.ContainsKey(edge))
                        {
                            prevTriangleLinkedToEdge.Add(edge, idx);
                        }
                        else 
                        {
                            prevTriangleLinkedToEdge[edge] = idx;
                        }
                    }
                }
            }
            return graph;
        }

        static IEnumerable<int> enumarateNeighbors(int[] triangle, int curr) 
        {
            for (int i = 0; i < triangle.Length; ++i) 
            {
                if (i == curr) 
                {
                    continue;
                }
                yield return triangle[i];
            }
        }
        /*
         Complexity: (E*E/V) + E, => E^2
         */
        static Dictionary<int, HashSet<int>> generateSquaredGraph(Dictionary<int, HashSet<int>> graph) 
        {
            Dictionary<int, HashSet<int>> squaredGraph = new Dictionary<int, HashSet<int>>(graph);
            Dictionary<int, HashSet<int>> parentByNode = new Dictionary<int, HashSet<int>>(graph);

            foreach (var n in graph.Keys) 
            {
                foreach (var v in graph[n]) 
                {
                    if (!parentByNode.ContainsKey(v))
                    {
                        parentByNode.Add(v, new HashSet<int>());
                    }
                    parentByNode[v].Add(n);
                }
            }
            foreach (var v in graph.Keys)
            {
                foreach (var w in graph[v])
                {
                    foreach (var u in parentByNode[v]) 
                    {
                        squaredGraph[u].Add(w);
                    }
                }
            }

            return squaredGraph;
        }

        static Dictionary<int, Dictionary<int, bool>> generateSquaredGraph(Dictionary<int, Dictionary<int,bool>> graph)
        {
            Dictionary<int, Dictionary<int, bool>> squaredGraph = new Dictionary<int, Dictionary<int, bool>>(graph);
            foreach (var u in graph.Keys)
            {
                foreach (var v in graph[u].Keys) 
                {
                    if (graph[u][v]) 
                    {
                        foreach (var w in graph[v].Keys) 
                        {
                            squaredGraph[u][w] = true;
                        }
                    }
                }
            }
            return squaredGraph;
        }
    }
}
