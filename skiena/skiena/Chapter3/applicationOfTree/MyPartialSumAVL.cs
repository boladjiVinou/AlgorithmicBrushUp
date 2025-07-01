using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    public class MyPartialSumAVL<M,N> : MyAvlTree<KeyValue<M, N>>
        where M : IEquatable<M>, IComparable<M>
        where N : IEquatable<N>, IComparable<N>, IAdditionOperators<N, N, N>, ISubtractionOperators<N, N, N>
    {
        protected override MyPartialSumAVLNode<KeyValue<M,N>,M,N> createNode(KeyValue<M, N> data)
        {
            return new MyPartialSumAVLNode<KeyValue<M, N>, M, N>(null, data);
        }
        public N? getPartialSum(M key) 
        {
            N totalSum = default;
            if (root != null) 
            {
                totalSum = ((MyPartialSumAVLNode<KeyValue<M, N>, M, N>)root).getSum();
            }
            var foundKey = findKey(key);
            if (foundKey != null) 
            {
                var foundKeyRightChild = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>)foundKey.getRight();
                if (foundKeyRightChild != null)
                {
                    totalSum -= foundKeyRightChild.getSum();
                }
            }
            return totalSum;
        }

        public void insert(M key, N val) 
        {
            add(new KeyValue<M, N>(key, val));
        }

        public void remove(M key)
        {
            root = root?.removeFirst(new KeyValue<M, N>(key, default));
        }
        public MyPartialSumAVLNode<KeyValue<M, N>, M, N>? findKey(M key) 
        {
            var curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)root;
            while (curr != null)
            {
                int compResult = curr.Value.key.CompareTo(key);
                if (compResult > 0)
                {
                    curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)curr.getLeft();
                }
                else if (compResult < 0)
                {
                    curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)curr.getRight();
                }
                else
                {
                    return curr;
                }
            }
            return null;
        }
        public void addTo(M key, N val) 
        {
            var curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)root;
            var tmpData = new KeyValue<M, N>(key, val);
            Stack<MyPartialSumAVLNode<KeyValue<M, N>, M, N>> visitedNodes = new Stack<MyPartialSumAVLNode<KeyValue<M, N>, M, N>>();
            bool foundKey = false;
            while (curr != null) 
            {
                int compResult = curr.Value.CompareTo(tmpData);
                visitedNodes.Push(curr);
                if (compResult > 0)
                {
                    curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)curr.getLeft();
                }
                else if (compResult < 0)
                {
                    curr = (MyPartialSumAVLNode<KeyValue<M, N>, M, N>?)curr.getRight();
                }
                else 
                {
                    curr.Value.associatedValue += val;
                    foundKey = true;
                    break;
                }
            }
            if (foundKey)
            {
                while (visitedNodes.Count > 0) 
                {
                    visitedNodes.Pop().update();
                }
            }
        }
    }
}
