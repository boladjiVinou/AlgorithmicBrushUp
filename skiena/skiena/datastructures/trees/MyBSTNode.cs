using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MyBSTNode<T>(MyBSTNode<T>? ancestor,T val) where T : IEquatable<T>, IComparable<T>
    {
        protected T modifiableValue = val;
        public T Value 
        {
            get 
            { 
                return modifiableValue;
            }
        } 
        protected MyBSTNode<T>? left;
        protected MyBSTNode<T>? right;
        protected MyBSTNode<T>? parent = ancestor;


        public virtual MyBSTNode<T> insert(T val) 
        {
            var comparisonRes = Value.CompareTo(val);
            if (Value.CompareTo(val) > 0)
            {
                if (left == null)
                {
                    left = createChild(val);
                }
                else
                {
                    left.insert(val);
                }
            }
            else
            {
                if (right == null)
                {
                    right = createChild(val);
                }
                else
                {
                    right.insert(val);
                }
            }
            return this;
        }

        protected virtual MyBSTNode<T> createChild(T val) 
        {
            return new MyBSTNode<T>(this, val);
        }
        public virtual MyBSTNode<T>? getLeft()
        {
            return left;
        }
        public virtual  MyBSTNode<T>? getRight() 
        {
            return right;
        }

        public bool hasLeftChild() 
        {
            return left != null;
        }
        public bool hasRightChild() 
        {
            return right != null;
        }
        protected void setLeft(MyBSTNode<T>? pLeft)
        {
            left = pLeft;
        }
        protected void setRight(MyBSTNode<T>? pRight) 
        {
            right = pRight;
        }

        public MyBSTNode<T> getMaximum()
        {
            MyBSTNode<T>? curr = this;
            MyBSTNode<T> max = this;
            while (curr != null)
            {
                max = curr;
                curr = curr.getRight();
            }
            return max;
        }
        public MyBSTNode<T> getMinimum()
        {
            MyBSTNode<T>? curr = this;
            MyBSTNode<T> min = this;
            while (curr != null)
            {
                min = curr;
                curr = curr.getLeft();
            }
            return min;
        }

        public MyBSTNode<T>? searchMinimumExcept(MyBSTNode<T> nodeToSkip)
        {
            MyBSTNode<T>? curr = this;
            MyBSTNode<T>? min = null;
            MyBSTNode<T>? prevMin = null;
            while (curr != null)
            {
                prevMin = min;
                min = curr;
                curr = curr.getLeft();
            }
            if (min == nodeToSkip)
            {
                return prevMin;
            }
            return min;
        }

        public MyBSTNode<T>? searchSuccessorInRightSubtree() 
        {
            MyBSTNode<T>? right = getRight();
            if (right != null )
            {
                return right.getMinimum();
            }
            return null;
        }

        protected bool isLeftChild() 
        {
            return parent?.left == this;
        }

        protected bool isRightChild() 
        {
            return parent?.right == this;
        }
        public MyBSTNode<T>? getPredecessorInLeftSubtree()
        {
            MyBSTNode<T>? left = getLeft();
            if (left != null)
            {
                return left.getMaximum();
            }
            return null;
        }

        private MyBSTNode<T>? searchPredecessorAmongParents()
        {
            if (isRightChild())
            {
                return parent;
            }
            var par = parent;
            while (par != null && par.isLeftChild())
            {
                par = par.parent;
            }
            if (par != null)
            {
                par = par.parent;
            }
            return par;
        }

        protected void replaceCurrentReferenceInTreeBy(MyBSTNode<T>? node) 
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

        public virtual MyBSTNode<T>? removeFirst(T val)
        {
            var comparisonRes = Value.CompareTo(val);
            if (comparisonRes > 0)
            {
                left = getLeft()?.removeFirst(val);
            }
            else if (comparisonRes < 0)
            {
                right = getRight()?.removeFirst(val);
            }
            else if (left == null && right != null)
            {
                replaceCurrentReferenceInTreeBy(right);
                return right;
            }
            else if (right == null && left != null)
            {
                replaceCurrentReferenceInTreeBy(left);
                return left;
            }
            else if (right != null && left != null)
            {
                MyBSTNode<T>? successor = searchSuccessorInRightSubtree();
                if (successor != null) // it is garantee to have a successor in this case
                {
                    T newValue = successor.Value;
                    right = getRight()?.removeFirst(successor.Value);
                    modifiableValue = newValue;
                }
            }
            else 
            {
                // the current node is the node to delete and has no children
                return null;
            }
            return this;
        }


        protected void setParent(MyBSTNode<T>? node) 
        {
            parent = node;
        }
        public virtual MyBSTNode<T>? getParent() { return parent; }
        public bool contains(T data)
        {
            var res = Value.CompareTo(data);
            if (res == 0)
            {
                return true;
            }
            else if (res < 0 && right != null)
            {
                return right.contains(data);
            }
            else if(res > 0 && left != null)
            {
                return left.contains(data);
            }
            return false;
        }
    }
}
