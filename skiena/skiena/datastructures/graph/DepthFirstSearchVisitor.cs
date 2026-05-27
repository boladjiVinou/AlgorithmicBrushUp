using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class DepthFirstSearchVisitor<T> : INodeVisitor<T> where T : IEquatable<T>
    {
        protected HashSet<GraphNode<T>> visited = [];
        protected HashSet<GraphNode<T>> visitInProgress = [];
        private GraphNode<T> latestVisitedNode;
        public virtual void preVisitNode(GraphNode<T> node)
        {
            visitInProgress.Add(node);
        }

        public virtual void visitNode(GraphNode<T> node)
        {
            foreach (var neighbor in node.getNeighbors()) 
            {
                if (visited.Contains(neighbor) || visitInProgress.Contains(node)) 
                {
                    continue;
                }
                neighbor.accept(this);
            }
        }

        public virtual void postVisitNode(GraphNode<T> node)
        {
            visitInProgress.Remove(node);
            visited.Add(node);
            latestVisitedNode = node;
        }

        public HashSet<GraphNode<T>> getVisitedNodes() 
        {
            return [..visited];
        }
        public bool hasVisited(GraphNode<T> node) 
        {
            return visited.Contains(node);
        }
        public int getNbOfVisitedNode() 
        {
            return visited.Count;
        }

        public GraphNode<T>? getLastNodeVisited() 
        {
            return latestVisitedNode;
        }
    }
}
