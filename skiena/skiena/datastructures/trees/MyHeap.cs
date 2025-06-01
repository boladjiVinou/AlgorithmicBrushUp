using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
    public class MyHeap<T> where T : IEquatable<T>, IComparable<T>
    {
        private List<T> data = [];
        private int nbElements;
        private readonly Comparer<T>? comparer;
        public MyHeap() 
        {
            data.Add(default);
        }
        public MyHeap(Comparer<T> comparer):this()
        {
            this.comparer = comparer;
        }
        public T removeTop() 
        {
            if (nbElements == 0)
            {
                throw new InvalidOperationException("empty Heap");
            }
            T maxElement = data[1];
            if (nbElements - 1 > 0)
            {
                swap(1, nbElements);
                --nbElements;
                maxHeapifyTopDown();
            }
            else 
            {
                --nbElements;
            }
           return maxElement;
        }
        public T peekTop()
        {
            if (nbElements == 0) 
            {
                throw new InvalidOperationException("empty Heap");
            }
            return data[1];
        }

        public void insert(T element) 
        {
            ++nbElements;
            if (nbElements < data.Count)
            {
                data[nbElements] = element;
            }
            else 
            {
                data.Add(element);
            }
            maxHeapifyBottomUp();
        }

        private void maxHeapifyBottomUp() 
        {
            ensureHeapPropertyBottomUp(nbElements/2);
        }
        private void maxHeapifyTopDown() 
        {
            ensureHeapPropertyTopDown(1);
        }

        private void ensureHeapPropertyTopDown(int index)
        {
            int i = index;
            while (i < nbElements) 
            {
                int leftChildIdx = 2 * i;
                int rightChildIdx = leftChildIdx + 1;
                int maxIdx = i;
                if (leftChildIdx <= nbElements && compare(data[maxIdx], data[leftChildIdx]) < 0)
                {
                    maxIdx = leftChildIdx;
                }
                if (rightChildIdx <= nbElements &&  compare(data[maxIdx], data[rightChildIdx]) < 0)
                {
                    maxIdx = rightChildIdx;
                }
                if (i != maxIdx)
                {
                    swap(i, maxIdx);
                    i = maxIdx;
                }
                else 
                {
                    break;
                }
            }
        }

        private void ensureHeapPropertyBottomUp(int index) 
        {
            if (index == 0) 
            {
                return;
            }
            int i = index;
            while (i > 0) 
            {
                int leftChildIdx = 2 * i;
                int rightChildIdx = leftChildIdx + 1;
                int maxIdx = i;
                if (leftChildIdx <= nbElements && compare(data[maxIdx], data[leftChildIdx]) < 0)
                {
                    maxIdx = leftChildIdx;
                }
                if (rightChildIdx <= nbElements && compare(data[maxIdx], data[rightChildIdx]) < 0)
                {
                    maxIdx = rightChildIdx;
                }
                if (maxIdx != i)
                {
                    swap(i, maxIdx);
                    i /= 2;
                }
                else 
                {
                    break;
                }
            }
           
        }
        private void swap(int i, int j) 
        {
            if (i < nbElements && j <= nbElements) 
            {
                T tmp = data[i];
                data[i] = data[j];
                data[j] = tmp;
            }
        }

        public int getSize() 
        {
            return nbElements;
        }

        private int compare(T t1, T t2) 
        {
            if (comparer != null) 
            {
                return comparer.Compare(t1, t2);
            }
            return t1.CompareTo(t2);
        }
    }
}
