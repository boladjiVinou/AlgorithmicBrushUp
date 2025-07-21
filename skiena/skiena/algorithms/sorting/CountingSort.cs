using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class CountingSort<T> where T : IBinaryInteger<T>, ISubtractionOperators<T,T,T>
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
            T zero = intAsT(0);
            for (int i = start; i <= end; i++)
            {
                if (!hist.ContainsKey(data[i]))
                {
                    hist.Add(data[i], 0);
                }
                ++hist[data[i]];

                if ((min - data[i]) > zero)
                {
                    min = data[i];
                }
                if ((max - data[i]) < zero)
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
            T zero = intAsT(0);
            for (int i = start; i <= end; i++)
            {
                if ((data[i] - zero) < zero)
                {
                    all.Add(data[i]);
                }
                else
                {
                    positives.Add(data[i]);
                }
                if (min > data[i]) 
                {
                    min = data[i];
                }
                if (data[i] > max) 
                {
                    max = data[i];
                }
            }
            bool maxNegative = max < zero;
            bool minNegative = min < zero;
            if (maxNegative || (!minNegative && !maxNegative))
            {
                sameSignCountingSort(data, start, end, min, max, maxNegative);
            }
            else 
            {
                int mid = all.Count;
                foreach (var item in positives)
                {
                    all.Add(item);
                }
                T negativeMin = min;
                T negativeMax = all[0];
                T positiveMin = positives[0];
                T positiveMax = max;
                for (int i = start; i <= end; i++)
                {
                    data[i] = all[i - start];
                    if (i < mid)
                    {
                        if (data[i] > negativeMax)
                        {
                            negativeMax = data[i];
                        }
                    }
                    else
                    {
                        if (data[i] < positiveMin)
                        {
                            positiveMin = data[i];
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
            for (int i = start; i <=end; i++) 
            {
                ++count[computeCountIndex(data[i], min,max,isNegative)];
            }
            for (int i = 1;i< count.Length; i++) 
            {
                count[i] += count[i - 1];
            }
            T[] result = new T[end-start+1];
            for (int j =end; j >= start; j--) 
            {
                int idx = computeCountIndex(data[j], min, max, isNegative);
                --count[idx];
                result[count[idx]] = data[j];
            }
            for (int i = start; i <= end; i++) 
            {
                data[i] =  result[i-start];
            }
        }
        private static int computeCountIndex(T value, T min, T max, bool isNegative) 
        {
            return isNegative ? tAsInt(value - min): tAsInt(value - min);
        }
        private static int computeSpan(T min, T max, bool isNegative) 
        {
            return isNegative ? tAsInt((-min) - (-max)) + 1 : tAsInt(max -min) + 1;
        }
        private static T intAsT(int val) 
        {
            return (T)(object)val;
        }
        private static int tAsInt(T val) 
        {
            return (int)(object)val;
        }
    }
}
