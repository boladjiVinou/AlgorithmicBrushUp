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
        private Action<T> beforeVisitAction = (v) => { };
        private Action<T> afterVisitAction = (v) => { };
        private Action<T> visitAction = (v) => { };

        public void connect(T n1, T n2) 
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

    }
}
