using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class UndirectedGraph<T> : Graph<T> where T : IEquatable<T>
    {
        public UndirectedGraph() : base()
        {
        }
        public UndirectedGraph(Graph<T> graph):base(graph)
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
            neighborsPerNode[nodePerValue[n2]].Add(nodePerValue[n1]);
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
            neighborsPerNode[node2].Remove(node1);
            if (neighborsPerNode[node1].Count == 0)
            {
                neighborsPerNode.Remove(node1);
                nodePerValue.Remove(n1);
            }
            if (neighborsPerNode[node2].Count == 0)
            {
                neighborsPerNode.Remove(node2);
                nodePerValue.Remove(n2);
            }
        }

        public UndirectedGraph<T> computeMaximumInducedSubgraph(int minDegree)
        {
            var graph = new UndirectedGraph<T>(this);
            bool inspectGraph = true;
            while (inspectGraph)
            {
                inspectGraph = false;
                foreach (var item in graph.getRoots())
                {
                    Queue<T> q = [];
                    q.Enqueue(item);
                    while (q.Count > 0)
                    {
                        var node = q.Dequeue();
                        var nodeNeighbors = graph.getNeighbors(node);
                        bool breakLink = graph.getDegree(node) < minDegree;
                        foreach (var item1 in nodeNeighbors)
                        {
                            q.Enqueue(item1);
                            if (breakLink)
                            {
                                graph.disconnect(node, item1);
                                inspectGraph = true;
                            }
                        }
                    }
                }
            }
            return graph;

        }

        public override IEnumerable<T> getRoots()
        {
            int maxDegree = getVertices().Select(x => getDegree(x)).Max();
            return getVertices().Where(x => getDegree(x) == maxDegree || getDegree(x) == 0);
        }

        public override int getDegree(T n)
        {
            if (!nodePerValue.ContainsKey(n))
            {
                return 0;
            }
            var node = nodePerValue[n];
            if (neighborsPerNode.ContainsKey(node))
            {
                return 0;
            }
            return neighborsPerNode[node].Count;
        }

    }
}
