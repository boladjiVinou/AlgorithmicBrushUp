using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MyBTNode<T>(MyBTNode<T>? ancestor, T val) where T : IEquatable<T>
    {
        protected T modifiableValue = val;
        public T Value
        {
            get
            {
                return modifiableValue;
            }
        }
        protected MyBTNode<T>? left;
        protected MyBTNode<T>? right;
        protected MyBTNode<T>? parent = ancestor;


        public virtual MyBTNode<T> insert(T val)
        {
            Queue<MyBTNode<T>?> queue = [];
            queue.Enqueue(this);
            MyBTNode<T>? latestFound = null;
            MyBTNode<T>? currChild = null;
            bool inserted = false;
            while (queue.Any() && !inserted)
            {
                latestFound = currChild;
                currChild = queue.Dequeue();
                if (currChild != null)
                {
                    if (!currChild.hasLeftChild())
                    {
                        currChild.setLeft(currChild.createChild(val));
                        inserted = true;
                    }
                    else if (!currChild.hasRightChild()) 
                    {
                        currChild.setRight(currChild.createChild(val));
                        inserted = true;
                    }
                    else
                    {
                        queue.Enqueue(currChild.getLeft());
                        queue.Enqueue(currChild.getRight());
                    }
                }
            }
            if (!inserted) 
            {
                latestFound?.setLeft(latestFound.createChild(val));
            }
            return this;
        }

        protected virtual MyBTNode<T> createChild(T val)
        {
            return new MyBTNode<T>(this, val);
        }
        public virtual MyBTNode<T>? getLeft()
        {
            return left;
        }
        public virtual MyBTNode<T>? getRight()
        {
            return right;
        }
        public MyBTNode<T>? removeFirst(T val) 
        {
            Queue<MyBTNode<T>?> queue = [];
            queue.Enqueue(this);
            MyBTNode<T>? successor = null;
            while (queue.Any()) 
            {
                var tmp = queue.Dequeue();
                if (tmp == null) 
                {
                    continue;
                }
                if (tmp.Value.Equals(val))
                {
                    successor = searchSuccessorInRightSubtree();
                    replaceCurrentReferenceInTreeBy(successor);
                    break;
                }
                else 
                {
                    queue.Enqueue(tmp.getLeft());
                    queue.Enqueue(tmp.getRight());
                }
            }
            return successor;
        }
        protected void replaceCurrentReferenceInTreeBy(MyBTNode<T>? node)
        {
            if (isLeftChild())
            {
                parent?.setLeft(node);
            }
            else
            {
                parent?.setRight(node);
            }
            node?.setParent(parent);
            parent = null;
        }

        public MyBTNode<T>? searchSuccessorInRightSubtree()
        {
            Queue<MyBTNode<T>?> queue = [];
            queue.Enqueue(getRight());
            MyBTNode<T>? deepestChild = null;
            MyBTNode<T>? currChild = null;
            while (queue.Any())
            {
                deepestChild = currChild;
                currChild = queue.Dequeue();
                if (currChild != null) 
                {
                    queue.Enqueue(currChild.getLeft());
                    queue.Enqueue(currChild.getRight());
                }
            }
            return deepestChild;
        }

        public bool hasLeftChild()
        {
            return left != null;
        }
        public bool hasRightChild()
        {
            return right != null;
        }
        protected void setLeft(MyBTNode<T>? pLeft)
        {
            left = pLeft;
        }
        protected void setRight(MyBTNode<T>? pRight)
        {
            right = pRight;
        }

        protected bool isLeftChild()
        {
            return parent?.left == this;
        }

        protected bool isRightChild()
        {
            return parent?.right == this;
        }

        protected void setParent(MyBTNode<T>? node)
        {
            parent = node;
        }
        public virtual MyBTNode<T>? getParent() { return parent; }

        public bool contains(T val) 
        {
            if (Value.Equals(val)) 
            {
                return true;
            }
            return( left?.contains(val) ?? false) || (right?.contains(val) ?? false);
        }
     
    }
}
