using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class CountingSort<T> where T : IBinaryInteger<T>
    {
        public static void unstableSort(List<T> data) 
        {
            unstableCountingSortImpl(data, 0, data.Count-1);
        }
        private static void unstableCountingSortImpl(List<T> data, int start, int end) 
        {
            if (data.Count == 0 || start<=end) 
            {
                return;
            }
            T min = data[start];
            T max = data[start];

            Dictionary<T, ulong> hist = new Dictionary<T, ulong>();
            for (int i = start; i <= end; i++)
            {
                if (!hist.ContainsKey(data[i]))
                {
                    hist.Add(data[i], 0);
                }
                ++hist[data[i]];

                if (min.CompareTo(data[i]) > 0)
                {
                    min = data[i];
                }
                if (max.CompareTo(data[i]) < 0)
                {
                    max = data[i];
                }
            }
            int idx = start;
            for (T i = min; i <= max; i++)
            {
                if (!hist.ContainsKey(i))
                {
                    continue;
                }
                for (ulong j = 0; j < hist[i]; j++)
                {
                    data[idx] = i;
                    ++idx;
                }
            }
        }
        public static void sort(List<T> data) 
        {
            stableSortImpl(data, 0, data.Count - 1);
        }

        private static void stableSortImpl(List<T> data, int start,int end) 
        {
            T min = data[start];
            T max = data[start];

            List<T> all = new List<T>();
            List<T> positives = new List<T>();
            for (int i = start; i <= end; i++)
            {
                if (data[i].CompareTo(0) < 0)
                {
                    all.Add(data[i]);
                }
                else
                {
                    positives.Add(data[i]);
                }
            }
            bool maxNegative = max.CompareTo(0) < 0;
            if (maxNegative || (min.CompareTo(0) >= 0 && !maxNegative))
            {
                sameSignCountingSort(data, start, end, min, max, maxNegative);
            }
            else 
            {
                int mid = all.Count;
                all.AddRange(positives);
                T negativeMin = all[0];
                T negativeMax = all[0];
                T positiveMin = positives[0];
                T positiveMax = positives[0];
                for (int i = start; i <= end; i++)
                {
                    data[i] = all[i - start];
                    if (i < mid)
                    {
                        if (negativeMin.CompareTo(data[i]) > 0)
                        {
                            negativeMin = data[i];
                        }
                        if (data[i].CompareTo(negativeMax) > 0)
                        {
                            negativeMax = data[i];
                        }
                    }
                    else
                    {
                        if (positiveMin.CompareTo(data[i]) > 0)
                        {
                            positiveMin = data[i];
                        }
                        if (data[i].CompareTo(positiveMax) > 0)
                        {
                            positiveMax = data[i];
                        }
                    }
                }
                sameSignCountingSort(data, start, start + mid - 1, negativeMin, negativeMax,true);
                sameSignCountingSort(data, mid, end, positiveMin, positiveMax, false);
            }
        }
        private static void sameSignCountingSort(List<T> data, int start, int end, T min, T max, bool isNegative) 
        {
            if (start >= end) 
            {
                return;
            }
            int[] count = new int[computeSpan(min,max,isNegative)];
            for (int i = 0; i < data.Count; i++) 
            {
                ++count[computeCountIndex(data[i], min,max,isNegative)];
            }
            for (int i = 1;i< count.Length; i++) 
            {
                count[i] += count[i - 1];
            }
            T[] result = new T[data.Count];
            for (int j = data.Count - 1; j >= 0; j--) 
            {
                int idx = computeCountIndex(data[j], min, max, isNegative);
                --count[idx];
                result[count[idx]] = data[j];
            }
            for (int i = 0; i < data.Count; i++) 
            {
                data[i] = result[i];
            }
        }
        private static int computeCountIndex(T value, T min, T max, bool isNegative) 
        {
            return isNegative ? (-max).CompareTo(-value):value.CompareTo(min);
        }
        private static int computeSpan(T min, T max, bool isNegative) 
        {
            return isNegative ? (-min).CompareTo(-max) + 1 : max.CompareTo(min) + 1;
        }
    }
}
