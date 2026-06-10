using skiena.Chapter5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class HamiltonianPathFinder<T> : INodeVisitor<T> where T : IEquatable<T>, IComparable<T>
    {
        private Stack<GraphNode<T>> path = [];
        private int nbNodesToVisit;
        private HashSet<CustomTuple<T>> usedEdges = [];

        public HamiltonianPathFinder(int nbNodesToVisit) 
        {
            this.nbNodesToVisit = nbNodesToVisit;
        }
        public void preVisitNode(GraphNode<T> node)
        {
        }

        public void visitNode(GraphNode<T> node)
        {
            foreach (var neighbor in node.getNeighbors())
            {
                var edge = new CustomTuple<T>(node.Value, neighbor.Value);
                if (usedEdges.Contains(edge))
                {
                    continue;
                }
                usedEdges.Add(edge);
                neighbor.accept(this);
            }
        }

        public void postVisitNode(GraphNode<T> node)
        {
            path.Push(node);

            if (path.Count > 0 && path.Peek() != node) 
            {
                throw new InvalidOperationException("Bad visiting state");
            }
        }
        public bool hasFoundAPath() 
        {
            return path.Count == nbNodesToVisit;
        }

        public GraphNode<T>[] getPath()
        {
            if (hasFoundAPath())
            {
                var tmpPath = new GraphNode<T>[path.Count];
                var foundPath = new Stack<GraphNode<T>>(path);
                for (int i = path.Count-1; i >=0; --i) 
                {
                    tmpPath[i] = foundPath.Pop();
                }
                return tmpPath;
            }
            return [];
        }
    }
}
