using skiena.datastructures.graph.basegraph;
using skiena.datastructures.graph.interfaces;

namespace skiena.datastructures.graph
{
    public class UndirectedGraph<T> : BaseUndirectedGraph<T>, IUnweightedGraph<T> where T : IEquatable<T>, IComparable<T>
    {
        public UndirectedGraph():base()
        {
        }

        public UndirectedGraph(Graph<T> graph) :base(graph)
        {
        }

        public IUnweightedGraph<T> connect(T n1, T n2)
        {
            connectImpl(n1, n2);
            return this;
        }

        public IUnweightedGraph<T> disconnect(T n1, T n2)
        {
            disconnectImpl(n1, n2);
            return this;
        }
    }
}
