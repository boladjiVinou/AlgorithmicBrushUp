using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public interface INodeVisitor<T> where T : IEquatable<T>
    {
        void preVisitNode(GraphNode<T> node);
        void visitNode(GraphNode<T> node);
        void postVisitNode(GraphNode<T> node);
    }
}
