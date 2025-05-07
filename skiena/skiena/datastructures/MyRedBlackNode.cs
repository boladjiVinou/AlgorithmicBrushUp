using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyRedBlackNode<T> : MyBSTNode<T> where T : IEquatable<T>, IComparable<T>
    {
        private int height;
        private bool isRed = true;
        private int blackHeight;
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
            return rebalanceIfNeeded() ?? this;
        }

        private bool checkConsecutiveRed() 
        {
            var tmpParent = (MyRedBlackNode<T>?)getParent();
            return tmpParent != null && isRed && tmpParent.isRed;
        }

        private bool hasRedAunt()
        {
            MyRedBlackNode<T>? aunt = getAunt();
            return aunt != null && aunt.isRed;
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

        /*
        A color flip involves:
        Changing the color of the parent to black
        Changing the color of the uncle to black
        Changing the color of the grandparent to red
        */
        private void flipColor() 
        {
            getParent()?.flipColorFlag();
            getParent()?.getParent()?.flipColorFlag();
            getAunt()?.flipColorFlag();
        }

        private void flipColorFlag() 
        {
            isRed = !isRed;
        }

        public override MyRedBlackNode<T>? getParent() { return (MyRedBlackNode<T> ?)base.getParent(); }

        private MyRedBlackNode<T>? rebalanceIfNeeded()
        {
            int balance = computeBalance();
            if (balance > 1)
            {
                var tmpLeft = getLeft();
                if (tmpLeft?.computeBalance() > 0)// outer insert
                {
                    tmpLeft.rotateRight();
                    return tmpLeft;
                }
                else if (tmpLeft != null) // inner insert
                {
                    var rightChildOfLeftChild = tmpLeft.getRight();
                    rightChildOfLeftChild?.rotateLeft();
                    rightChildOfLeftChild?.rotateRight();
                    return rightChildOfLeftChild;
                }
            }
            else if (balance < -1)
            {
                var tmpRight = getRight();
                if (tmpRight?.computeBalance() < 0)// outer insert
                {
                    tmpRight?.rotateLeft();
                    return tmpRight;
                }
                else if (tmpRight != null) // inner insert
                {
                    var leftChildOfRightChild = tmpRight.getLeft();
                    leftChildOfRightChild?.rotateRight();
                    leftChildOfRightChild?.rotateLeft();
                    return leftChildOfRightChild;
                }
            }
            recomputeHeight();
            recomputeBlackHeight();
            return this;
        }


        private void recomputeHeight()
        {
            height = Math.Max(computeLeftHeight(), computeRightHeight());
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

        private void recomputeBlackHeight()
        {
            blackHeight = Math.Max(computeLeftBlackHeight(), computeRightBlackHeight());
        }

        private int computeRightBlackHeight()
        {
            var tmpRight = getRight();
            if (tmpRight != null)
            {
                return tmpRight.blackHeight + (isRed ? 0 : 1);
            }
            return 0;
        }

        private int computeLeftBlackHeight()
        {
            var tmpLeft = getLeft();
            if (tmpLeft != null)
            {
                return tmpLeft.blackHeight + (isRed ? 0 : 1);
            }
            return 0;
        }
        private void rotateRight()
        {
            var tmpRight = getRight();
            var oldParent = (MyRedBlackNode<T>?)getParent();
            replaceBy(tmpRight);
            if (oldParent != null)
            {
                setRight(oldParent);
                becomeParent(oldParent);
            }

            updateHeights(tmpRight, oldParent);
        }

        private void rotateLeft()
        {
            var tmpLeft = getLeft();
            var oldParent = (MyRedBlackNode<T>?)getParent();
            replaceBy(tmpLeft);
            if (oldParent != null)
            {
                setLeft(oldParent);
                becomeParent(oldParent);
            }

            updateHeights(tmpLeft, oldParent);
        }

        private void updateHeights(MyRedBlackNode<T>? tmpRight, MyRedBlackNode<T>? oldParent)
        {
            List<MyRedBlackNode<T>?> nodesToUpdate =
            [
                tmpRight,
                oldParent,
                this,
                (MyRedBlackNode<T>?)getParent(),
            ];
            foreach (var touchedNodes in nodesToUpdate)
            {
                touchedNodes?.recomputeHeight();
                touchedNodes?.recomputeBlackHeight();
            }
        }
        private void becomeParent(MyRedBlackNode<T>? node)
        {
            setParent(node?.getParent());
            if (node != null && node.isLeftChild())
            {
                ((MyRedBlackNode<T>?)node?.getParent())?.setLeft(this);
            }
            else if (node != null)
            {
                ((MyRedBlackNode<T>?)node?.getParent())?.setRight(this);
            }
            node?.setParent(this);
        }

        private int computeBalance()
        {
            return computeLeftHeight() - computeRightHeight();
        }

        public bool isBalanced()
        {
            return Math.Abs(computeBalance()) <= 1;
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
