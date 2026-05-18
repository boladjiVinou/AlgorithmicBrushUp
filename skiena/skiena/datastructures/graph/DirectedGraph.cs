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
            }
            if (!nodePerValue.ContainsKey(n2))
            {
                nodePerValue.Add(n2, createNode(n2));
            }
            nodePerValue[n1].connectTo(nodePerValue[n2]);
        }
        public override void disconnect(T n1, T n2)
        {
            if (!nodePerValue.ContainsKey(n1) || !nodePerValue.ContainsKey(n2))
            {
                return;
            }
            var node1 = nodePerValue[n1];
            var node2 = nodePerValue[n2];
            node1.disconnectFrom(node2);
            if(node1.getInDegree() == 0 && node1.getOutDegree() == 0)
            {
                nodePerValue.Remove(n1);
            }
            if (node2.getInDegree() == 0 && node2.getOutDegree() == 0)
            {
                nodePerValue.Remove(n2);
            }
        }

        public override IEnumerable<T> getPossibleRoots()
        {
            return nodePerValue.Values.Where(x => x.getInDegree() == 0).Select(x => x.Value);
        }

    }
}
