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

        public void remove()
        {
            if (left == null && right != null)
            {
                replaceCurrentReferenceInTreeBy(right);
            }
            else if (right == null && left != null)
            {
                replaceCurrentReferenceInTreeBy(left);
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
                replaceCurrentReferenceInTreeBy(null);
            }
        }
    }
}
