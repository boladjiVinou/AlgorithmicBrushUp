using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyRedBlackNode<T> : MyBSTNode<T> where T : IEquatable<T>, IComparable<T>
    {
        private bool isRed = true;
        public MyRedBlackNode(MyBSTNode<T>? ancestor, T val) : base(ancestor, val)
        {
        }

        public override MyRedBlackNode<T> insert(T val)
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

        private MyRedBlackNode<T>? getAunt()
        {
            var tmpParent = getParent();
            if (tmpParent == null)
            {
                return null;
            }
            MyRedBlackNode<T>? aunt;
            if (isLeftChild())
            {
                aunt = tmpParent.getRight();
            }
            else
            {
                aunt = tmpParent.getLeft();
            }

            return aunt;
        }

        private void flipColor() 
        {
            getParent()?.setRed(false);
            getParent()?.getParent()?.setRed(true);
            getAunt()?.setRed(false);
        }

        private void updateRootColorIfNeeded() 
        {
            if (getParent() == null && isRed) 
            {
                isRed = false;
            }
        }
        private void setRed(bool red) 
        {
            this.isRed = red;
        }
        public override MyRedBlackNode<T>? getParent() 
        { 
            return (MyRedBlackNode<T> ?)base.getParent(); 
        }

        private bool isRoot() 
        {
            return getParent() == null;
        }
        private MyRedBlackNode<T> rebalanceIfNeeded()
        {
            // I fix conflicts by being on grand parent level
            if (isRoot() && isRed) 
            {
                updateRootColorIfNeeded();
                return this;
            }
            var tmpLeft = getLeft();
            var tmpRight = getRight();
            if (tmpLeft != null && tmpRight != null && tmpLeft.isRed && tmpRight.isRed)// two red children
            {
                List<MyRedBlackNode<T>?> grandChildren = new List<MyRedBlackNode<T>?>()
                { tmpLeft.getLeft(),tmpLeft.getRight(), tmpRight.getLeft(), tmpRight.getRight()};
                var problematicGrandChild =  grandChildren.Where(x => x != null && x.isRed);
                if (problematicGrandChild.Any())
                {
                    problematicGrandChild.Single()?.flipColor();
                }
                return this;
            }
            else if (tmpLeft != null && tmpLeft.isRed && (tmpRight == null || !tmpRight.isRed))// left child red other black
            {
                var rightChildOfLeftChild = tmpLeft.getRight();
                var leftChildOfLeftChild = tmpLeft.getLeft();
                if (rightChildOfLeftChild != null && rightChildOfLeftChild.isRed)
                {
                    // inner
                    rightChildOfLeftChild.rotateLeft();
                    rightChildOfLeftChild.rotateRight();
                    setRed(true);
                    rightChildOfLeftChild.setRed(false);
                    return rightChildOfLeftChild;
                }
                else if (leftChildOfLeftChild != null && leftChildOfLeftChild.isRed) 
                {
                    // outer
                    tmpLeft.rotateRight();
                    tmpLeft.setRed(false);
                    setRed(true);
                    return tmpLeft;
                }
            }
            else if (tmpRight != null && tmpRight.isRed && (tmpLeft == null || !tmpLeft.isRed))// right child red other black
            {
                var rightChildOfRightChild = tmpRight.getRight();
                var leftChildOfRightChild = tmpRight.getLeft();
                
                if (leftChildOfRightChild != null && leftChildOfRightChild.isRed)
                {
                    // inner
                    leftChildOfRightChild.rotateRight();
                    leftChildOfRightChild.rotateLeft();
                    setRed(true);
                    leftChildOfRightChild.setRed(false);
                    return leftChildOfRightChild;
                }
                else if (rightChildOfRightChild != null && rightChildOfRightChild.isRed)
                {
                    // outer
                    tmpRight.rotateLeft();
                    tmpRight.setRed(false);
                    setRed(true);
                    return tmpRight;
                }
            }
            return this;
        }

        private void rotateRight()
        {
            var tmpRight = getRight();
            var oldParent = getParent();
            replaceBy(tmpRight);
            if (oldParent != null)
            {
                setRight(oldParent);
                becomeParent(oldParent);
            }
        }

        private void rotateLeft()
        {
            var tmpLeft = getLeft();
            var oldParent = getParent();
            replaceBy(tmpLeft);
            if (oldParent != null)
            {
                setLeft(oldParent);
                becomeParent(oldParent);
            }
        }

        private void becomeParent(MyRedBlackNode<T>? node)
        {
            setParent(node?.getParent());
            if (node != null && node.isLeftChild())
            {
                (node?.getParent())?.setLeft(this);
            }
            else if (node != null)
            {
                (node?.getParent())?.setRight(this);
            }
            node?.setParent(this);
        }

        public override MyRedBlackNode<T>? getLeft()
        {
            return (MyRedBlackNode<T>?)base.getLeft();
        }
        public override MyRedBlackNode<T>? getRight()
        {
            return (MyRedBlackNode<T>?)base.getRight();
        }
    }
}
