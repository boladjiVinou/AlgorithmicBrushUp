using skiena.datastructures.graph.basegraph;
using skiena.datastructures.graph.interfaces;

namespace skiena.datastructures.graph.specificgraph
{
    public class DirectedGraph<T> : BaseDirectedGraph<T>, IUnweightedGraph<T> where T : IEquatable<T>
    {
        public DirectedGraph()
        {
        }

        public DirectedGraph(Graph<T> graph):base(graph)
        {
        }


        public IUnweightedGraph<T> connect(T n1, T n2)
        {
            connectImpl(n1, n2);
            return this;
        }

        public bool contains(T node)
        {
            return this.nodePerValue.ContainsKey(node) && nodePerValue[node] != null;
        }

        public IUnweightedGraph<T> disconnect(T n1, T n2)
        {
            disconnectImpl(n1, n2);
            return this;
        }
    }
}
