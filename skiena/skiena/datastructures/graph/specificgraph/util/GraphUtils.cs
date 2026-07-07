using skiena.datastructures.graph.interfaces;
using System.Numerics;

namespace skiena.datastructures.graph.specificgraph.util
{
    public class GraphUtils<T, U>
        where T : IEquatable<T>, IComparable<T>
        where U : INumber<U>, IMinMaxValue<U>, INumberBase<U>, IAdditionOperators<U,U,U>
    {
        public static IWeightedGraph<T, U> dijkstraShortestPath(IWeightedGraph<T, U> graph, T source, T destination, out U finalDistance)
        {
            Dictionary<T,U> distanceByNode = [];
            HashSet<T> visited = new();
            Dictionary<T, T> parentByNode = new();
            foreach (var v in graph.getVertices())
            {
                distanceByNode.Add(v, U.MaxValue);
            }
            distanceByNode[source] = U.Zero;



            PriorityQueue<T, U> q = new();
            q.Enqueue(source, U.Zero);
            while(q.Count> 0) 
            {
                if (q.TryDequeue(out T curr, out U distance) && !visited.Contains(curr))
                {
                    if (distanceByNode[curr] < distance) 
                    {
                        continue;
                    }
                    visited.Add(curr);
                    foreach (var n in graph.getNeighbors(curr).Where(x => !visited.Contains(x))) 
                    {
                        U tmpWeight = distanceByNode[curr] + graph.getWeight(curr, n);
                        if (distanceByNode[n].CompareTo(tmpWeight) > 0) 
                        {
                            distanceByNode[n] = tmpWeight;
                            q.Enqueue(n, tmpWeight);
                        }
                    }
                }
            }



            finalDistance = distanceByNode[destination];
            if (distanceByNode[destination] == U.MaxValue) 
            {
                return new WeightedDirectedGraph<T, U>();
            }


            WeightedDirectedGraph<T, U> path = new WeightedDirectedGraph<T, U>();
            bool pathFound = false;
            T tmpCurr = destination;
            while (!pathFound) 
            {
                T parent = parentByNode[tmpCurr];
                path.connect(parent,tmpCurr, graph.getWeight(parent,tmpCurr));
                tmpCurr = parent;
                pathFound = tmpCurr.Equals( source);
            }
            return path;
        }
    }
    
}
