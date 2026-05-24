using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class SCCNode<T> : IEquatable<SCCNode<T>> where T : IEquatable<T>
    {
        private HashSet<GraphNode<T>> components = new HashSet<GraphNode<T>>();
        public SCCNode(HashSet<GraphNode<T>> scc) 
        {
            components = [.. scc];
        }

        public bool Equals(SCCNode<T>? other)
        {
            if (other == null) 
            {
                return false;
            }
           return components.Equals(other.components);
        }

        public override bool Equals(object obj)
        {
            return obj is SCCNode<T> && Equals(obj as SCCNode<T>);
        }

        public override int GetHashCode()
        {
            return components.GetHashCode();
        }
        public HashSet<GraphNode<T>> getNodes() 
        {
            return components;
        }
    }
}
