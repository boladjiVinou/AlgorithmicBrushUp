using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    /**
    The goal of this data structure is to be able
    to delete kth element
    get kth element
    check existence of element 
    all in log(n) time
    */
    public class MyCustomAvlTree<T> : MyAvlTree<T> where T : IEquatable<T>, IComparable<T>
    {
        protected override MyAvlNode<T> createNode(T val)
        {
            return new MyCustomAvlNode<T>(null, val);
        }

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
                    k -= nbLeft + 1;
                    curr = (MyCustomAvlNode<T>?)curr.getRight();
                }
            }
            return default;
        }
        public IEnumerable<T> iterateOnSmallerThan(T val) 
        {
            var key = findKey(val);
            if (key != null) 
            {
                foreach (var node in inOrderIterationFrom(key.getLeft()))
                {
                    yield return node;
                }
            }
        }
        private MyBSTNode<T>? findKey(T val) 
        {
            var curr = root;
            while (curr != null)
            {
                int compResult = curr.Value.CompareTo(val);
                if (compResult > 0)
                {
                    curr = curr.getLeft();
                }
                else if (compResult < 0)
                {
                    curr = curr.getRight();
                }
                else
                {
                    return curr;
                }
            }
            return null;
        }
        public MyCustomAvlNode<T>? getRoot()
        {
            return (MyCustomAvlNode<T>?)root;
        }
        public void setRoot(MyCustomAvlNode<T>? newRoot)
        {
            root = newRoot;
        }

    }
}
