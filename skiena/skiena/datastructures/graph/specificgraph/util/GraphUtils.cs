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
            HashSet<T> visited = [];
            Dictionary<T, T> parentByNode = [];
            foreach (var v in graph.getVertices())
            {
                distanceByNode.Add(v, U.MaxValue);
                parentByNode.Add(v, v);
            }
            if (!distanceByNode.ContainsKey(destination)) 
            {
                finalDistance = U.MaxValue;
                return new WeightedDirectedGraph<T, U>();
            }
            foreach(var e in graph.getEdges()) 
            {
                if(graph.getWeight(e.Item1,e.Item2).CompareTo(U.Zero) < 0) 
                {
                    finalDistance = U.MaxValue;
                    return new WeightedDirectedGraph<T, U>();
                }
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
                            parentByNode[n] = curr;
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


            WeightedDirectedGraph<T, U> path = new();
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
        public static IWeightedGraph<T, U> bellmanFordShortestPath(IWeightedGraph<T, U> graph, T start, T end, out U finalDistance)
        {
            Dictionary<T, U> distanceByNode = [];
            Dictionary<T, T> parentByNode = [];
            foreach (var v in graph.getVertices()) 
            {
                distanceByNode.Add(v, U.MaxValue);
                parentByNode.Add(v, v);
            }
            if (!distanceByNode.ContainsKey(end)) 
            {
                finalDistance = U.MaxValue;
                return new WeightedDirectedGraph<T, U>();
            }

            int nVertices = distanceByNode.Keys.Count;
            distanceByNode[start] = U.Zero;
            for (int i =0;i<nVertices-1;i++)
            {
                foreach (var edge in graph.getEdges())
                {
                    if (distanceByNode[edge.Item1] == U.MaxValue)
                    {
                        continue;
                    }
                    U newDistance = distanceByNode[edge.Item1] + graph.getWeight(edge.Item1, edge.Item2);
                    if(distanceByNode[edge.Item2].CompareTo(newDistance) > 0) 
                    {
                        distanceByNode[edge.Item2] = newDistance;
                        parentByNode[edge.Item2] = edge.Item1;
                    }
                }
            }
            // negative cycle detection
            foreach (var edge in graph.getEdges())
            {
                U newDistance = distanceByNode[edge.Item1] + graph.getWeight(edge.Item1, edge.Item2);
                if (distanceByNode[edge.Item2].CompareTo(newDistance) > 0)
                {
                    distanceByNode[end] = U.MaxValue;
                    break;
                }
            }


            finalDistance = distanceByNode[end];
            if (distanceByNode[end] == U.MaxValue)
            {
                return new WeightedDirectedGraph<T, U>();
            }


            WeightedDirectedGraph<T, U> path = new();
            bool pathFound = false;
            T tmpCurr = end;
            while (!pathFound)
            {
                T parent = parentByNode[tmpCurr];
                if(!parent.Equals( tmpCurr))
                {
                    path.connect(parent, tmpCurr, graph.getWeight(parent, tmpCurr));
                }
        
                tmpCurr = parent;
                pathFound = tmpCurr.Equals(start);
            }
            return path;
        }
        public static Dictionary<T, Dictionary<T, T>> computeShortestPathBetweenAllPair(IWeightedGraph<T, U> graph, out Dictionary<T, Dictionary<T, U>> distanceByPair) 
        {
            distanceByPair = [];
            Dictionary<T, Dictionary<T, T>> ancestry = [];

            var vertices = graph.getVertices();
            foreach (var v in vertices)
            {
                if (!distanceByPair.ContainsKey(v))
                {
                    distanceByPair.Add(v, new());
                }
                if (!distanceByPair[v].ContainsKey(v))
                {
                    distanceByPair[v].Add(v, U.Zero);
                }
                foreach (var w in vertices)
                {
                    if (!distanceByPair[v].ContainsKey(w))
                    {
                        distanceByPair[v].Add(w, U.MaxValue);
                    }
                }
            }
            foreach (var edge in graph.getEdges()) 
            {
                distanceByPair[edge.Item1][edge.Item2] = graph.getWeight(edge.Item1, edge.Item2);
            }

            foreach (var v1 in vertices)
            {
                foreach(var v2 in vertices)  
                {
                    foreach(var v3 in vertices) 
                    {
                        if (v2.Equals(v3) || distanceByPair[v2][v1] == U.MaxValue || distanceByPair[v1][v3] == U.MaxValue)
                        {
                            continue;
                        }
                        var tmp = distanceByPair[v2][v1]  + distanceByPair[v1][v3];
                        if (distanceByPair[v2][v3].CompareTo(tmp) > 0) 
                        {
                            distanceByPair[v2][v3] = tmp;
                            if (!ancestry.ContainsKey(v2)) 
                            {
                                ancestry.Add(v2, []);
                            }
                            if (!ancestry[v2].ContainsKey(v3)) 
                            {
                                ancestry[v2].Add(v3, v1);
                            }
                            else 
                            {
                                ancestry[v2][v3] = v1;
                            }
                        }
                    }
                }
            }
            // negative cycle detection
            foreach (var v1 in vertices)
            {
                foreach (var v2 in vertices)
                {
                    foreach (var v3 in vertices)
                    {
                        if (v2.Equals(v3) || distanceByPair[v2][v1] == U.MaxValue || distanceByPair[v1][v3] == U.MaxValue)
                        {
                            continue;
                        }
                        var tmp = distanceByPair[v2][v1] + distanceByPair[v1][v3];
                        if (distanceByPair[v2][v3].CompareTo(tmp) > 0)
                        {
                            distanceByPair = [];
                            return [];
                        }
                    }
                }
            }

            return ancestry;

        }

        public U computeMaximumFlow(WeightedDirectedGraph<T, U> graph, T start, T end)
        {
            if (!graph.contains(start) || !graph.contains(end)) 
            {
                return U.MinValue;
            }
            WeightedDirectedGraph<T, U> residual = new(graph);
            WeightedDirectedGraph<T, U> network = new(graph);

            foreach (var v in network.getVertices()) 
            {
                foreach (var w in network.getNeighbors(v)) 
                {
                    network.setWeight(v, w, U.Zero);
                }
            }
            var path = searchPath(residual, start, end, out U flow);
            while (flow > U.Zero) 
            {
                for (int i = 1; i < path.Count; i++)
                {
                    T u = path[i - 1];
                    T v = path[i];
                    var prevFlow = network.getWeight(u, v);

                    network.setWeight(u, v, prevFlow + flow);
                    residual.setWeight(u, v, residual.getWeight(u, v) - flow);

                    if (!residual.areNodeConnected(v,u)) 
                    {
                        residual.connect(v, u, -flow);
                    }
                    else 
                    {
                        residual.setWeight(v,u, residual.getWeight(v, u) - flow);
                    }
                }

                path = searchPath(residual, start, end, out flow);
            }

            U maxFlow = U.Zero;
            foreach (var edge in graph.getEdges().Where(x => x.Item2.Equals(end))) 
            {
                maxFlow += network.getWeight(edge.Item1, edge.Item2);
            }
            return maxFlow;
        }
        private List<T> searchPath(WeightedDirectedGraph<T, U> graph, T src, T dst, out U flow)
        {
            Queue<Node> queue = [];
            queue.Enqueue(new( src,default, U.MaxValue));
            HashSet<T> visited = [];
            Node destinationNode = null;
            while(queue.Count > 0)
            {
                var curr = queue.Dequeue();
                visited.Add(curr.value);
                if (curr.value.Equals(dst))
                {
                    destinationNode = curr;
                    break;
                }
                foreach (var v in graph.getNeighbors(curr.value)) 
                {
                    if (visited.Contains(v) || graph.getWeight(curr.value, v) == U.Zero) 
                    {
                        continue;
                    }

                    queue.Enqueue(new(v,curr,U.Min(curr.flow, graph.getWeight(curr.value, v))));
                }
            }
            List<T> path = [];
            if (destinationNode != null) 
            {
                flow = destinationNode.flow;
            }
            else 
            {
                flow = U.Zero;
            }
            while (destinationNode != null)
            {
                path.Add(destinationNode.value);
                destinationNode = destinationNode.previous;
            }
            path.Reverse();
            return path;
        }
        private class Node
        {
            public T value { get; set; }
            public Node previous { get; }
            public U flow { get; set; }
            public Node(T value, Node prev, U flow) 
            {
                previous = prev;
                this.value = value;
                this.flow = flow;
            }
        }
    }
    
}
