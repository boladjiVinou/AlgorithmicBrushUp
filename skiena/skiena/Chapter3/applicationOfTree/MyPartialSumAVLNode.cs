using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    public class MyPartialSumAVLNode<T,M,N> (MyAvlNode<T>? ancestor,T val) : MyAvlNode<T>(ancestor, val) 
        where T: KeyValue<M, N>, IEquatable<T>, IComparable<T> 
        where M :  IEquatable<M>, IComparable<M>
        where N : IEquatable<N>, IComparable<N>, IAdditionOperators<N, N, N>, ISubtractionOperators<N, N, N>
    {
        private N? sum = default;
        protected override void recomputeData()
        {
            base.recomputeData();
            sum = Value.associatedValue;
            var left = (MyPartialSumAVLNode<T, M, N>?)getLeft();
            if (left != null && left.sum != null)
            {
                sum += left.sum;
            }
            var right = (MyPartialSumAVLNode<T, M, N>?)getRight();
            if (right != null && right.sum != null) 
            {
                sum += right.sum;
            }
        }
        public override MyPartialSumAVLNode<T, M, N> insert(T val)
        {
            var res = (MyPartialSumAVLNode<T, M, N>)base.insert(val);
            return res;
        }

        public N? getSum() 
        {
            return sum;
        }

        public void update() 
        {
            recomputeData();
        }
        protected override MyPartialSumAVLNode<T,M,N> createChild(T val)
        {
            return new MyPartialSumAVLNode<T, M, N>(this, val);
        }

    }
}
