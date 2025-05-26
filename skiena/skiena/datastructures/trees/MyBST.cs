using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MyBST<T> : IEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        protected MyBSTNode<T>? root { get; set; }

        public virtual void add(T val)
        {
            if (root == null) 
            {
                root = createNode(val);
            }
            else
            {
                root = root.insert(val);
            }
        }

        protected virtual MyBSTNode<T> createNode(T val) 
        {
           return new MyBSTNode<T>(null, val);
        }


        public virtual void remove(T val) 
        {
            if (root != null) 
            {
                root = root.removeFirst(val);
            }
        }

        public bool isABST() 
        {
            if (root == null) 
            {
                return true;
            }
            Queue<MyBSTNode<T>> nodesQueue = new Queue<MyBSTNode<T>>();
            nodesQueue.Enqueue(root);
            while (nodesQueue.Count > 0) 
            {
                var node = nodesQueue.Dequeue();
                var left = node.getLeft();
                var right = node.getRight();
                if (left != null && left.Value.CompareTo(node.Value) < 0)
                {
                    nodesQueue.Enqueue(left);
                }
                else if (left != null)
                {
                    return false;
                }
                if (right != null && right.Value.CompareTo(node.Value) >= 0)
                {
                    nodesQueue.Enqueue(right);
                }
                else if(right != null)
                {
                    return false;
                }
            }
            return true;
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

        public IEnumerator<T> GetEnumerator()
        {
            return inOrderIteration().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T? getMaximum() 
        {
            if (root == null) 
            {
                return default;
            }
            return root.getMaximum().Value;
        }
        public T? getMinimum()
        {
            if (root == null)
            {
                return default;
            }
            return root.getMinimum().Value;
        }
    }
}
