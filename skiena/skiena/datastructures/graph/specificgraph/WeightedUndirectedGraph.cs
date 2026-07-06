using skiena.datastructures.graph.basegraph;
using skiena.datastructures.graph.interfaces;
using skiena.datastructures.graph.specificgraph;

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


        public void setWeight(T n1, T n2, U weight)
        {
            if (weightsByEdge.ContainsKey(n1) && weightsByEdge[n1].ContainsKey(n2))
            {
                weightsByEdge[n1][n2] = weight;
            }
        }

        public WeightedUndirectedGraph<T, U> primMinimumSpanningTree(T start)
        {
            PriorityQueue<Tuple<T, T>, U> queue = new();
            WeightedUndirectedGraph<T, U> mst = new();
            MyDisjointSet<T> disjointSet = new();

            if (nodePerValue.ContainsKey(start))
            {
                foreach (var n in getNeighbors(start))
                {
                    queue.Enqueue(new Tuple<T, T>(start, n), weightsByEdge[start][n]);
                }
                while (queue.Count > 0)
                {
                    Tuple<T, T> edge = queue.Dequeue();
                    if (disjointSet.areConnected(edge.Item1, edge.Item2)) 
                    {
                        continue;
                    }
                    if (!disjointSet.contains(edge.Item1)) 
                    {
                        disjointSet.insert(edge.Item1);
                    }
                    if (!disjointSet.contains(edge.Item2))
                    {
                        disjointSet.insert(edge.Item2);
                    }
                    disjointSet.connect(edge.Item1, edge.Item2);
                    mst.connect(edge.Item1, edge.Item2, weightsByEdge[edge.Item1][edge.Item2]);
                    foreach (var n in getNeighbors(edge.Item2)) 
                    {
                        queue.Enqueue(new Tuple<T,T>(edge.Item2, n), weightsByEdge[edge.Item2][n]);
                    }
                }
            }
            return mst;
        }

        public WeightedUndirectedGraph<T, U> kruskalMinimumSpanningTree() 
        {
            PriorityQueue<Tuple<T, T>, U> queue = new();
            WeightedUndirectedGraph<T, U> mst = new();
            MyDisjointSet<T> disjointSet = new();
            
            var orderedEdges = getEdges().OrderBy(x => weightsByEdge[x.Item1][x.Item2]).ToList();
            foreach(var edge in orderedEdges) 
            {
                if (disjointSet.areConnected(edge.Item1, edge.Item2))
                {
                    continue;
                }
                if (!disjointSet.contains(edge.Item1))
                {
                    disjointSet.insert(edge.Item1);
                }
                if (!disjointSet.contains(edge.Item2))
                {
                    disjointSet.insert(edge.Item2);
                }
                disjointSet.connect(edge.Item1, edge.Item2);
                mst.connect(edge.Item1, edge.Item2, weightsByEdge[edge.Item1][edge.Item2]);
            }
            return mst;
        }

    }
}
