using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyDisjointSet<T>
    {
        private Dictionary<T,T> roots = [];
        private Dictionary<T,int> ranks = [];

        public MyDisjointSet(T[] data) 
        {
            foreach (T node in data) 
            {
                insert(node);
            }
        }

        public void insert(T node) 
        {
            roots.Add(node, node);
            ranks.Add(node, 0);
        }
        public bool areConnected(T n1, T n2) 
        {
            return roots.ContainsKey(n1) && roots.ContainsKey(n2) && findRoot(n1).Equals(findRoot(n2));
        }

        public T findRoot(T node) 
        {
            T curr = node;
            Stack<T> path = [];
            while (roots.ContainsKey(curr) && !curr.Equals(roots[curr])) 
            {
                path.Push(curr);
                curr = roots[curr];
            }
            while (path.Count > 0) 
            {
                roots[path.Pop()] = curr;
            }
            return curr;
        }

        public void connect(T node, T node2) 
        {
            var nodeParent = findRoot(node);
            var node2Parent = findRoot(node2);
            if (nodeParent != null && node2Parent !=null)
            {
                if (ranks[nodeParent] > ranks[node2Parent])
                {
                    roots[node2Parent] = nodeParent;
                    ranks[nodeParent] += 1;
                }
                else
                {
                    roots[nodeParent] = node2Parent;
                    ranks[node2Parent] += 1;
                }
            }
        }
    }
}
