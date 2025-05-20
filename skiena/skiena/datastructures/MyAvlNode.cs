using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyAvlNode<T> : MyBSTNode<T> where T : IEquatable<T>, IComparable<T>
    {
        private int height;
        public MyAvlNode(MyAvlNode<T>? ancestor, T val) : base(ancestor, val)
        {
        }
        public override MyAvlNode<T> insert(T val)
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
                    left = left.insert(val);
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
                    right = right.insert(val);
                }
            }
            return rebalanceIfNeeded();
        }

        private void recomputeHeight()
        {
           height = Math.Max(computeLeftHeight(),computeRightHeight());
        }

        private int computeRightHeight()
        {
            var tmpRight = getRight();
            if (tmpRight != null)
            {
                return tmpRight.height + 1;
            }
            return 0;
        }

        private int computeLeftHeight()
        {
            var tmpLeft = getLeft();
            if (tmpLeft != null)
            {
                return tmpLeft.height + 1;
            }
            return 0;
        }

        private void rotateRight() 
        {
            var tmpRight = getRight();
            var oldParent = (MyAvlNode<T>?)getParent();
            replaceBy(tmpRight);
            if (oldParent != null) 
            {
                setRight(oldParent);
                becomeParent(oldParent);
            }
   
            List<MyAvlNode<T>?> nodesToUpdate =
            [
                tmpRight,
                oldParent,
                this,
                (MyAvlNode<T>?)getParent(),
            ];
            foreach (var touchedNodes in nodesToUpdate) 
            {
                touchedNodes?.recomputeHeight();
            }
        }
        private void rotateLeft()
        {
            var tmpLeft = getLeft();
            var oldParent = (MyAvlNode<T>?)getParent();
            replaceBy(tmpLeft);
            if (oldParent != null)
            {
                setLeft(oldParent);
                becomeParent(oldParent);
            }

            List<MyAvlNode<T>?> nodesToUpdate =
            [
                tmpLeft,
                oldParent,
                this,
                (MyAvlNode<T>?)getParent(),
            ];
            foreach (var touchedNodes in nodesToUpdate)
            {
                touchedNodes?.recomputeHeight();
            }
        }

        private void becomeParent(MyAvlNode<T>? node)
        {
            setParent(node?.getParent());
            if (node != null && node.isLeftChild())
            {
                ((MyAvlNode<T>?)node?.getParent())?.setLeft(this);
            }
            else if (node != null)
            {
                ((MyAvlNode<T>?)node?.getParent())?.setRight(this);
            }
            node?.setParent(this);
        }

        private MyAvlNode<T> rebalanceIfNeeded()
        {
            int balance = computeBalance();
            if (balance > 1)
            {
                var tmpLeft = getLeft();
                var rightChildOfLeftChild = tmpLeft?.getRight();
                if (tmpLeft?.computeBalance() > 0)// outer insert
                {
                    tmpLeft.rotateRight();
                    return tmpLeft;
                }
                else if (tmpLeft != null && rightChildOfLeftChild != null) // inner insert
                {
                    rightChildOfLeftChild.rotateLeft();
                    rightChildOfLeftChild.rotateRight();
                    return rightChildOfLeftChild;
                }
            }
            else if (balance < -1)
            {
                var tmpRight = getRight();
                var leftChildOfRightChild = tmpRight.getLeft();
                if (tmpRight?.computeBalance() < 0)// outer insert
                {
                    tmpRight.rotateLeft();
                    return tmpRight;
                }
                else if(tmpRight != null && leftChildOfRightChild != null) // inner insert
                {
                    leftChildOfRightChild.rotateRight();
                    leftChildOfRightChild.rotateLeft();
                    return leftChildOfRightChild;
                }
            }
            recomputeHeight();
            return this;
        }

        private int computeBalance()
        {
            return computeLeftHeight() - computeRightHeight();
        }

        public override MyAvlNode<T>? removeFirst(MyBSTNode<T>? root, T val) 
        {
            return ((MyAvlNode<T>?)base.removeFirst(root, val))?.rebalanceIfNeeded();
        }
        protected override MyAvlNode<T> createChild(T val)
        {
            return new MyAvlNode<T>(this, val);
        }
        public bool isBalanced() 
        {
            return Math.Abs(computeBalance()) <= 1;
        }
        public override MyAvlNode<T>? getLeft()
        {
            return (MyAvlNode<T>?) base.getLeft();
        }
        public override MyAvlNode<T>? getRight()
        {
            return (MyAvlNode<T>?)base.getRight();
        }
    }
}
