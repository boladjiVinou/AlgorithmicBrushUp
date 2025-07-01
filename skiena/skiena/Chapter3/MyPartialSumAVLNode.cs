using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3
{
    public class MyPartialSumAVLNode (MyAvlNode<KeyValue<int, long>>? ancestor, KeyValue<int, long> val) : MyAvlNode<KeyValue<int, long>>(ancestor, val)
    {
        private long sum = default;
        protected override void recomputeData()
        {
            base.recomputeData();
            sum = Value.associatedValue;
            var left = getLeft();
            if (left != null)
            {
                sum += ((MyPartialSumAVLNode)left).sum;
            }
            var right = getRight();
            if (right != null) 
            {
                sum += ((MyPartialSumAVLNode)right).sum;
            }
        }

        public long getPartialSum() 
        {
            long rightSum = 0;
            var right = (MyPartialSumAVLNode?)getRight();
            if (right != null) 
            {
                rightSum = right.sum;
            }
            return sum - rightSum - Value.associatedValue;
        }

        public void update() 
        {
            recomputeData();
        }
        protected override MyPartialSumAVLNode createChild(KeyValue<int, long> val)
        {
            return new MyPartialSumAVLNode(this, val);
        }

    }
}
