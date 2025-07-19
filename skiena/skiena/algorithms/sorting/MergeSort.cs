using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class MergeSort<T> where T : IEquatable<T>, IComparable<T>
    {
        public static void sort(List<T> data) 
        {
            mergeSort(data, 0, data.Count - 1);
        }

        private static void mergeSort(List<T> data, int start, int end)
        {
            if (start >= end) 
            {
                return;
            }
            int mid = start + (end - start) / 2;
            mergeSort(data, start, mid);
            mergeSort(data, mid + 1, end);
            merge(data, start, mid, mid + 1, end);
        }

        private static void merge(List<T> data, int start1, int end1, int start2, int end2) 
        {
            T[] merged = new T[data.Count];
            int i = start1;
            int j = start2;
            int count = 0;
            for (; i <= end1 && j <= end2;) 
            {
                int comparison = data[i].CompareTo(data[j]);
                if (comparison > 0)
                {
                    merged[count] = data[j];
                    j++;
                }
                else
                {
                    merged[count] = data[i];
                    ++i;
                }
                ++count;
            }
            while (i <= end1) 
            {
                merged[count] = data[i];
                ++i;
                ++count;
            }
            while (j <= end2)
            {
                merged[count] = data[j];
                ++j;
                ++count;
            }
            for (int k = 0; k < merged.Length; k++) 
            {
                data[k] = merged[k];
            }
        }
    }
}
