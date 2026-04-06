using skiena.datastructures.trees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace skiena.datastructures.graph
{
    public class Graph<T> where T : IEquatable<T>
    {
        Dictionary<T,GraphNode<T>> nodePerValue = [];
        Dictionary<GraphNode<T>, HashSet<GraphNode<T>>> neighborsPerNode = [];
        private Action<T> beforeVisitAction = (v) => { };
        private Action<T> afterVisitAction = (v) => { };
        private Action<T> visitAction = (v) => { };

        public Graph() 
        {
        }
        public Graph(Graph<T> graph) 
        {
            nodePerValue = new Dictionary<T,GraphNode<T>>(graph.nodePerValue);
            neighborsPerNode = new Dictionary<GraphNode<T>, HashSet<GraphNode<T>>>(graph.neighborsPerNode);
        }

        public void biDirectionalConnect(T n1, T n2)
        {
            if (!nodePerValue.ContainsKey(n1))
            {
                nodePerValue.Add(n1, createNode(n1));
                neighborsPerNode.Add(nodePerValue[n1], []);
            }
            if (!nodePerValue.ContainsKey(n2))
            {
                nodePerValue.Add(n2, createNode(n2));
                neighborsPerNode.Add(nodePerValue[n2], []);
            }
            neighborsPerNode[nodePerValue[n1]].Add(nodePerValue[n2]);
            neighborsPerNode[nodePerValue[n2]].Add(nodePerValue[n1]);
        }


        public void biDirectionalDisconnect(T n1, T n2)
        {
            if (!nodePerValue.ContainsKey(n1) || !nodePerValue.ContainsKey(n2))
            {
                return;
            }
            var node1 = nodePerValue[n1];
            var node2 = nodePerValue[n2];
            neighborsPerNode[node1].Remove(node2);
            neighborsPerNode[node2].Remove(node1);
            if (neighborsPerNode[node1].Count == 0) 
            {
                neighborsPerNode.Remove(node1);
                nodePerValue.Remove(n1);
            }
            if (neighborsPerNode[node2].Count == 0)
            {
                neighborsPerNode.Remove(node2);
                nodePerValue.Remove(n2);
            }
        }
        public void uniDirectionalConnect(T n1, T n2)
        {
            if (!nodePerValue.ContainsKey(n1))
            {
                nodePerValue.Add(n1, createNode(n1));
                neighborsPerNode.Add(nodePerValue[n1], []);
            }
            if (!nodePerValue.ContainsKey(n2))
            {
                nodePerValue.Add(n2, createNode(n2));
                neighborsPerNode.Add(nodePerValue[n2], []);
            }
            neighborsPerNode[nodePerValue[n1]].Add(nodePerValue[n2]);
        }
        public void uniDirectionalDisconnect(T n1, T n2) 
        {
            if (!nodePerValue.ContainsKey(n1) || !nodePerValue.ContainsKey(n2))
            {
                return;
            }
            var node1 = nodePerValue[n1];
            var node2 = nodePerValue[n2];
            neighborsPerNode[node1].Remove(node2);
            if (neighborsPerNode[node1].Count == 0)
            {
                neighborsPerNode.Remove(node1);
                nodePerValue.Remove(n1);
            }
        }

        public Dictionary<T,Dictionary<T, bool>> getAdjencyMatrice() 
        {
            Dictionary<T, Dictionary<T, bool>> adjencyMap = [];
            foreach(var v in nodePerValue.Keys)
            {
                adjencyMap.Add(v, new Dictionary<T, bool>());
                foreach (var node in nodePerValue.Keys)
                {
                    adjencyMap[v].Add(node, neighborsPerNode[nodePerValue[v]].Contains(nodePerValue[node]));
                }
            }
            return adjencyMap;
        }

        public Dictionary<T, HashSet<T>> getAdjencyList() 
        {
            Dictionary<T, HashSet<T>> adjencyList = [];
            foreach (var v in nodePerValue.Keys) 
            {
                adjencyList.Add(v, [.. neighborsPerNode[nodePerValue[v]].Select(x=>x.Value)]);
            }
            return adjencyList;
        }

        /*
         n^2 complexity
         */
        public static Dictionary<T, HashSet<T>> convertToAdjacencyList(Dictionary<T, Dictionary<T, bool>> adjacencyMatrix) 
        {
            Dictionary<T, HashSet<T>> adjacencyList = new Dictionary<T, HashSet<T>>();
            foreach (var node in adjacencyMatrix.Keys) 
            {
                if (!adjacencyList.ContainsKey(node)) 
                {
                    adjacencyList.Add(node, new HashSet<T>());
                }
                foreach (var neighbor in adjacencyMatrix.Keys) 
                {
                    if (adjacencyMatrix[node][neighbor]) 
                    {
                        adjacencyList[node].Add(neighbor);
                    }
                }
            }
            return adjacencyList;
        }
        /*
         n*m complexity, n vertex, m edges
         */
        public static Dictionary<T, Dictionary<T, int>> convertToIncidenceMatrix(Dictionary<T, HashSet<T>> adjacencyList) 
        {
            Dictionary<T, Dictionary<T, int>> incidenceMatrix = new Dictionary<T, Dictionary<T, int>>();
            var edges = adjacencyList.SelectMany(x => x.Value).ToHashSet();
            foreach (var node in adjacencyList.Keys) 
            {
                if (!incidenceMatrix.ContainsKey(node)) 
                {
                    incidenceMatrix.Add(node, []);
                }
                foreach (var edge in edges) 
                {
                    incidenceMatrix[node].Add(edge, adjacencyList[node].Contains(edge) ? 1: 0);
                }
            }
            return incidenceMatrix;
        }
        /*
         n*m, n nb of vertice, m nb of edges
         */
        public static Dictionary<T, HashSet<T>> convertToAdjacencyList(Dictionary<T, Dictionary<T, int>> incidenceMatrix)
        {
            Dictionary<T, HashSet<T>> adjacencyList = [];
            foreach (var node in incidenceMatrix.Keys) 
            {
                if (!adjacencyList.ContainsKey(node))
                {
                    adjacencyList.Add(node, []);
                }
                foreach (var edge in incidenceMatrix[node].Keys) 
                {
                    if (incidenceMatrix[node][edge] != 0) 
                    {
                        adjacencyList[node].Add(edge);
                    }
                }
            }
            return adjacencyList;
        }

        public List<Tuple<T, T>> getEdges() 
        {
            List<Tuple<T, T>> edges = new List<Tuple<T, T>>();
            foreach (var v in neighborsPerNode.Keys)
            {
                foreach (var n in neighborsPerNode[v]) 
                {
                    edges.Add(new Tuple<T, T>(v.Value, n.Value));
                }
            }
            return edges;
        }

        public List<T> getVertices() 
        {
            List<T> vertices = new List<T>();
            foreach(var key in nodePerValue.Keys)
            {
                if (nodePerValue[key] != null) 
                {
                    vertices.Add(key);
                }
            }
            return vertices;
        }

       public HashSet<T> getNeighbors(T n) 
        {
            return neighborsPerNode[nodePerValue[n]].Select(x => x.Value).ToHashSet();
        }

        public bool areNodeConnected(T n1, T n2) 
        {
            if (nodePerValue.ContainsKey(n1) && nodePerValue.ContainsKey(n2)) 
            {
                return neighborsPerNode[nodePerValue[n1]].Contains(nodePerValue[n2]) 
                    || neighborsPerNode[nodePerValue[n2]].Contains(nodePerValue[n1]);
            }
            return false;
        }
        private GraphNode<T> createNode(T n1)
        {
            var node = new GraphNode<T>(n1);
            node.setAfterVisitAction(afterVisitAction);
            node.setVisitAction(visitAction);
            node.setBeforeVisitAction(beforeVisitAction);
            return node;
        }

        public Graph<T> withBeforeVisitAction(Action<T> beforeVisitAction)
        {
            this.beforeVisitAction = beforeVisitAction;
            return this;
        }
        public Graph<T> withVisitAction(Action<T> action)
        {
            this.visitAction = action;
            return this;
        }
        public Graph<T> withAfterVisitAction(Action<T> afterVisitAction)
        {
            this.afterVisitAction = afterVisitAction;
            return this;
        }
        
        public int computeChromaticNumberGreedy() 
        {
            Dictionary<GraphNode<T>, int> colorByNode = [];
            HashSet<int> generatedColors = [];
            foreach (var node in nodePerValue.Values) 
            {
                HashSet<int> neighborsColors = new HashSet<int>();
                foreach (var neighbor in neighborsPerNode[node]) 
                {
                    if (colorByNode.ContainsKey(neighbor)) 
                    {
                        neighborsColors.Add(colorByNode[neighbor]);
                    }
                }
                var availableColors = generatedColors.Where(x => !neighborsColors.Contains(x));
                if (availableColors.Any())
                {
                    colorByNode.Add(node, availableColors.Min());
                }
                else 
                {
                    colorByNode.Add(node, generatedColors.Count+1);
                }
                generatedColors.Add(colorByNode[node]);
            }
            return generatedColors.Count;
        }
        public int getDegree(T n) 
        {
            if (!nodePerValue.ContainsKey(n)) 
            {
                return 0;
            }
            var node = nodePerValue[n];
            if (!neighborsPerNode.ContainsKey(node)) 
            {
                return 0;
            }
            return neighborsPerNode[node].Count;
        }

        public bool isBipartite(out Dictionary<T, int> colorByNode) 
        {
            // 0 red, 1 blue
            colorByNode = [];
            Queue<T> q = [];
            foreach (var root in getRoots())
            {
                q.Enqueue(root);
                colorByNode.Add(root, 0);
            }
            while (q.Count > 0) 
            {
                var node = q.Dequeue();
                var childColor = colorByNode[node] == 0 ? 1 : 0;
                foreach (var item in getNeighbors(node))
                {
                    bool childSeen = colorByNode.ContainsKey(item);
                    if (childSeen && colorByNode[item] != childColor) 
                    {
                        return false;
                    }
                    if (!childSeen) 
                    {
                        colorByNode.Add(item, childColor);
                        q.Enqueue(item);
                    }
                }
            }
            return true;
        }

        public IEnumerable<T> getRoots() 
        {
            int maxDegree = getVertices().Select(x => getDegree(x)).Max();
            return getVertices().Where(x => getDegree(x) == maxDegree || getDegree(x) == 0);
        }
        public Graph<T> computeMaximumInducedSubgraph(int minDegree) 
        {
            var graph = new Graph<T>(this);
            bool inspectGraph = true;
            while (inspectGraph) 
            {
                inspectGraph = false;
                foreach (var item in graph.getRoots())
                {
                    Queue<T> q = [];
                    q.Enqueue(item);
                    while (q.Count > 0)
                    {
                        var node = q.Dequeue();
                        var nodeNeighbors = graph.getNeighbors(node);
                        bool breakLink = graph.getDegree(node) < minDegree;
                        foreach (var item1 in nodeNeighbors)
                        {
                            q.Enqueue(item1);
                            if (breakLink)
                            {
                                graph.biDirectionalDisconnect(node, item1);
                                inspectGraph = true;
                            }
                        }
                    }
                }
            }
            return graph;

        }
    }
}
