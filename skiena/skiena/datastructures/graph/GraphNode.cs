using skiena.datastructures.lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class GraphNode<T> : IEquatable<GraphNode<T>>, IVisitable<T> where T : IEquatable<T>
    {
        public T Value { get; set; }
        private int inDegree = 0;
        private HashSet<GraphNode<T>> neighbors = [];

        public GraphNode(T value) 
        {
            Value = value;
        }
        public void accept(INodeVisitor<T> visitor) 
        {
            if(visitor == null) {  return; }

            visitor.preVisitNode(this);
            visitor.visitNode(this);
            visitor.postVisitNode(this);
        }

        public bool Equals(GraphNode<T>? other) 
        {
            if (other == null) 
            {
                return false;
            }
            return Value.Equals(other.Value);
        }

        public void connectTo(GraphNode<T> node) 
        {
            if (node == null || neighbors.Contains(node)) 
            {
                return;
            }
            ++node.inDegree;
            neighbors.Add(node);
        }
        public void disconnectFrom(GraphNode<T> node)
        {
            if(node == null || !neighbors.Contains(node))
            {
                return;
            }
            --node.inDegree;
            neighbors.Remove(node);
        }

        public bool isConnectedTo(GraphNode<T> node) 
        {
            return node != null && neighbors.Contains(node);
        }
        public int getInDegree() {  return inDegree; }
        public int getOutDegree() { return neighbors.Count; }
        public HashSet<GraphNode<T>>getNeighbors() { return [..neighbors]; }
    }
}
