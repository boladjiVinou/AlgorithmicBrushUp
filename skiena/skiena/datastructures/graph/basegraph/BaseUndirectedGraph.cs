namespace skiena.datastructures.graph.basegraph
{
    public class BaseUndirectedGraph<T> : Graph<T> where T : IEquatable<T>, IComparable<T>
    {
        public BaseUndirectedGraph() : base()
        {
        }
        public BaseUndirectedGraph(Graph<T> graph) : base(graph)
        {
            foreach (var item in nodePerValue.Values)
            {
                foreach (var neighbor in graph.getNeighbors(item.Value))
                {
                    connectImpl(item.Value, neighbor);
                }
            }
        }
        protected override BaseUndirectedGraph<T> connectImpl(T n1, T n2)
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
            nodePerValue[n2].connectTo(nodePerValue[n1]);
            return this;
        }


        protected override BaseUndirectedGraph<T> disconnectImpl(T n1, T n2)
        {
            if (!nodePerValue.ContainsKey(n1) || !nodePerValue.ContainsKey(n2))
            {
                return this;
            }
            var node1 = nodePerValue[n1];
            var node2 = nodePerValue[n2];
            node1.disconnectFrom(node2);
            node2.disconnectFrom(node1);

            if (node1.getOutDegree() == 0 && node1.getInDegree() == 0)
            {
                nodePerValue.Remove(n1);
            }
            if (node2.getOutDegree() == 0 && node2.getInDegree() == 0)
            {
                nodePerValue.Remove(n2);
            }
            return this;
        }

        public int computeChromaticNumberGreedy()
        {
            return computeChromaticNumberGreedy(this);
        }
        public BaseUndirectedGraph<T> computeMaximumInducedSubgraph(int minDegree)
        {
            var graph = new BaseUndirectedGraph<T>(this);
            bool inspectGraph = true;
            while (inspectGraph)
            {
                inspectGraph = false;
                foreach (var item in graph.getPossibleCommonRoot())
                {
                    Queue<T> q = [];
                    q.Enqueue(item);
                    while (q.Count > 0)
                    {
                        var node = q.Dequeue();
                        var nodeNeighbors = graph.getNeighbors(node);
                        bool breakLink = graph.getInDegree(node) < minDegree;
                        foreach (var item1 in nodeNeighbors)
                        {
                            q.Enqueue(item1);
                            if (breakLink)
                            {
                                graph.disconnectImpl(node, item1);
                                inspectGraph = true;
                            }
                        }
                    }
                }
            }
            return graph;

        }

        public List<T> getHamiltonianPath()
        {
            var nodes = nodePerValue.Values.Where(x => x != null).ToList();
            foreach (var start in nodes)
            {
                var pathFinder = new HamiltonianPathFinder<T>(nodes.Count);
                start.accept(pathFinder);
                if (pathFinder.hasFoundAPath())
                {
                    return [.. pathFinder.getPath().Select(x => x.Value)];
                }
            }
            return [];
        }

        public override IEnumerable<T> getPossibleCommonRoot()
        {
            var dfsVisitor = new DepthFirstSearchVisitor<T>();
            var nodes = nodePerValue.Values.Where(x => x != null);
            GraphNode<T>? startNode = null;
            if (nodes.Any())
            {
                startNode = nodes.First();
                startNode.accept(dfsVisitor);
            }

            var lastVisitedNode = dfsVisitor.getLastNodeVisited();
            if (lastVisitedNode != null && startNode != null && dfsVisitor.getNbOfVisitedNode() == nodes.Count())
            {
                yield return startNode.Value;
            }
            yield break;
        }
        public override List<GraphNode<T>> getDeletionOrder()
        {
            var postOrderVisitor = new PostOrderVisitor<T>();
            var roots = getPossibleCommonRoot();
            if (!roots.Any())
            {
                return [];
            }
            T root = roots.First();
            nodePerValue[root].accept(postOrderVisitor);
            return postOrderVisitor.getPostOrder();
        }

    }
}
