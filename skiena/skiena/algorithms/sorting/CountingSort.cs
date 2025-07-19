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
            countingSortWithDictionary(data, start, end, min, max);
        }

        private static void countingSortWithDictionary(List<T> data, int start, int end, T min, T max) 
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
