using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3
{
    public class MyCustomAvlTree<T> : MyAvlTree<T> where T : IEquatable<T>, IComparable<T>
    {
        public T? getKthSmallestData(int k) 
        {
            var node = getKthSmallest(k);
            if (node != null) 
            {
                return node.Value;
            }
            return default;
        }

        public void deleteKthSmallest(int k) 
        {
            var node = getKthSmallest(k);
            if (node != null)
            {
                root = ((MyCustomAvlNode<T>?)root)?.removeNode(node);
            }
        }

        private MyCustomAvlNode<T>? getKthSmallest(int k) 
        {
            var curr = (MyCustomAvlNode<T>?)root;
            while (curr != null)
            {
                int nbLeft = curr.getNbLeftChildren();
                int nbRight = curr.getNbRightChildren();
                if (k <= nbLeft)
                {
                    curr = (MyCustomAvlNode<T>?)curr.getLeft();
                }
                else if (k == nbLeft + 1)
                {
                    return curr;
                }
                else
                {
                    curr = (MyCustomAvlNode<T>?)curr.getRight();
                }
            }
            return default;
        }
    }
}
