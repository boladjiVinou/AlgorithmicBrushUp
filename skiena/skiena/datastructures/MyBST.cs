using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyBST<T> where T : IEquatable<T>, IComparable<T>
    {
        public MyBSTNode<T>? root { get; set; }

        public void add(T val)
        {
            if (root == null) 
            {
                root = new MyBSTNode<T>(null, val);
            }
            else
            {
                root.insert(val);
            }
        }
      

        public void remove(T val) 
        {
            if (root != null) 
            {
                root = root.remove(root, val);
            }
        }

        public IEnumerable<T> inOrderIteration() 
        {
            if (root == null) 
            {
                yield break;
            }
            Stack<MyBSTNode<T>> visitStack = new Stack<MyBSTNode<T>>();
            HashSet<MyBSTNode<T>?> visited = new HashSet<MyBSTNode<T>?>();
            visitStack.Push(root);
            while (visitStack.Count > 0) 
            {
                var node = visitStack.Peek();
                while (node != null && node.getLeft() != null && !visited.Contains(node.getLeft()))
                {
                    node = node.getLeft();
                    if (node != null) 
                    {
                        visitStack.Push(node);
                    }
                }

                var tmp = visitStack.Pop();
                yield return tmp.Value;
                visited.Add(tmp);


                var tmpRightChild = tmp.getRight();
                if (tmpRightChild != null && !visited.Contains(tmpRightChild)) 
                {
                    visitStack.Push(tmpRightChild);
                }
            }
        }

        public IEnumerable<T> postOrderIteration() 
        {
            if (root == null)
            {
                yield break;
            }
            Stack<MyBSTNode<T>> visitStack = new Stack<MyBSTNode<T>>();
            HashSet<MyBSTNode<T>?> visited = new HashSet<MyBSTNode<T>?>();
            visitStack.Push(root);
            while (visitStack.Count > 0)
            {
                var node = visitStack.Peek();
                while (node != null && node.getLeft() != null && !visited.Contains(node.getLeft()))
                {
                    node = node.getLeft();
                    if (node != null)
                    {
                        visitStack.Push(node);
                    }
                }

                var tmpRightChild = visitStack.Peek().getRight();
                if (tmpRightChild != null && !visited.Contains(tmpRightChild))
                {
                    visitStack.Push(tmpRightChild);
                }

                var tmp = visitStack.Peek();
                var tmpLeft = tmp.getLeft();
                var tmpRight = tmp.getRight();
                if (!visited.Contains(tmp) && 
                    (tmpLeft == null || visited.Contains(tmpLeft)) &&
                    (tmpRight == null || visited.Contains(tmpRight)))
                {
                    yield return tmp.Value;
                    visited.Add(tmp);
                    visitStack.Pop();
                }
            }
        }
        public IEnumerable<T> preOrderIteration() 
        {
            if (root == null)
            {
                yield break;
            }
            Stack<MyBSTNode<T>?> visitStack = new Stack<MyBSTNode<T>?>();
            visitStack.Push(root);
            while (visitStack.Count > 0)
            {
                var curr = visitStack.Pop();
                if (curr != null) 
                {
                    yield return curr.Value;


                    if (curr.hasRightChild())
                    {
                        visitStack.Push(curr.getRight());
                    }

                    if (curr.hasLeftChild())
                    {
                        visitStack.Push(curr.getLeft());
                    }
                }
            }
        }
    }
}
