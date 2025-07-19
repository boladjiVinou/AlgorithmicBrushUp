using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class CountingSort<T> where T : IBinaryInteger<T>
    {
        public static void sort(List<T> data) 
        {
            countingSortImpl(data, 0, data.Count-1);
        }
        private static void countingSortImpl(List<T> data, int start, int end) 
        {
            if (data.Count == 0 || start<=end) 
            {
                return;
            }
            T min = data[start];
            T max = data[start];
            for (int i = start; i <= end; i++) 
            {
                if (min.CompareTo(data[i]) > 0) 
                {
                    min = data[i];
                }
                if (max.CompareTo(data[i]) < 0) 
                {
                    max = data[i];
                }
            }
            int minComparedToZero = min.CompareTo(0);
            int maxComparedToZero = max.CompareTo(0);
            if ((minComparedToZero >= 0 && maxComparedToZero >= 0) || maxComparedToZero <= 0)
            {
                sameSignCountingSort(data, start, end,min, max);
            }
            else 
            {
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

                sameSignCountingSort(data, start, start + mid-1, negativeMin, negativeMax);
                sameSignCountingSort(data, mid, end, positiveMin, positiveMax);
            }
        }

        private static void sameSignCountingSort(List<T> data, int start, int end, T min, T max) 
        {
            Dictionary<T, ulong> hist = new Dictionary<T, ulong>();
            for (int i = start; i <= end; i++) 
            {
                if (!hist.ContainsKey(data[i])) 
                {
                    hist.Add(data[i], 0);
                }
                ++hist[data[i]];
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
    }
}
