using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MyAvlTree<T> : MyBST<T> where T : IEquatable<T>, IComparable<T>
    {
        protected override MyAvlNode<T> createNode(T val)
        {
            return new MyAvlNode<T>(null, val);
        }

        public T? getRootValue()
        {
            if (root == null) 
            {
                return default;
            }
            return root.Value;
        }
        public bool isRootBalanced() 
        {
            return root == null || ((MyAvlNode<T>)root).isBalanced();
        }
        public bool areAllNodesBalanced() 
        {
            if (root != null)
            {
                Queue<MyAvlNode<T>> q = new Queue<MyAvlNode<T>>();
                q.Enqueue((MyAvlNode<T>)root);
                while (q.Count > 0) 
                {
                    var tmp = q.Dequeue();
                    if (!tmp.isBalanced()) 
                    {
                        return false;
                    }
                    var leftChild = tmp.getLeft();
                    if (leftChild != null) 
                    {
                        q.Enqueue(leftChild);
                    }
                    var rightChild = tmp.getRight();
                    if (rightChild != null)
                    {
                        q.Enqueue(rightChild);
                    }
                }
            }
            return true;
        }

        public bool containsLoop() 
        {
            if (root != null)
            {
                Queue<MyAvlNode<T>> q = new Queue<MyAvlNode<T>>();
                HashSet<MyAvlNode<T>> seen = new HashSet<MyAvlNode<T>>();
                q.Enqueue((MyAvlNode<T>)root);
                while (q.Count > 0)
                {
                    var tmp = q.Dequeue();
                    if (seen.Contains(tmp))
                    {
                        return true;
                    }
                    seen.Add(tmp);
                    var leftChild = tmp.getLeft();
                    if (leftChild != null)
                    {
                        q.Enqueue(leftChild);
                    }
                    var rightChild = tmp.getRight();
                    if (rightChild != null)
                    {
                        q.Enqueue(rightChild);
                    }
                }
            }
            return false;
        }
    }
}
