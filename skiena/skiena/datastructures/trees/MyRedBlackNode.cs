using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    // course: https://courses.ms.wits.ac.za/~steve/aaa/book/large/RedBlack.html
    public class MyRedBlackNode<T> : MyBSTNode<T> where T : IEquatable<T>, IComparable<T>
    {
        private enum Color 
        {
            Red,
            Black,
            DoubleBlack
        }
        private Color color = Color.Red;
        private class MyNullRedBlackNode: MyRedBlackNode<T>
        {
            public MyNullRedBlackNode(MyBSTNode<T>? ancestor, T val) : base(ancestor, val)
            {
                color = Color.Black;
            }
        }
        public MyRedBlackNode(MyBSTNode<T>? ancestor, T val) : base(ancestor, val)
        {
            color = ancestor == null ? Color.Black : Color.Red;
        }

        public bool isNullNodeInstance() 
        {
            return this is MyNullRedBlackNode;
        }
        private MyRedBlackNode<T> createNullNode() 
        {
            return new MyNullRedBlackNode(getParent(), default);
        }
        protected override MyRedBlackNode<T> createChild(T val)
        {
            return new MyRedBlackNode<T>(this, val);
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
                    left = left?.insert(val);
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
                    right = right?.insert(val);
                }
            }
            return rebalanceIfNeededAfterInsertion();
        }

        public static bool isNodeNull(MyBSTNode<T>? node)
        {
            return node == null || asRedBlackNode(node).isNullNodeInstance();
        }

        private MyRedBlackNode<T>? getAunt()
        {
            var tmpParent = getParent();
            if (tmpParent == null)
            {
                return null;
            }
            var tmpGrandParent = tmpParent.getParent();
            MyRedBlackNode<T>? aunt;
            if (tmpParent.isLeftChild())
            {
                aunt = tmpGrandParent?.getRight();
            }
            else
            {
                aunt = tmpGrandParent?.getLeft();
            }

            return aunt;
        }

        private void flipColor() 
        {
            getParent()?.setColor(Color.Black);
            getParent()?.getParent()?.setColor(Color.Red);
            getAunt()?.setColor(Color.Black);
        }
        public bool isRed() 
        {
            return color == Color.Red;
        }
        private void updateRootColorIfNeeded() 
        {
            if (getParent() == null && isRed())
            {
                color = Color.Black;
            }
        }
        private void setColor(Color color) 
        {
            this.color = color;
        }
        public override MyRedBlackNode<T>? getParent() 
        { 
            return (MyRedBlackNode<T> ?)base.getParent(); 
        }

        private bool isRoot() 
        {
            return getParent() == null;
        }
        private MyRedBlackNode<T> rebalanceIfNeededAfterInsertion()
        {
            // I fix conflicts by being on grand parent level
            if (isRoot() && isRed())
            {
                updateRootColorIfNeeded();
                return this;
            }
            var tmpLeft = getLeft();
            var tmpRight = getRight();
            if (tmpLeft != null && tmpRight != null && tmpLeft.isRed() && tmpRight.isRed())// two red children
            {
                List<MyRedBlackNode<T>?> grandChildren = new List<MyRedBlackNode<T>?>()
                { tmpLeft.getLeft(),tmpLeft.getRight(), tmpRight.getLeft(), tmpRight.getRight()};
                var problematicGrandChild =  grandChildren.Where(x => x != null && x.isRed());
                if (problematicGrandChild.Any())
                {
                    problematicGrandChild.Single()?.flipColor();
                }
                return this;
            }
            else if (tmpLeft != null && tmpLeft.isRed() && (tmpRight == null || !tmpRight.isRed()))// left child red other black
            {
                var rightChildOfLeftChild = tmpLeft.getRight();
                var leftChildOfLeftChild = tmpLeft.getLeft();
                if (rightChildOfLeftChild != null && rightChildOfLeftChild.isRed())
                {
                    // inner
                    rightChildOfLeftChild.rotateLeft();
                    rightChildOfLeftChild.rotateRight();
                    setColor(Color.Red);
                    rightChildOfLeftChild.setColor(Color.Black);
                    return rightChildOfLeftChild;
                }
                else if (leftChildOfLeftChild != null && leftChildOfLeftChild.isRed())
                {
                    // outer
                    tmpLeft.rotateRight();
                    tmpLeft.setColor(Color.Black);
                    setColor(Color.Red);
                    return tmpLeft;
                }
            }
            else if (tmpRight != null && tmpRight.isRed() && (tmpLeft == null || !tmpLeft.isRed()))// right child red other black
            {
                var rightChildOfRightChild = tmpRight.getRight();
                var leftChildOfRightChild = tmpRight.getLeft();
                
                if (leftChildOfRightChild != null && leftChildOfRightChild.isRed())
                {
                    // inner
                    leftChildOfRightChild.rotateRight();
                    leftChildOfRightChild.rotateLeft();
                    setColor(Color.Red);
                    leftChildOfRightChild.setColor(Color.Black);
                    return leftChildOfRightChild;
                }
                else if (rightChildOfRightChild != null && rightChildOfRightChild.isRed())
                {
                    // outer
                    tmpRight.rotateLeft();
                    tmpRight.setColor(Color.Black);
                    setColor(Color.Red);
                    return tmpRight;
                }
            }
            return this;
        }

        private void rotateRight()
        {
            var tmpRight = getRight();
            var oldParent = getParent();
            replaceCurrentReferenceInTreeBy(tmpRight);
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
            replaceCurrentReferenceInTreeBy(tmpLeft);
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

        public override MyBSTNode<T>? removeFirst(T val)
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
                MyRedBlackNode<T>? tmpRight = isRed() ? asRedBlackNode(right): recolorReplacingNode(right);
                return tmpRight?.repairIfNeededAfterDeletion();
            }
            else if (right == null && left != null)
            {
                replaceCurrentReferenceInTreeBy(left);
                var tmpLeft = isRed() ? asRedBlackNode(left) : recolorReplacingNode(left);
                return tmpLeft?.repairIfNeededAfterDeletion();
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
                if (!isRed() && getParent() != null)
                {
                    return recolorReplacingNode(null);
                }
                else 
                {
                    return null;
                }
            }
            return asRedBlackNode(this)?.repairIfNeededAfterDeletion();
        }

        private MyRedBlackNode<T> recolorReplacingNode(MyBSTNode<T>? node)
        {
            var tmpNode = asRedBlackNode(node);

            if (tmpNode == null)
            {
                tmpNode = createNullNode();
            }
            tmpNode.setColor(!tmpNode.isRed() && !isRed() ? Color.DoubleBlack : Color.Black);

            return tmpNode;
        }

        static  MyRedBlackNode<T>? asRedBlackNode(MyBSTNode<T>? node) 
        {
            return (MyRedBlackNode<T>?)node;
        }

        private MyRedBlackNode<T> repairIfNeededAfterDeletion() 
        {
            //I fix issues from double black node's parent level
            if (getParent() == null) 
            {
                updateRootColorIfNeeded();
            }
            if (color == Color.DoubleBlack) 
            {
                return this; // will be fixed on parent level
            }
            var tmpLeft = getLeft();
            var tmpRight = getRight();
            if (tmpLeft != null && tmpLeft.color == Color.DoubleBlack)
            {
                // Case 1 sibling is red
                if (tmpRight != null && tmpRight.color == Color.Red)
                {
                    setColor(Color.Red);
                    tmpRight.setColor(Color.Black);
                    tmpRight?.rotateLeft();
                }
                tmpRight = getRight();
                var rightChildOfRightChild = tmpRight?.getRight();
                var leftChildOfRightChild = tmpRight?.getLeft();
                List<MyRedBlackNode<T>?> rightChildren = new List<MyRedBlackNode<T>?>()
                {
                    rightChildOfRightChild,leftChildOfRightChild
                };
                // case 2 sibling is black all its children black
                if (tmpRight == null || tmpRight.color == Color.Black && rightChildren.All(x => x == null || !x.isRed()))
                {
                    tmpRight?.setColor(Color.Red);
                    setColor(isRed() ? Color.Black : Color.DoubleBlack);
                }
                // case 3 sibling black inner child red
                else if (tmpRight != null && tmpRight.color == Color.Black && (leftChildOfRightChild?.isRed()).GetValueOrDefault())
                {
                    tmpRight.setColor(Color.Red);
                    leftChildOfRightChild?.setColor(Color.Black);
                    leftChildOfRightChild?.rotateRight();
                    return repairIfNeededAfterDeletion(); 
                }
                // case 4 sibling black outer child red
                else if (tmpRight != null && tmpRight.color == Color.Black && (rightChildOfRightChild?.isRed()).GetValueOrDefault())
                {
                    tmpRight.setColor(color);
                    setColor(Color.Black);
                    rightChildOfRightChild?.setColor(Color.Black);
                    tmpRight.rotateLeft();
                }

                removeDoubleBlackColor(tmpLeft);
            }
            else if (tmpRight != null && tmpRight.color == Color.DoubleBlack) 
            {
                // Case 1 sibling is red
                if (tmpLeft != null && tmpLeft.color == Color.Red)
                {
                    setColor(Color.Red);
                    tmpLeft.setColor(Color.Black);
                    tmpLeft?.rotateRight();
                }
                tmpLeft = getLeft();
                var rightChildOfLeftChild = tmpLeft?.getRight();
                var leftChildOfLeftChild = tmpLeft?.getLeft();
                List<MyRedBlackNode<T>?> leftChildren = new List<MyRedBlackNode<T>?>()
                {
                    rightChildOfLeftChild, leftChildOfLeftChild
                };
                // case 2 sibling is black all its children black
                if (tmpLeft == null || tmpLeft.color == Color.Black && leftChildren.All(x => x == null || !x.isRed()))
                {
                    tmpLeft?.setColor(Color.Red);
                    setColor(isRed() ? Color.Black : Color.DoubleBlack);
                }
                // case 3  sibling black  inner child red
                else if (tmpLeft != null && tmpLeft.color == Color.Black && (rightChildOfLeftChild?.isRed()).GetValueOrDefault())
                {
                    tmpLeft.setColor(Color.Red);
                    rightChildOfLeftChild?.setColor(Color.Black);
                    rightChildOfLeftChild?.rotateLeft();
                    return repairIfNeededAfterDeletion();
                }
                // case 4  sibling black outer child red
                else if (tmpLeft != null && tmpLeft.color == Color.Black && (leftChildOfLeftChild?.isRed()).GetValueOrDefault())
                {
                    tmpLeft.setColor(color);
                    setColor(Color.Black);
                    leftChildOfLeftChild?.setColor(Color.Black);
                    tmpLeft.rotateRight();
                }

                removeDoubleBlackColor(tmpRight);
            }
            return this;
        }

        private void removeDoubleBlackColor(MyRedBlackNode<T> node)
        {
            node.setColor(Color.Black);// removing double black flag
            if (node.isNullNodeInstance()) // destroy Nil node
            {
                if (node.isLeftChild())
                {
                    node.getParent()?.setLeft(null);
                }
                else 
                {
                    node.getParent()?.setRight(null);
                }
            }
        }

        public int countBlackPathLength() 
        {
            var tmpLeft = asRedBlackNode(left);
            var tmpRight = asRedBlackNode(right);
            int leftLength = 0;
            int rightLength = 0;
            if (tmpLeft != null) 
            {
                leftLength = tmpLeft.countBlackPathLength();
            }
            if (tmpRight != null)
            {
                rightLength = tmpRight.countBlackPathLength();
            }
            if (leftLength != rightLength) 
            {
                throw new InvalidDataException("Different lengths of black path on a node");
            }
            return leftLength + (isRed() ? 0 : 1);
        }
    }
}
