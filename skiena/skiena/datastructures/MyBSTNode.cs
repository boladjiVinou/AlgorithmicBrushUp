using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyBSTNode<T>(MyBSTNode<T>? ancestor,T val) where T : IEquatable<T>, IComparable<T>
    {
        private T modifiableValue = val;
        public T Value 
        {
            get 
            { 
                return modifiableValue;
            }
        } 
        private MyBSTNode<T>? left;
        private MyBSTNode<T>? right;
        private MyBSTNode<T>? parent = ancestor;


        public void insert(T val) 
        {
            var comparisonRes = Value.CompareTo(val);
            if (Value.CompareTo(val) > 0)
            {
                if (left == null)
                {
                    left = new MyBSTNode<T>(this, val);
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
                    right = new MyBSTNode<T>(this, val);
                }
                else
                {
                    right.insert(val);
                }
            }
        }
        public MyBSTNode<T>? getLeft()
        {
            return left;
        }
        public MyBSTNode<T>? getRight() 
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
        private void setLeft(MyBSTNode<T>? pLeft)
        {
            left = pLeft;
        }
        private void setRight(MyBSTNode<T>? pRight) 
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

        public MyBSTNode<T>? getSuccessorInRightSubtree() 
        {
            MyBSTNode<T>? right = getRight();
            if (right != null)
            {
                return right.getMinimum();
            }
            return null;
        }

        private MyBSTNode<T>? searchSuccessorAmongParent()
        {
            if (isLeftChild())
            {
                return parent;
            }
            var par = parent;
            while (par != null && par.isRightChild())
            {
                par = par.parent;
            }
            if (par != null) 
            {
                par = par.parent;
            }
            return par;
        }

        private bool isLeftChild() 
        {
            return parent?.left == this;
        }

        private bool isRightChild() 
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

        private void replaceBy(MyBSTNode<T> node) 
        {
            if (isLeftChild())
            {
                parent?.setLeft(node);
            }
            else
            {
                parent?.setRight(node);
            }
            node.parent = parent;
            this.parent = null;
        }

        public MyBSTNode<T>? remove(MyBSTNode<T>? root,T val)
        {
            if (root == null) 
            {
                return null;
            }
            var comparisonRes = Value.CompareTo(val);
            if (comparisonRes > 0)
            {
                left = getLeft()?.remove(left, val);
            }
            else if (comparisonRes < 0)
            {
                right = getRight()?.remove(right, val);
            }
            else if (left == null && right != null)
            {
                replaceBy(right);
                if (this == root) 
                {
                    return right;
                }
            }
            else if (right == null && left != null) 
            {
                replaceBy(left);
                if (this == root)
                {
                    return left;
                }
            }
            else if (right != null && left != null)
            {
                MyBSTNode<T>? successor = getSuccessorInRightSubtree();
                if (successor != null) // it is garantee to have a successor in this case
                {
                    T newValue = successor.Value;
                    right = remove(right, successor.Value);
                    modifiableValue = newValue;
                }
            }
            return root;
        }


        private void setParent(MyBSTNode<T>? node) 
        {
            parent = node;
        }
        public MyBSTNode<T>? getParent() { return parent; }
    }
}
