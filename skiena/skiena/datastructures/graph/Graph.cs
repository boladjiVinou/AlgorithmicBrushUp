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
        Dictionary<T,GraphNode<T>> nodePerValue = new Dictionary<T, GraphNode<T>>();
        Dictionary<GraphNode<T>, HashSet<GraphNode<T>>> neighborsPerNode = new Dictionary<GraphNode<T>, HashSet<GraphNode<T>>>();

        public void connect(T n1, T n2) 
        {
            if (!nodePerValue.ContainsKey(n1)) 
            {
                nodePerValue.Add(n1, new GraphNode<T>(n1));
                neighborsPerNode.Add(nodePerValue[n1], new HashSet<GraphNode<T>>());
            }
            if (!nodePerValue.ContainsKey(n2)) 
            {
                nodePerValue.Add(n2, new GraphNode<T>(n2));
                neighborsPerNode.Add(nodePerValue[n2], new HashSet<GraphNode<T>>());
            }
            neighborsPerNode[nodePerValue[n1]].Add(nodePerValue[n2]);
            neighborsPerNode[nodePerValue[n2]].Add(nodePerValue[n1]);
        }

        public Dictionary<T,Dictionary<T, bool>> getAdjencyMatrice() 
        {
            Dictionary<T, Dictionary<T, bool>> adjencyMap = new Dictionary<T, Dictionary<T, bool>>();
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
            Dictionary<T, HashSet<T>> adjencyList = new Dictionary<T, HashSet<T>>();
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

    }
}
