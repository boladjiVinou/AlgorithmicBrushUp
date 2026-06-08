using MoreLinq.Extensions;
using skiena.datastructures;
using skiena.datastructures.graph;
using skiena.datastructures.trees;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
            return new UndirectedGraph<int>(graph).computeChromaticNumberGreedy();
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
            foreach (string data in tree.postOrderIteration().Select(x=>x.Trim())) 
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
        Complexity O(n * 3 * n) n being nb of triangles
         */
        public static Graph<int> createDualGraph(List<int[]> triangles) 
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            Dictionary<CustomTuple<int>,HashSet<int>> prevTriangleLinkedToEdge = [];
            for (int idx = 0; idx<triangles.Count; idx++) 
            {
                int[] triangle = triangles[idx];
                graph.insertNode(idx);
                var edges = new List<CustomTuple<int>>() 
                { new(triangle[0], triangle[1]), 
                    new(triangle[0], triangle[2]),
                    new(triangle[2], triangle[1])};

                foreach (var edge in edges)
                {
                    if (prevTriangleLinkedToEdge.ContainsKey(edge))
                    {
                        foreach (var otherTriangle in prevTriangleLinkedToEdge[edge])
                        {
                            graph.connect(otherTriangle, idx);
                        }
                    }
                }
                foreach (var edge in edges)
                {
                    if (!prevTriangleLinkedToEdge.ContainsKey(edge))
                    {
                        prevTriangleLinkedToEdge.Add(edge, new HashSet<int>());
                    }
                    prevTriangleLinkedToEdge[edge].Add(idx);
                }
            }
            return graph;
        }
        /*
         5-12
         Complexity: (E*E/V) + E, => E^2
         */
        public static Dictionary<int, HashSet<int>> generateSquaredGraph(Dictionary<int, HashSet<int>> graph) 
        {
            Dictionary<int, HashSet<int>> squaredGraph = new Dictionary<int, HashSet<int>>();

            foreach (var u in graph.Keys)
            {
                if (!squaredGraph.ContainsKey(u)) 
                {
                    squaredGraph.Add(u, new HashSet<int>());
                }
                foreach (var v in graph[u])
                {
                    squaredGraph[u].Add(v);
                    foreach (var w in graph[v]) 
                    {
                        if (u != w) 
                        {
                            squaredGraph[u].Add(w);
                        }
                    }
                }
            }

            return squaredGraph;
        }

       public static Dictionary<int, Dictionary<int, bool>> generateSquaredGraph(Dictionary<int, Dictionary<int,bool>> graph)
        {
            Dictionary<int, Dictionary<int, bool>> squaredGraph = new Dictionary<int, Dictionary<int, bool>>();
            foreach (var u in graph.Keys) 
            {
                squaredGraph.Add(u, []);
                foreach (var v in graph[u].Keys)
                {
                    squaredGraph[u].Add(v, false);
                }
            }
            foreach (var u in graph.Keys)
            {
                foreach (var v in graph[u].Keys) 
                {

                    if (graph[u][v]) 
                    {
                        foreach (var w in graph[v].Keys) 
                        {
                            if (graph[v][w]) 
                            {
                                squaredGraph[u][w] = true;
                            }
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
        public static HashSet<int> minimumSizeVerticesA(Graph<int> tree) 
        {
            return [.. tree.getVertices().Where(x => tree.getInDegree(x) > 1)];
        }
        /*
         5.13.b
        the graph is expected to be a tree
         */
        private class TempNode<T>
        {
             public T node { get; set; }
             public T neighbor { get; set; }

            public TempNode(T node, T neighbor)
            {
                this.node = node;
                this.neighbor = neighbor;
            }
        }
        public static HashSet<int> minimumSizeVertexVersionB(UndirectedGraph<int> tree) 
        {
            HashSet<int> minimumVerticeSet = [];
            UndirectedGraph<int> tmpGraph = new(tree);
            PriorityQueue<TempNode<int>,int> verticeQueue = new();
            foreach (var v in tmpGraph.getVertices().Where(x=>tmpGraph.getInDegree(x) > 0))
            {
                var neighbor = tree.getNeighbors(v).First();
                var inDegree = tmpGraph.getInDegree(v);
                verticeQueue.Enqueue(new TempNode<int>(v,neighbor), -inDegree);
            }
            while (verticeQueue.Count>0)
            {
                var vertice = verticeQueue.Dequeue();
                if (!minimumVerticeSet.Contains(vertice.node) && !minimumVerticeSet.Contains(vertice.neighbor))
                {
                    minimumVerticeSet.Add(vertice.node);
                }
               
                foreach(var neighbor in tmpGraph.getNeighbors(vertice.node))
                {
                    tmpGraph.disconnect(vertice.node, neighbor);
                    var neighBorDegree = tmpGraph.getInDegree(neighbor);
                    if (neighBorDegree > 0) 
                    {
                        var neighborNeighbor = tree.getNeighbors(neighbor).First();
                        verticeQueue.Enqueue(new TempNode<int>(neighbor, neighborNeighbor), -neighBorDegree);
                    }
                }
            }
            
            return minimumVerticeSet;
        }
        /*
         5.13.c
         */
        public static HashSet<int> minimumWeightCover(UndirectedGraph<int> tree, Dictionary<int,int> weightByNode) 
        {
            HashSet<int> minimumVerticeSet = [];
            UndirectedGraph<int> tmpGraph = new(tree);
            PriorityQueue<TempNode<int>, int> verticeQueue = new();
            foreach (var v in tmpGraph.getVertices().Where(x => tmpGraph.getInDegree(x) > 0))
            {
                var neighbor = tree.getNeighbors(v).First();
                var inDegree = tmpGraph.getInDegree(v);
                verticeQueue.Enqueue(new TempNode<int>(v, neighbor), weightByNode[v] + inDegree);// at same weight favor nodes with multiple connections
            }
            while (verticeQueue.Count > 0)
            {
                var vertice = verticeQueue.Dequeue();
                if ( (!minimumVerticeSet.Contains(vertice.node) && !minimumVerticeSet.Contains(vertice.neighbor)))
                {
                    minimumVerticeSet.Add(vertice.node);
                }

                foreach (var neighbor in tmpGraph.getNeighbors(vertice.node))
                {
                    tmpGraph.disconnect(vertice.node, neighbor);
                    int neighborDegree = tmpGraph.getInDegree(neighbor);
                    if (neighborDegree > 0)
                    {
                        var neighborNeighbor = tree.getNeighbors(neighbor).First();
                        verticeQueue.Enqueue(new TempNode<int>(neighbor, neighborNeighbor), weightByNode[neighbor] + neighborDegree);// at same weight favor nodes with multiple connections
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
        public static bool containsIndependantSet(Graph<int> graph)
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
       public static ISet<int> computeMaxIndependentSetA(Graph<int> tree) 
        {
            HashSet<int> minimumVerticeSet = [];
            UndirectedGraph<int> tmpGraph = new(tree);
            Queue<TempNode<int>> verticeQueue = new();
            foreach (var v in tmpGraph.getVertices().Where(x => tmpGraph.getInDegree(x) == 0))
            {
                minimumVerticeSet.Add(v);
            }
            foreach (var v in tmpGraph.getVertices().Where(x => tmpGraph.getInDegree(x)  == 1))
            {
                var neighbor = tree.getNeighbors(v).First();
                verticeQueue.Enqueue(new TempNode<int>(v, neighbor));
            }
            while (verticeQueue.Count > 0)
            {
                var vertice = verticeQueue.Dequeue();
                if (!minimumVerticeSet.Contains(vertice.node) && !minimumVerticeSet.Contains(vertice.neighbor))
                {
                    minimumVerticeSet.Add(vertice.node);
                }

                foreach (var neighbor in tmpGraph.getNeighbors(vertice.node))
                {
                    tmpGraph.disconnect(vertice.node, neighbor);
                    var neighBorDegree = tmpGraph.getInDegree(neighbor);
                    if (neighBorDegree == 1)
                    {
                        var neighborNeighbor = tree.getNeighbors(neighbor).First();
                        verticeQueue.Enqueue(new TempNode<int>(neighbor, neighborNeighbor));
                    }
                }
            }

            return minimumVerticeSet;
        }
        /*
         5.16.b
        */
        public static ISet<int> computeMaxIndependentSetB(Graph<int> tree)
        {

            HashSet<int> minimumVerticeSet = [];
            UndirectedGraph<int> tmpGraph = new(tree);
            PriorityQueue<TempNode<int>, int> verticeQueue = new();
            foreach (var v in tmpGraph.getVertices().Where(x => tmpGraph.getInDegree(x) == 0))
            {
                minimumVerticeSet.Add(v);
            }
            foreach (var v in tmpGraph.getVertices().Where(x => tmpGraph.getInDegree(x) > 0))
            {
                var neighbor = tree.getNeighbors(v).First();
                var inDegree = tmpGraph.getInDegree(v);
                verticeQueue.Enqueue(new TempNode<int>(v, neighbor), -inDegree);
            }
            while (verticeQueue.Count > 0)
            {
                var vertice = verticeQueue.Dequeue();
                if (!minimumVerticeSet.Contains(vertice.node) && !minimumVerticeSet.Contains(vertice.neighbor))
                {
                    minimumVerticeSet.Add(vertice.node);
                }

                foreach (var neighbor in tmpGraph.getNeighbors(vertice.node))
                {
                    tmpGraph.disconnect(vertice.node, neighbor);
                    var neighBorDegree = tmpGraph.getInDegree(neighbor);
                    if (neighBorDegree > 0)
                    {
                        var neighborNeighbor = tree.getNeighbors(neighbor).First();
                        verticeQueue.Enqueue(new TempNode<int>(neighbor, neighborNeighbor), -neighBorDegree);
                    }
                }
            }

            return minimumVerticeSet;
        }

        /*
         5.16.c
        */
        public static ISet<int> computeMaxIndependentSetC(Graph<int> tree,Dictionary<int,int> vertexWeights)
        {
            HashSet<int> maxSet = [];
            int maxWeight = 0;

            foreach (var v in tree.getVertices())
            {
                HashSet<int> tmpSet1 = new HashSet<int>();
                int size1 = computeBestWeightIndependantSet(tree, v,  tmpSet1, n => vertexWeights[n], [],0);

                if (size1 > maxWeight) 
                {
                    maxSet = tmpSet1;
                    maxWeight = size1;
                }
            }

            return maxSet;
        }

        public static int computeBestWeightIndependantSet(Graph<int> tree, int node, HashSet<int> set, Func<int, int> weightProvider, HashSet<int> excludedNode,int currWeight)
        {
            // when excluding the curr node
            int weightWithoutNode = currWeight;
            HashSet<int> setWhenNodeNotAdded = [];
            excludedNode.Add(node);
            foreach (var n in tree.getNeighbors(node).Where(x => !excludedNode.Contains(x)))
            {
                weightWithoutNode = computeBestWeightIndependantSet(tree, n, setWhenNodeNotAdded, weightProvider, excludedNode, weightWithoutNode);
            }

            // when taking the curr node

            excludedNode.Remove(node);
            int weightWithNode = currWeight + weightProvider(node);
            foreach (var n in tree.getNeighbors(node))
            {
                excludedNode.Add(n);
            }
            HashSet<int> setWhenNodeAdded = [];
            foreach (var n in tree.getNeighbors(node).Where(x => !excludedNode.Contains(x)).SelectMany(y => tree.getNeighbors(y).Where(z => z != node && !excludedNode.Contains(z))))
            {

                weightWithNode = computeBestWeightIndependantSet(tree, n, setWhenNodeAdded, weightProvider, excludedNode, weightWithNode);
            }

            // comparing and taking the best

            if (weightWithNode > weightWithoutNode)
            {
                foreach (var item in setWhenNodeAdded)
                {
                    set.Add(item);
                }
                set.Add(node);
                return weightWithNode;
            }
            else 
            {
                foreach (var n in tree.getNeighbors(node))
                {
                    excludedNode.Add(n);
                }
                foreach (var item in setWhenNodeNotAdded)
                {
                    set.Add(item);
                }
                return weightWithoutNode;
            }
        }


        /*
         5.17
         */
        public static bool findTriangleVersionA(Graph<int> graph) 
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
        public static bool findTriangleVersionB(Graph<int> graph) 
        {
            foreach (var root in graph.getPossibleCommonRoot())
            {
                if (checkTriangleWithDfs(graph, root, new HashSet<int>())) 
                {
                    return true;
                }
            }
            return false;
        }
       public static bool checkTriangleWithDfs(Graph<int> graph, int node, ISet<int> encountered) 
        {
            encountered.Add(node);
            foreach (var item in graph.getNeighbors(node))
            {
                if (item != node && encountered.Contains(item)) 
                {
                    return true;
                }
                graph.disconnect(node, item);
                if (checkTriangleWithDfs(graph, item, encountered))
                {
                    graph.connect(node, item);
                    return true;
                }
                else 
                {
                    graph.connect(node, item);
                }
            }
            encountered.Remove(node);
            return false;
        }

        /*
         5.18
         */
        public static Dictionary<int, int> findSchedule(List<Tuple<int,int>> desiredMoviesPerClient) 
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            foreach (var item in desiredMoviesPerClient)
            {
                graph.connect(item.Item1, item.Item2);
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
        public static int computeDiameter(DirectedGraph<int> tree) 
        {
            int diameter = 0;
          //  int maxDegree = tree.getVertices().Max(x => tree.getInDegree(x));
            foreach (var item in tree.getPossibleCommonRoot())
            {
                Stack<Tuple<int,int>> stack = [];
                stack.Push(new Tuple<int,int>(item,0));
                HashSet<int> visited = [];
                while (stack.Count > 0) 
                {
                    var currData = stack.Pop();
                    visited.Add(currData.Item1);
                    var neighbors = tree.getNeighbors(currData.Item1).Where(x => !visited.Contains(x));
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
        public static UndirectedGraph<int> computeMaximumInducedSubgraph(UndirectedGraph<int> graph, int k) 
        {
            return graph.computeMaximumInducedSubgraph(k);
        }

        /*
         5.21
         */
        record NodeData(int value, int pathLength) 
        {
        }
       public static int findNumberOfShortestPath(Graph<int> directedGraph, int v, int w) 
        {
            int pathLength = -1;
            int nbPath = 0;
            Queue<NodeData> q = [];
            q.Enqueue(new NodeData(v,0));
            while (q.Count > 0) 
            {
                var tmp = q.Dequeue();
                if (tmp.value == w) 
                {
                    if (pathLength > 0 && pathLength != tmp.pathLength) 
                    {
                        break; // in bread first search if it is different it means it is greater , since the edges has no weight
                    }
                    ++nbPath;
                    pathLength = tmp.pathLength;
                }
                foreach(int neighbor in directedGraph.getNeighbors(tmp.value)) 
                {
                    q.Enqueue(new NodeData(neighbor, tmp.pathLength + 1));
                }
            }
            return nbPath;
        }

        /*
         5.22
         */
        public static void reduceEdges(UndirectedGraph<int> graph) 
        {
            Queue<int> q = [];
            foreach (int v in graph.getVertices().Where(x => graph.getInDegree(x) == 2)) 
            {
                q.Enqueue(v);
            }
            while (q.Count > 0)
            {
                int v = q.Dequeue();
                var neighbors = graph.getNeighbors(v).ToList();
                foreach (var n in neighbors)
                {
                    graph.disconnect(v, n);
                }
                graph.connect(neighbors[0], neighbors[1]);
                if (graph.getInDegree(neighbors[0]) == 2) 
                {
                    q.Enqueue(neighbors[0]);
                }
                if (graph.getInDegree(neighbors[1]) == 2) 
                {
                    q.Enqueue(neighbors[1]);
                }
            }
        }

        /*
         5.23
        a) directed edge when 1 -> 2,  1 hates 2
         */
       public  static List<int> getLineOrderA(DirectedGraph<int> relations) 
        {
            var degreesByNode = relations.getVertices().ToDictionary(x => x, y => relations.getInDegree(y));
            Queue<int> q = [];
            foreach (var e in degreesByNode.Keys.Where(x => degreesByNode[x] == 0)) 
            {
                q.Enqueue(e);
            }
            List<int> result = [];
            while (q.Count > 0) 
            {
                int v = q.Dequeue();
                result.Add(v);
                foreach (var e in relations.getNeighbors(v)) 
                {
                    --degreesByNode[e];
                    if(degreesByNode[e] == 0) 
                    {
                        q.Enqueue(e);
                    }
                }
            }
            return degreesByNode.Values.Any(x =>x > 0) ? []:result;
        }
        /*
         5.23 b)
         */
       public  static int getLineOrderB(DirectedGraph<int> relations) 
        {
            var degreesByNode = relations.getVertices().ToDictionary(x => x, y => relations.getInDegree(y));
            Queue<Tuple<int,int>> q = [];
            foreach (var e in degreesByNode.Keys.Where(x => degreesByNode[x] == 0))
            {
                q.Enqueue(new Tuple<int, int>(e,0));
            }
            int minRow = 0;
            while (q.Count > 0)
            {
                var tmp = q.Dequeue();
                minRow = Math.Max(minRow, tmp.Item2);
                foreach (var e in relations.getNeighbors(tmp.Item1))
                {
                    --degreesByNode[e];
                    if (degreesByNode[e] == 0)
                    {
                        q.Enqueue(new Tuple<int, int>(e, tmp.Item2+1));
                    }
                }
            }
            return degreesByNode.Values.Any(x => x > 0) ? -1:  minRow;
        }
        /*
         5.25


         */

        public static bool graphContainsAnArborescence(DirectedGraph<int> graph) 
        {
            return graph.containsAnArborescence();
        }

        /*
         5.26
         */
        public static bool isAMotherVertex(DirectedGraph<int> graph, int node) 
        {
            return graph.isAMotherVertex(node);
        }
        public static bool containsAMotherVertex(DirectedGraph<int> graph) 
        {
            return graph.containsAMotherVertex();
        }

        /*
         5.27
         */
        public static List<int> getHamiltonianPath(UndirectedGraph<int> graph) 
        {
            return graph.getHamiltonianPath();
        }
        /*
         5.28
         */
        public static IEnumerable<int> getNonArticulationNodes(Graph<int> graph) 
        {
            return graph.getNonArticulationNodes().Select(x=> x.Value);
        }
        /*
         5.29
         */
        public static List<int> getDeletionOrder(Graph<int> graph) 
        {
            return [.. graph.getDeletionOrder().Select(x=>x.Value)];
        }
    }
}
