using skiena.datastructures.graph.basegraph;
using skiena.datastructures.graph.interfaces;

namespace skiena.datastructures.graph
{
    public class WeightedUndirectedGraph<T, U> : BaseUndirectedGraph<T>, IWeightedGraph<T,U>
        where T : IEquatable<T>, IComparable<T>
        where U : IEquatable<U>, IComparable<U>
    {
        private Dictionary<T, Dictionary<T, U>> weightsByEdge = [];
        public IWeightedGraph<T,U> connect(T n1, T n2, U weight)
        {
            if (!weightsByEdge.ContainsKey(n1)) 
            {
                weightsByEdge.Add(n1, new Dictionary<T, U>());
            }
            if (!weightsByEdge.ContainsKey(n2))
            {
                weightsByEdge.Add(n2, new Dictionary<T, U>());
            }
            weightsByEdge[n1][n2] = weight;
            weightsByEdge[n2][n1] = weight;
            return this;
        }

        public IWeightedGraph<T,U> disconnect(T n1, T n2)
        {
            if (weightsByEdge.ContainsKey(n1))
            {
                weightsByEdge[n1].Remove(n2);
            }
            if (weightsByEdge.ContainsKey(n2))
            {
                weightsByEdge[n2].Remove(n1);
            }
            return this;
        }

    }
}
