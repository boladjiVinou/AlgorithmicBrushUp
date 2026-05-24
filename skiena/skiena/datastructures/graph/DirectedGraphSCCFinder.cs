using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class DirectedGraphSCCFinder<T>: DepthFirstSearchVisitor<T> where T : IEquatable<T>
    {
        private Dictionary<GraphNode<T>, int> lowLinkByNode = [];
        private Dictionary<GraphNode<T>, int> idByNode = [];
        private Stack<GraphNode<T>> tarjanStack = [];
        private int idCounter = 0;

        public override void preVisitNode(GraphNode<T> node)
        {
            base.preVisitNode(node);
            if (!idByNode.ContainsKey(node)) 
            {
                idByNode.Add(node, ++idCounter);
            }
            lowLinkByNode[node] = idByNode[node];
            tarjanStack.Push(node);
        }

        public override void visitNode(GraphNode<T> node)
        {
            foreach (var neighbor in node.getNeighbors())
            {
                if (visited.Contains(neighbor))
                {
                    lowLinkByNode[node] = Math.Min(lowLinkByNode[node], idByNode[neighbor]);
                    continue;
                }
                if (!visitInProgress.Contains(node)) 
                {
                    neighbor.accept(this);
                }
                lowLinkByNode[node] = Math.Min(lowLinkByNode[node], lowLinkByNode[neighbor]);
            }
        }

        public override void postVisitNode(GraphNode<T> node)
        {
            base.postVisitNode(node);
            if (lowLinkByNode[node] == idByNode[node]) // scc root
            {
                while (tarjanStack.Count > 0) 
                {
                    var prev = tarjanStack.Pop(); ;
                    if (prev != node) 
                    {
                        lowLinkByNode[prev] = lowLinkByNode[node];
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }

        public DirectedGraph<SCCNode<T>> search(DirectedGraph<T> graph, Dictionary<T, GraphNode<T>> nodePerValue) 
        {
            lowLinkByNode = [];
            idByNode = [];
            idCounter = 0;
            foreach (var val in graph.getVertices()) 
            {
                var node = nodePerValue[val];
                if (visited.Contains(node)) 
                {
                    continue;
                }
                if (visitInProgress.Contains(node)) 
                {
                    throw new InvalidOperationException("Bad depth first search done");
                }
                node.accept(this);
            }
            return buildCompressedSCCGraph([.. lowLinkByNode.GroupBy(x => x.Value, y =>y.Key).Select(x => x.ToHashSet())]);
        }

        private DirectedGraph<SCCNode<T>> buildCompressedSCCGraph(List<HashSet<GraphNode<T>>> connectedComponents) 
        {
            DirectedGraph<SCCNode<T>> compressedGraph = new DirectedGraph<SCCNode<T>>();
            Dictionary<GraphNode<T>, SCCNode<T>> nodeByCompressedScc = [];
            foreach (var connectedComponent in connectedComponents) 
            {
                var sccNode = new SCCNode<T>(connectedComponent);
                foreach (var node in connectedComponent) 
                {
                    nodeByCompressedScc.Add(node, sccNode);
                }
            }
            foreach (var connectedComponent in connectedComponents)
            {
                foreach (var node in connectedComponent)
                {
                    foreach (var neighbor in node.getNeighbors()) 
                    {
                        if (nodeByCompressedScc[neighbor] != nodeByCompressedScc[node])
                        {
                            compressedGraph.connect(nodeByCompressedScc[node], nodeByCompressedScc[neighbor]);
                        }
                    }
                    
                }
            }
            return compressedGraph;

        }
    }
}
