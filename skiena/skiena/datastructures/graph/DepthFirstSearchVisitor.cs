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
        }
    }
}
