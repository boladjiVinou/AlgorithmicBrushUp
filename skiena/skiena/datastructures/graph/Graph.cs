using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class Graph<T> where T : IEquatable<T>
    {
        Dictionary<T,GraphNode<T>> nodePerValue = [];
        Dictionary<GraphNode<T>, HashSet<GraphNode<T>>> neighborsPerNode = [];
        HashSet<GraphNode<T>> roots = new HashSet<GraphNode<T>>();
        private Action<T> beforeVisitAction = (v) => { };
        private Action<T> afterVisitAction = (v) => { };
        private Action<T> visitAction = (v) => { };

        public Graph() 
        {
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
            removeFromRoots(n1);
            removeFromRoots(n2);
        }

        private void removeFromRoots(T n1)
        {
            if (roots.Contains(nodePerValue[n1]))
            {
                roots.Remove(nodePerValue[n1]);
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
            removeFromRoots(n2);
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
                adjencyList.Add(v, neighborsPerNode[nodePerValue[v]].Select(x=>x.Value).ToHashSet());
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

        private GraphNode<T> createNode(T n1)
        {
            var node = new GraphNode<T>(n1);
            node.setAfterVisitAction(afterVisitAction);
            node.setVisitAction(visitAction);
            node.setBeforeVisitAction(beforeVisitAction);
            roots.Add(node);
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
        public IEnumerable<T> postOrderIteration() 
        {
            foreach (var root in roots) 
            {
                yield break;
            }
        }
        public IEnumerable<T> inOrderIteration()
        {
            foreach (var root in roots)
            {
                yield break;
            }
        }
        public IEnumerable<T> preOrderIteration()
        {
            foreach (var root in roots)
            {
                yield break;
            }
        }
    }
}
