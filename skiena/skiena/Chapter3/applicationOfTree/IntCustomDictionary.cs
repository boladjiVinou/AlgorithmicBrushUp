using skiena.datastructures.lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    public class IntCustomDictionary : IEnumerable<uint>
    {
        private uint[] data;
        private int[] indexes;
        private Queue<int> freeIdx = new Queue<int>();
        private int size = 0;

        public IntCustomDictionary(int m, int n)
        {
            data = new uint[m];
            indexes = new int[n+1];
        }
        public void insert(uint elem)
        {
            if (elem < 1 || elem >= indexes.Length) 
            {
                throw new ArgumentException("value out of accepted range");
            }

            if (size == data.Length) 
            {
                throw new IndexOutOfRangeException("The collection is full");
            }
            indexes[elem] = getIdx(elem);
            data[indexes[elem]] = elem;
            ++size;
        }
        int getIdx(uint elem) 
        {
            if (freeIdx.Count > 0) 
            {
                return freeIdx.Dequeue();
            }
            return size;
        }

        public void Remove(uint elem)
        {
            if (elem < 1 || elem >= indexes.Length)
            {
                throw new ArgumentException("value out of accepted range");
            }
            if (Contains(elem)) 
            {
                freeIdx.Enqueue(indexes[elem]);
                indexes[elem] = -1;
            }
        }

        public bool Contains(uint elem)
        {
            int idx = indexes[elem];
            if (idx < 0) 
            {
                return false;
            }
            return size > 0 && (data[idx] == elem);
        }

        public int getSize()
        {
            return size;
        }

        public IEnumerator<uint> GetEnumerator()
        {
            for (int i = 0; i< indexes.Length; i++)
            {
                if (indexes[i] < 0)
                {
                    continue;
                }
                yield return data[indexes[i]];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
