using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyRedBlackTree<T> : MyBST<T> where T : IEquatable<T>, IComparable<T>
    {
        protected override MyRedBlackNode<T> createNode(T val)
        {
            return new MyRedBlackNode<T>(null, val);
        }

        public bool isTreeValid()
        {
            if (root != null)
            {
                try
                {
                    ((MyRedBlackNode<T>)root).countBlackPathLength();
                }
                catch (InvalidDataException)
                {
                    return false;
                }
                Queue<MyRedBlackNode<T>> q = new Queue<MyRedBlackNode<T>>();
                q.Enqueue((MyRedBlackNode<T>)root);
                while (q.Count > 0)
                {
                    var tmp = q.Dequeue();
                    var tmpParent = tmp.getParent();
                    if (tmpParent != null && tmpParent.isRed() && tmp.isRed())
                    {
                        return false;
                    }
                    else if(tmpParent == null && tmp.isRed()) 
                    {
                        return false;
                    }
                    if (tmp.isNullNodeInstance())
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
                Queue<MyRedBlackNode<T>> q = new Queue<MyRedBlackNode<T>>();
                HashSet<MyRedBlackNode<T>> seen = new HashSet<MyRedBlackNode<T>>();
                q.Enqueue((MyRedBlackNode<T>)root);
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
