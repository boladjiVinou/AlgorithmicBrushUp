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
    public abstract class Graph<T> where T : IEquatable<T>
    {
        protected Dictionary<T,GraphNode<T>> nodePerValue = [];

        public Graph() 
        {
        }
        public Graph(Graph<T> graph) 
        {
            nodePerValue = new Dictionary<T,GraphNode<T>>(graph.nodePerValue);
        }

        public abstract void connect(T n1, T n2);
        public abstract void disconnect(T n1, T n2);


        public Dictionary<T,Dictionary<T, bool>> getAdjencyMatrice() 
        {
            Dictionary<T, Dictionary<T, bool>> adjencyMap = [];
            foreach(var v in nodePerValue.Keys)
            {
                adjencyMap.Add(v, new Dictionary<T, bool>());
                var nodeNeighbors = nodePerValue[v].getNeighbors();
                foreach (var node in nodePerValue.Keys)
                {
                    adjencyMap[v].Add(node, nodeNeighbors.Contains(nodePerValue[node]));
                }
            }
            return adjencyMap;
        }

        public Dictionary<T, HashSet<T>> getAdjencyList() 
        {
            Dictionary<T, HashSet<T>> adjencyList = [];
            foreach (var v in nodePerValue.Keys) 
            {
                adjencyList.Add(v, [.. nodePerValue[v].getNeighbors().Select(x=>x.Value)]);
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
            foreach (var v in nodePerValue.Keys)
            {
                foreach (var n in nodePerValue[v].getNeighbors()) 
                {
                    edges.Add(new Tuple<T, T>(v, n.Value));
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
            return nodePerValue[n].getNeighbors().Select(x => x.Value).ToHashSet();
        }

        public bool areNodeConnected(T n1, T n2) 
        {
            if (nodePerValue.ContainsKey(n1) && nodePerValue.ContainsKey(n2)) 
            {
                return nodePerValue[n1].isConnectedTo(nodePerValue[n2]) 
                    || nodePerValue[n2].isConnectedTo(nodePerValue[n1]);
            }
            return false;
        }
        protected GraphNode<T> createNode(T n1)
        {
            var node = new GraphNode<T>(n1);
            return node;
        }
        
        public int computeChromaticNumberGreedy() 
        {
            Dictionary<GraphNode<T>, int> colorByNode = [];
            HashSet<int> generatedColors = [];
            foreach (var node in nodePerValue.Values) 
            {
                HashSet<int> neighborsColors = new HashSet<int>();
                foreach (var neighbor in node.getNeighbors()) 
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
        public virtual int getDegree(T n)
        {
            if (!nodePerValue.ContainsKey(n))
            {
                return 0;
            }
            var node = nodePerValue[n];
            return node.getInDegree();
        }
        public bool isBipartite(out Dictionary<T, int> colorByNode) 
        {
            // 0 red, 1 blue
            colorByNode = [];
            Queue<T> q = [];
            foreach (var root in getPossibleRoots())
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

        public abstract IEnumerable<T> getPossibleRoots();
        public IEnumerable<T> dfs() 
        {
            Stack<T> visitStack = [];
            if (nodePerValue.Count > 0) 
            { }
            return Enumerable.Empty<T>();
        }

        public bool containsArborescence()
        {

            return false;
        }
    }
}
