using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace skiena.datastructures.graph
{
    public partial class EdgeClassifier<T> :DepthFirstSearchVisitor<T> where T:IEquatable<T>
    {
        private Dictionary<GraphNode<T>, Dictionary<GraphNode<T>, enEdge>> edgeTypeByEdge = [];
        private Dictionary<GraphNode<T>, int> entryTimePerNode = [];
        private int time = 0;
        public override void preVisitNode(GraphNode<T> node)
        {
            base.preVisitNode(node);
            ++time;
            if (!entryTimePerNode.ContainsKey(node))
            {
                entryTimePerNode.Add(node, time);
            }
            else 
            {
                entryTimePerNode[node] = time;
            }
        }

        public override void visitNode(GraphNode<T> node)
        {
            foreach (var neighbor in node.getNeighbors())
            {
                updateEdgeType(node, neighbor);

                if (visitInProgress.Contains(neighbor) || visited.Contains(neighbor))
                {
                    continue;
                }
                neighbor.accept(this);
            }
        }

        private void updateEdgeType(GraphNode<T> node, GraphNode<T> neighbor)
        {
            if (visitInProgress.Contains(neighbor))
            {
                setEdgeType(node, neighbor, enEdge.Back);
            }
            else if (visited.Contains(neighbor))
            {
                if (entryTimePerNode[node] < entryTimePerNode[neighbor])
                {
                    setEdgeType(node, neighbor, enEdge.Forward);
                }
                else if (entryTimePerNode[node] > entryTimePerNode[neighbor])
                {
                    setEdgeType(node, neighbor, enEdge.Cross);
                }
                else
                {
                    throw new InvalidOperationException("The edge is incoherent");
                }
            }
            else
            {
                setEdgeType(node, neighbor, enEdge.Tree);
            }
        }

        public enEdge getEdgeType(GraphNode<T> n1, GraphNode<T> n2) 
        {
            return edgeTypeByEdge[n1][n2];
        }

        private void setEdgeType(GraphNode<T> n1, GraphNode<T> n2, enEdge edge) 
        {
            if (!edgeTypeByEdge.ContainsKey(n1))
            {
                edgeTypeByEdge.Add(n1, []);
            }
            edgeTypeByEdge[n1].Add(n2, edge);
        }
    }
}
