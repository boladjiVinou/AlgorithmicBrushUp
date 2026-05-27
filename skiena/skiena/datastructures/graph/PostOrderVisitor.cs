using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class PostOrderVisitor<T>: DepthFirstSearchVisitor<T> where T : IEquatable<T>
    {
        private List<GraphNode<T>> postOrder = [];

        public override void postVisitNode(GraphNode<T> node)
        {
            base.postVisitNode(node);
            postOrder.Add(node);
        }

        public List<GraphNode<T>> getPostOrder() 
        {
            return [..postOrder];
        }
    }
}
