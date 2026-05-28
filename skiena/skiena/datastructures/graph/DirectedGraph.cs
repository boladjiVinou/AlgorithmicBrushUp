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
        public bool containsAnArborescence()
        {
            var possibleRoots = getPossibleRoots().ToList();
            if (possibleRoots.Count == 1) 
            {
                var classifier = new ContentClassifier<T>();
                nodePerValue[possibleRoots[0]].accept(classifier);
                return areAllNodesReachableFrom(possibleRoots[0]) && 
                    !classifier.containsEdge(enEdge.Back);// no back edge means no cycle
            }
            return false;
        }
        public bool isAMotherVertex(T val) 
        {
            if (nodePerValue.ContainsKey(val) && nodePerValue[val] != null)
            {
                return areAllNodesReachableFrom(val);
            }
            return false;
        }

        private bool areAllNodesReachableFrom(T val)
        {
            var dfsVisitor = new DepthFirstSearchVisitor<T>();
            nodePerValue[val].accept(dfsVisitor);
            return dfsVisitor.getVisitedNodes().Count == nodePerValue.Values.Where(x => x != null).Count();
        }

        public bool containsAMotherVertex() 
        {
            var sccFinder = new DirectedGraphSCCFinder<T>();
            
            var sccGraph = sccFinder.search(this, nodePerValue);

            var inNode = sccGraph.nodePerValue.Values.Where(x => sccGraph.getInDegree(x.Value) == 0).ToList();

            return inNode.Count == 1 && isAMotherVertex(inNode[0].Value.getNodes().First().Value);

        }

        public override IEnumerable<T> getPossibleRoots()
        {
            var dfsVisitor = new DepthFirstSearchVisitor<T>();
            foreach (var node in nodePerValue.Values)
            {
                if (dfsVisitor.hasVisited(node))
                {
                    continue;
                }
                node.accept(dfsVisitor);
            }
            var lastVisitedNode = dfsVisitor.getLastNodeVisited();
            if (lastVisitedNode != null)
            {
                var secondPassDfs = new DepthFirstSearchVisitor<T>();
                lastVisitedNode.accept(secondPassDfs);
                if (secondPassDfs.getNbOfVisitedNode() == nodePerValue.Values.Where(x => x != null).Count())
                {
                    yield return lastVisitedNode.Value;
                }
            }
            yield break;
        }

        public override List<GraphNode<T>> getDeletionOrder()
        {
            var sccFinder = new DirectedGraphSCCFinder<T>();

            var sccGraph = sccFinder.search(this, nodePerValue);

            List<GraphNode<T>> deletionOrder = [];
            Queue<GraphNode<SCCNode<T>>> topologicalSortQueue = new Queue<GraphNode<SCCNode<T>>>();
            foreach (var item in sccGraph.nodePerValue.Values.Where(x => sccGraph.getInDegree(x.Value) == 0))
            {
                topologicalSortQueue.Enqueue(item);
            }
            while (topologicalSortQueue.Count > 0) 
            {
                var scc = topologicalSortQueue.Dequeue();
                foreach (var node in scc.Value.getNodes()) 
                {
                    deletionOrder.Add(node);
                }
               
                foreach (var sccNeighbor in sccGraph.getNeighbors(scc.Value))
                {
                    sccGraph.disconnect(scc.Value, sccNeighbor);
                    if (sccGraph.getInDegree(sccNeighbor) == 0) 
                    {
                        topologicalSortQueue.Enqueue(sccGraph.nodePerValue[sccNeighbor]);
                    }
                }
            }
            if (sccGraph.nodePerValue.Count > 0) 
            {
                return [];
            }

            deletionOrder.Reverse();
            return deletionOrder;
        }

    }
}
