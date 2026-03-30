using skiena.datastructures;
using skiena.datastructures.graph;
using skiena.datastructures.trees;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
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
         5-12
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
        /*
         5.13.a
        We have a tree -> there is no cycle in it
        there is no cycle -> each edge lead to an uncovered node
        an edge is composed by 2 nodes -> by removing the leaves the cover is the remaining nodes, we cant have less than that
         */
        static HashSet<int> minimumSizeVerticesA(Graph<int> tree) 
        {
            return [.. tree.getVertices().Where(x => tree.getDegree(x) > 1)];
        }
        /*
         5.13.b
         */
        static HashSet<int> minimumSizeVertexVersionB(Graph<int> graph) 
        {
            HashSet<int> minimumVerticeSet = [];
            Graph<int> tmpGraph = new(graph);
            var orderedVertices =  tmpGraph.getVertices().OrderByDescending(x => tmpGraph.getDegree(x)).ToList();
            foreach (var v in orderedVertices)
            {
                HashSet<int> neighbors = tmpGraph.getNeighbors(v);
                if (neighbors.Count != 0) 
                {
                    minimumVerticeSet.Add(v);
                    foreach (var neighbor in neighbors)
                    {
                        tmpGraph.biDirectionalDisconnect(v, neighbor);
                    }
                }
            }
            return minimumVerticeSet;
        }
        /*
         5.13.c
         */
        static HashSet<int> minimumWeightCover(Graph<int> graph, Dictionary<int,int> weightByNode) 
        {
            HashSet<int> minimumVerticeSet = [];
            Graph<int> tmpGraph = new(graph);
            var orderedVertices = tmpGraph.getVertices()
                .OrderByDescending(x => tmpGraph.getDegree(x) - weightByNode[x])
                .ToList();
            foreach (var v in orderedVertices)
            {
                HashSet<int> neighbors = tmpGraph.getNeighbors(v);
                if (neighbors.Count != 0)
                {
                    minimumVerticeSet.Add(v);
                    foreach (var neighbor in neighbors)
                    {
                        tmpGraph.biDirectionalDisconnect(v, neighbor);
                    }
                }
            }
            return minimumVerticeSet;
        }

        /*
         5.14
        Not necesarly

          n1  -- n2
          |   \
          n3   n4

        We can generate a dfs tree
        n1 -> n2
        n1 -> n3
        n1 -> n4
        deleting the leaves gives us n1 as cover

        but we can also have as tree by starting randomly by n2

        n2 -> n1
        n1 -> n3
        n1 -> n4

        deleting the leaves gives us as cover, n2,n1 which is not minimal cover of the graph
         */

        /*
         5.15
        It means the graph is bipartite
         */
        static bool containsIndependantSet(Graph<int> graph)
        {
            /*var cover = minimumSizeVertexVersionB(graph);
            var edges = graph.getEdges();
            if (edges.Count > Math.Pow(cover.Count, 2)) 
            {
                foreach (var v in cover) 
                {
                    foreach (var u in cover) 
                    {
                        if (v != u && graph.areNodeConnected(u,v))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return !graph.getEdges()
                .Where(x => cover.Contains(x.Item1) && cover.Contains(x.Item2))
                .Any();*/
            Dictionary<int, int> colorByNode = new Dictionary<int, int>();
            return graph.isBipartite(out colorByNode);

        }
        /**
         5.16.a
         */
        static ISet<int> computeMaxIndependentSetA(Graph<int> tree, int root) 
        {
            HashSet<int> independentSet = [];
            int maxSize= computeMaxIndependentSetSize(tree, root, [],false,independentSet, (n)=> 1);
            if (maxSize != independentSet.Count) 
            {
                throw new ApplicationException("computed set size mismatch");
            }
            return independentSet;
        }
        /*
         5.16.b
        */
        static ISet<int> computeMaxIndependentSetB(Graph<int> tree, int root)
        {
            HashSet<int> independentSet = [];
            computeMaxIndependentSetSize(tree, root, [], false, independentSet, (n) => tree.getDegree(n));
            return independentSet;
        }

        /*
         5.16.c
        */
        static ISet<int> computeMaxIndependentSetC(Graph<int> tree,Dictionary<int,int> vertexWeights, int root)
        {
            HashSet<int> independentSet = [];
            computeMaxIndependentSetSize(tree, root, [], false,
                independentSet, (n) => vertexWeights.TryGetValue(n, out int weight) ? weight : 1);
            return independentSet;
        }

        static int computeMaxIndependentSetSize(Graph<int> tree, int node, Dictionary<int, int> memo, bool parentIncluded, ISet<int> independentSet, Func<int,int> weightProvider) 
        {
            if (memo.TryGetValue(node, out int value)) 
            {
                return value;
            }

            if (!parentIncluded) 
            {
                independentSet.Add(node);
            }

            int sizeWithNodeIncluded = parentIncluded ? 0 : tree.getNeighbors(node)
                .Where(x => !independentSet.Contains(x))
                .Sum(x => computeMaxIndependentSetSize(tree, x, memo, true, independentSet,weightProvider)) + weightProvider(node);

            if (!parentIncluded)
            {
                independentSet.Remove(node);
            }

            int sizeWithNodeExcluded = tree.getNeighbors(node)
                .Where(x => !independentSet.Contains(x))
                .Sum(x => computeMaxIndependentSetSize(tree, x, memo, false, independentSet,weightProvider));

            int finalResult = 0;
            if (sizeWithNodeIncluded > sizeWithNodeExcluded)
            {
                independentSet.Add(value);
                finalResult = sizeWithNodeIncluded;
            }
            else 
            {
                finalResult = sizeWithNodeExcluded;
            }
            memo.Add(node, finalResult);
            return finalResult;
        }

        /*
         5.17
         */
        static bool findTriangleVersionA(Graph<int> graph) 
        {
            foreach (var n1 in graph.getVertices()) 
            {
                foreach (var n2 in graph.getNeighbors(n1)) 
                {
                    foreach (var n3 in graph.getNeighbors(n2)) 
                    {
                        if (n3 != n1 && graph.areNodeConnected(n3,n1)) 
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static bool findTriangleVersionB(Graph<int> graph) 
        {
            foreach (var root in graph.getRoots())
            {
                if (checkTriangleWithDfs(graph, root, new HashSet<int>())) 
                {
                    return true;
                }
            }
            return false;
        }
        static bool checkTriangleWithDfs(Graph<int> graph, int node, ISet<int> encountered) 
        {
            encountered.Add(node);
            foreach (var item in graph.getNeighbors(node))
            {
                if (item != node && encountered.Contains(item)) 
                {
                    return true;
                }
                if (checkTriangleWithDfs(graph, item, encountered)) 
                {
                    return true;
                }
            }
            encountered.Remove(node);
            return false;
        }

        /*
         5.18
         */
        static Dictionary<int, int> findSchedule(List<Tuple<int,int>> desiredMoviesPerClient) 
        {
            Graph<int> graph = new Graph<int>();
            foreach (var item in desiredMoviesPerClient)
            {
                graph.biDirectionalConnect(item.Item1, item.Item2);
            }
            Dictionary<int, int> colorByNode;
            if (graph.isBipartite(out colorByNode))
            {
                return colorByNode;
            }
            return [];
        }
        /*
         5.19
         */
        static int computeDiameter(Graph<int> tree) 
        {
            int diameter = 0;
            int maxDegree = tree.getVertices().MaxBy(x => tree.getDegree(x));
            foreach (var item in tree.getRoots())
            {
                Stack<Tuple<int,int>> stack = [];
                stack.Push(new Tuple<int,int>(item,0));
                while (stack.Count > 0) 
                {
                    var currData = stack.Pop();
                    var neighbors = tree.getNeighbors(currData.Item1).Where(x => x != currData.Item1);
                    foreach (var item1 in neighbors)
                    {
                        stack.Push(new Tuple<int, int>(item1, currData.Item2 + 1));
                    }
                    if (!neighbors.Any()) 
                    {
                        diameter = Math.Max(diameter, currData.Item2);
                    }
                }
                
            }
            return diameter;
        }

        /*
         5.20
         */
        static Graph<int> computeMaximumInducedSubgraph(Graph<int> graph, int k) 
        {
            return graph.computeMaximumInducedSubgraph(k);
        }


    }
}
