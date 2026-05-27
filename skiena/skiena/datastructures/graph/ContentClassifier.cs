using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace skiena.datastructures.graph
{
    public class ContentClassifier<T> :DepthFirstSearchVisitor<T> where T:IEquatable<T>
    {
        private Dictionary<GraphNode<T>, Dictionary<GraphNode<T>, enEdge>> edgeTypeByEdge = [];
        private Dictionary<GraphNode<T>, int> entryTimePerNode = [];
        private Dictionary<GraphNode<T>, GraphNode<T>> reachableAncestorByNode = [];
        private Dictionary<GraphNode<T>, int> treeOutDegreeByNode = [];
        private Dictionary<GraphNode<T>, GraphNode<T>> parentByNode = [];
        private Dictionary<GraphNode<T>, enArticulationNode> articulationByNode = [];
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
            if (!reachableAncestorByNode.ContainsKey(node)) 
            {
                reachableAncestorByNode.Add(node,node);
            }
            if (!treeOutDegreeByNode.ContainsKey(node)) 
            {
                treeOutDegreeByNode.Add(node,0);
            }
        }

        public override void visitNode(GraphNode<T> node)
        {
            foreach (var neighbor in node.getNeighbors())
            {
                updateEdgeType(node, neighbor);

                updateNodeData(node, neighbor);

                if (visitInProgress.Contains(neighbor) || visited.Contains(neighbor))
                {
                    continue;
                }
                if (!parentByNode.ContainsKey(neighbor)) 
                {
                    parentByNode.Add(neighbor, node);
                }
                neighbor.accept(this);
            }
        }

        private void updateNodeData(GraphNode<T> node, GraphNode<T> neighbor)
        {
            if (getEdgeType(node, neighbor) == enEdge.Tree)
            {
                ++treeOutDegreeByNode[node];
            }
            else if (getEdgeType(node, neighbor) == enEdge.Back && parentByNode.ContainsKey(node) && parentByNode[node] != neighbor)
            {
                if (entryTimePerNode[neighbor] < entryTimePerNode[reachableAncestorByNode[node]])
                {
                    reachableAncestorByNode[node] = neighbor;
                }
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

        public bool containsEdge(enEdge edge) 
        {
            return edgeTypeByEdge.Values.Any(x => x.Values.Contains(edge));
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

        public override void postVisitNode(GraphNode<T> node)
        {
            base.postVisitNode(node);
            if (!parentByNode.ContainsKey(node) && treeOutDegreeByNode.ContainsKey(node) && treeOutDegreeByNode[node] > 1) // root
            {
                articulationByNode.Add(node, enArticulationNode.RootArticulation);
            }
            GraphNode<T>? grandParent = null;
            GraphNode<T>? parent = null;
            if (parentByNode.ContainsKey(node))
            {
                parent = parentByNode[node];
            }

            if (parent != null && parentByNode.ContainsKey(parent)) 
            {
                grandParent = parentByNode[parentByNode[node]];
            }
            if (reachableAncestorByNode[node] == parent && grandParent != null)
            {
                articulationByNode.Add(node, enArticulationNode.ParentArticulation);
            }

            if(reachableAncestorByNode.ContainsKey(node) && reachableAncestorByNode[node] == node && treeOutDegreeByNode[node] > 0)
            {
                articulationByNode.Add(node, enArticulationNode.BridgeArticulation);
            }
            if (entryTimePerNode.ContainsKey(node) && parent != null && entryTimePerNode.ContainsKey(parent)
                && entryTimePerNode[node] < entryTimePerNode[parent]) 
            {
                reachableAncestorByNode[parent] = reachableAncestorByNode[node];
            }
        }
        public bool isAnArticulationNode(GraphNode<T> node) 
        {
            return articulationByNode.ContainsKey(node);
        }
    }
}
