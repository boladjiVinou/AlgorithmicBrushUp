using skiena.datastructures.graph.basegraph;
using skiena.datastructures.graph.interfaces;

namespace skiena.datastructures.graph.specificgraph
{
    public class WeightedDirectedGraph<T,U> : BaseDirectedGraph<T>, IWeightedGraph<T, U>
        where T : IEquatable<T>, IComparable<T>
        where U : IEquatable<U>, IComparable<U>
    {
        private Dictionary<T, Dictionary<T, U>> weightsByEdge = [];


        public WeightedDirectedGraph() { }

        public WeightedDirectedGraph(WeightedDirectedGraph<T, U> graph) : base(graph)
        {
            foreach (var v in graph.weightsByEdge.Keys)
            {
                if (weightsByEdge.ContainsKey(v))
                {
                    weightsByEdge.Add(v, []);
                }
                foreach (var w in graph.weightsByEdge[v].Keys) 
                {
                    weightsByEdge[v].Add(w, graph.getWeight(v, w));
                }
            }
        }
        public IWeightedGraph<T, U> connect(T n1, T n2, U weight)
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

        public bool contains(T node)
        {
            return nodePerValue.ContainsKey(node) && nodePerValue[node] != null;
        }

        public IWeightedGraph<T, U> disconnect(T n1, T n2)
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

        public U getWeight(T n1, T n2)
        {
            return weightsByEdge[n1][n2];
        }

        public void setWeight(T n1, T n2, U weight)
        {
            if (weightsByEdge.ContainsKey(n1) && weightsByEdge[n1].ContainsKey(n2))
            {
                weightsByEdge[n1][n2] = weight;
            }
        }
    }
}
