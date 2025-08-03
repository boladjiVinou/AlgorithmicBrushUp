using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class Quicksort<T> where T : IEquatable<T>, IComparable<T>
    {
        public static void sort(List<T> data) 
        {
            quickSort(data, 0, data.Count-1);
        }
        private static void quickSort(List<T> data, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(data, start, end);

            quickSort(data, start, pivot);
            quickSort(data, pivot + 1, end);
        }

        public static void sort(List<T> data, Comparer<T> comparer)
        {
            quickSort(data, 0, data.Count - 1,comparer);
        }
        private static void quickSort(List<T> data, int start, int end, Comparer<T> comparer)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = partition(data, start, end,comparer);

            quickSort(data, start, pivot,comparer);
            quickSort(data, pivot + 1, end, comparer);
        }
        public static int partition(List<T> data, int start, int end, Comparer<T> comparer) 
        {
            int pivot = start + (end - start) / 2;
            int left = pivot - 1;
            int right = pivot + 1;
            for (; left >= start && right <= end;)
            {
                int leftComp = comparer.Compare(data[left], data[pivot]);
                int rightComp = comparer.Compare(data[right], data[pivot]);
                if (leftComp >= 0 && rightComp < 0)
                {
                    swap(data, left, right);
                    --left;
                    ++right;
                }
                else if (leftComp < 0)
                {
                    --left;
                }
                else if (rightComp >= 0)
                {
                    ++right;
                }
            }
            while (left >= start)
            {
                int leftComp = comparer.Compare(data[left], data[pivot]);
                if (leftComp >= 0)
                {
                    int newPivot = pivot - 1;
                    swap(data, left, pivot);
                    if (newPivot >= start)
                    {
                        swap(data, newPivot, left);
                        pivot = newPivot;
                    }
                }
                --left;
            }
            while (right <= end)
            {
                int rightComp = comparer.Compare(data[right], data[pivot]);
                if (rightComp < 0)
                {
                    int newPivot = pivot + 1;
                    swap(data, right, pivot);
                    if (newPivot <= end)
                    {
                        swap(data, right, newPivot);
                        pivot = newPivot;
                    }
                }
                ++right;
            }

            return pivot;
        }
        public static int partition(List<T> data, int start, int end)
        {
            return partition(data, start, end,Comparer<T>.Default);
        }

        private static void swap(List<T> data, int i, int j) 
        {
            T tmp = data[i];
            data[i] = data[j];
            data[j] = tmp;
        }
        public static T quickSelect(List<T> data, int k) 
        {
            return quickSelectImpl(data, k, 0, data.Count - 1);
        }
        private static T quickSelectImpl(List<T> data, int k, int start, int end) 
        {
            if (k >= data.Count || k < 0) 
            {
                throw new ArgumentException("Out of range value");
            }
            int pivot = start + (end - start) / 2;
            int left = pivot - 1;
            int right = pivot + 1;
            for (; left >= start && right <= end;)
            {
                int leftComp = data[left].CompareTo(data[pivot]);
                int rightComp = data[right].CompareTo(data[pivot]);
                if (leftComp >= 0 && rightComp < 0)
                {
                    swap(data, left, right);
                    --left;
                    ++right;
                }
                else if (leftComp < 0)
                {
                    --left;
                }
                else if (rightComp >= 0)
                {
                    ++right;
                }
            }
            while (left >= start)
            {
                int leftComp = data[left].CompareTo(data[pivot]);
                if (leftComp >= 0)
                {
                    int newPivot = pivot - 1;
                    swap(data, left, pivot);
                    if (newPivot >= start)
                    {
                        swap(data, newPivot, left);
                        pivot = newPivot;
                    }
                }
                --left;
            }
            while (right <= end)
            {
                int rightComp = data[right].CompareTo(data[pivot]);
                if (rightComp < 0)
                {
                    int newPivot = pivot + 1;
                    swap(data, right, pivot);
                    if (newPivot <= end)
                    {
                        swap(data, right, newPivot);
                        pivot = newPivot;
                    }
                }
                ++right;
            }
            if (k > pivot) 
            {
                return quickSelectImpl(data, k, pivot + 1, end);
            }
            else if (k < pivot)
            {
                return quickSelectImpl(data, k, start, pivot - 1);
            }
            return data[k];
        }
    }
}
