using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class HamiltonianPathFinder<T> : DepthFirstSearchVisitor<T>  where T : IEquatable<T>
    {
        private Stack<GraphNode<T>> path = [];
        private int nbNodesToVisit;

        public HamiltonianPathFinder(int nbNodesToVisit) 
        {
            this.nbNodesToVisit = nbNodesToVisit;
        }
        public override void preVisitNode(GraphNode<T> node)
        {
            base.preVisitNode(node);

            path.Push(node);
        }

        public override void postVisitNode(GraphNode<T> node)
        {
            base.postVisitNode(node);

            if (visited.Count != nbNodesToVisit) 
            {
                if (path.Count > 0 && path.Peek() != node)
                {
                    throw new InvalidOperationException("Bad visiting state");
                }
                if (path.Count > 0)
                {
                    path.Pop();
                    visited.Remove(node);
                }
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
