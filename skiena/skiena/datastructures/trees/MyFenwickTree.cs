using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace skiena.datastructures.trees
{
    // https://cp-algorithms.com/data_structures/fenwick.html
    public class MyFenwickTree
    {
        private List<long> partialSum;

        public MyFenwickTree(int size)
        {
            partialSum = new List<long>(size);
            for (int i = 0; i < size; i++)
            {
                partialSum.Add(0);
            }
        }
        public MyFenwickTree(List<int> data)
        {
            partialSum = new List<long>(data.Count);
            for (int i = 0; i < data.Count; i++) 
            {
                partialSum.Add(0);
            }
            for (int i = 0; i < data.Count; i++) 
            {
                partialSum[i] += data[i];
                int tmp = h(i);
                if (tmp < data.Count) 
                {
                    partialSum[tmp] += partialSum[i];
                }
            }
        }
        /* replacing all trailing  1  bits in the binary representation of  idx  with  0 bits*/
        private int g(int idx) 
        {
            return idx & (idx + 1);
        }
        //  flipping the last unset bit
        private int h(int idx) 
        {
            return idx | (idx + 1);
        }

        public void add(int idx,int val)
        {
            for (int i = idx; i < partialSum.Count; i = h(i)) 
            {
                partialSum[i] += val;
            }
        }
        public long getSum(int idx)
        {
            long res = 0;
            while (idx >= 0) 
            {
                res += partialSum[idx];
                idx = g(idx) - 1;
            }
            return res;
        }

        public long getSum(int start, int end) 
        {
            return getSum(end) - getSum(start-1);
        }
    }
}
