using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3
{
    public class MyCustomAvlNode<T>(MyAvlNode<T>? ancestor, T val) : MyAvlNode<T>(ancestor, val) where T : IEquatable<T>, IComparable<T>
    {
        private int nbLeftChild;
        private int nbRightChild;

        protected override void recomputeData()
        {
            base.recomputeData();
            nbLeftChild = computeLeftChildren();
            nbRightChild = computeRightChildren();
        }

        public int getNbChildren() 
        {
            return nbLeftChild + nbRightChild;
        }

        protected override MyCustomAvlNode<T> createChild(T val)
        {
            return new MyCustomAvlNode<T>(this, val);
        }

        private int computeRightChildren()
        {
            var tmpRight = (MyCustomAvlNode<T>?)getRight();
            if (tmpRight != null)
            {
                return  tmpRight.getNbChildren() + 1;
            }
            return 0;
        }

        private int computeLeftChildren()
        {
            var tmpLeft = (MyCustomAvlNode<T>?)getLeft();
            if (tmpLeft != null)
            {
                return tmpLeft.getNbChildren() + 1;
            }
            return 0;
        }

        public int getNbLeftChildren() 
        {
            return nbLeftChild;
        }
        public int getNbRightChildren() 
        {
            return nbRightChild;
        }
        /*
         * Delete the asked node in subtree, and return the rebalanced replacement node
         */
        public MyCustomAvlNode<T>? removeNode(MyCustomAvlNode<T> nodeToRemove)
        {
            if (nodeToRemove == null) 
            {
                return this;
            }
            if (this == nodeToRemove)
            {
                var tmpParent = (MyCustomAvlNode<T>?)getParent();
                if (left == null && right != null)
                {
                    replaceCurrentReferenceInTreeBy(right);
                    return (MyCustomAvlNode<T>?)right;
                }
                else if (right == null && left != null)
                {
                    replaceCurrentReferenceInTreeBy(left);
                    return (MyCustomAvlNode<T>?)left;
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
                    return (MyCustomAvlNode<T>)rebalanceIfNeeded();
                }
                else
                {
                    // the current node is the node to delete and has no children
                    replaceCurrentReferenceInTreeBy(null);
                }
                return null;
            }
            else
            {
                var compRes = Value.CompareTo(nodeToRemove.Value);
                if (compRes > 0)
                {
                    left = ((MyCustomAvlNode<T>?)left)?.removeNode(nodeToRemove)?.rebalanceIfNeeded();
                }
                else 
                {
                    right = ((MyCustomAvlNode<T>?)right)?.removeNode(nodeToRemove)?.rebalanceIfNeeded();
                }
                return (MyCustomAvlNode<T>)rebalanceIfNeeded();
            }
        }
    }
}
