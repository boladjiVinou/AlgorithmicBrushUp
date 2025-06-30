using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MySegmentNode<T> where T : IEquatable<T>, IComparable<T>
    {
        private readonly Func<T, T, T> joinFunction;
        private T modifiableValue;
        public T Value
        {
            get
            {
                return modifiableValue;
            }
        }
        public MySegmentNode<T>? left { get; set; }
        public MySegmentNode<T>? right { get; set; }
        public int start { get; set; }
        public int end { get; set; }

        public MySegmentNode(Func<T, T, T> joinFunction) 
        {
            this.joinFunction = joinFunction;
        }

        public void updateAt(T value, int idx)
        {
            if (start == end && start == idx)
            {
                modifiableValue = value;
            }
            var tmpLeft = left;
            var tmpRight = right;
            if (tmpLeft != null && tmpLeft.end < idx)
            {
                tmpRight?.updateAt(value, idx);
            }
            else
            {
                tmpLeft?.updateAt(value, idx);
            }
            if (tmpRight != null && tmpLeft != null) 
            {
                modifiableValue = joinFunction(tmpLeft.Value, tmpRight.Value);
            }
            else if (tmpRight != null)
            {
                modifiableValue = tmpRight.Value;
            }
            else if (tmpLeft != null)
            {
                modifiableValue = tmpLeft.Value;
            }
        }

        public MySegmentNode<T>? getAt(int idx)
        {
            if (start == end && start == idx)
            {
                return this;
            }
            var tmpLeft = left;
            var tmpRight = right;
            if (tmpLeft != null && tmpLeft.end < idx)
            {
                return tmpRight?.getAt(idx);
            }
            else
            {
                return tmpLeft?.getAt(idx);
            }
        }

        public T getResultBetween(int start, int end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (this.start == start && this.end == end)
            {
                return Value;
            }
            var tmpLeft = this.left;
            var tmpRight = this.right;
            T? leftResult = default;
            T? rightResult = default;
            if (tmpLeft != null && tmpLeft.end < start && tmpRight != null)
            {
                return tmpRight.getResultBetween(start, end);
            }
            if (tmpRight != null && tmpRight.start > end && tmpLeft != null)
            {
                return tmpLeft.getResultBetween(start, end);
            }

            bool leftSubSearchDone = false;
            bool rightSubSearchDone = false;
            if (tmpLeft != null && tmpLeft.start <= start && tmpLeft.end <= end)
            {
                leftSubSearchDone = true;
                leftResult = tmpLeft.getResultBetween(start, tmpLeft.end);
            }
            if (tmpRight != null && tmpRight.start >= start && tmpRight.end >= end)
            {
                rightSubSearchDone = true;
                rightResult = tmpRight.getResultBetween(tmpRight.start, end);
            }

            if (leftSubSearchDone && rightSubSearchDone && leftResult != null && rightResult != null)
            {
                return joinFunction(leftResult, rightResult);
            }
            else if (rightSubSearchDone && rightResult != null)
            {
                return rightResult;
            }
            else if (leftSubSearchDone && leftResult != null)
            {
                return leftResult;
            }
            throw new IndexOutOfRangeException("Cant find the asked value");
        }

        public static MySegmentNode<T>? buildSubTree(Func<T, T, T> joinFunction, T[] data, int start, int end)
        {
            if (start > end)
            {
                return null;
            }
            MySegmentNode<T>? curr = new MySegmentNode<T>(joinFunction);
            curr.start = start;
            curr.end = end;
            if (start == end) 
            {
                curr.modifiableValue = data[start];
                return curr;
            }

            int mid = start + (end - start) / 2;
            curr.left = buildSubTree(joinFunction, data, start, mid);
            curr.right = buildSubTree(joinFunction, data, mid + 1, end);
            if (curr.left != null && curr.right != null)
            {
                curr.modifiableValue = joinFunction(curr.left.Value, curr.right.Value);
            }
            else if (curr.left != null)
            {
                curr.modifiableValue = curr.left.Value;
            }
            else if (curr.right != null)
            {
                curr.modifiableValue = curr.right.Value;
            }
            else
            {
                curr = null;
            }
            return curr;
        }

    }
}
