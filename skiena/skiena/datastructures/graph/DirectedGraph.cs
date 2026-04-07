using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class DirectedGraph<T> : Graph<T> where T : IEquatable<T>
    {
        protected Dictionary<GraphNode<T>, HashSet<GraphNode<T>>> reversedLinks = [];

        public DirectedGraph() : base()
        {
        }
        public DirectedGraph(Graph<T> graph) : base(graph)
        {
        }
        public override void connect(T n1, T n2)
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
        public override void disconnect(T n1, T n2)
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

        public override IEnumerable<T> getRoots()
        {
            var neighbors = neighborsPerNode.Values.SelectMany(x =>x).Select(x=>x.Value).ToHashSet();
            return getVertices().Where(x => !neighbors.Contains(x));
        }

        public override int getDegree(T n)
        {
            if (!nodePerValue.ContainsKey(n))
            {
                return 0;
            }
            var node = nodePerValue[n];

            return neighborsPerNode.Values
                .Where(x => x.Contains(node))
                .Count();
        }


    }
}
