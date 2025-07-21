using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class RadixSort<T>  where T : IBinaryInteger<T>, ISubtractionOperators<T, T, T>
    {
        public static void sort(List<T> data, int nbOfBitsPerVariable)
        {
            if (data.Count == 0) 
            {
                return;
            }
            T max = data.Max();
            for (int shift = 0; shift < nbOfBitsPerVariable; shift++)
            {
                sortImpl(data, 0, data.Count - 1, shift);
            }
        }

        private static void sortImpl(List<T> data, int start, int end, int shift)
        {
            T min = data[start];
            T max = data[start];

            List<T> all = new List<T>();
            List<T> positives = new List<T>();
            T zero = intAsT(0);
            for (int i = start; i <= end; i++)
            {
                if (data[i]< zero)
                {
                    all.Add(data[i]);
                }
                else
                {
                    positives.Add(data[i]);
                }
                if (data[i]< min)
                {
                    min = data[i];
                }
                if (data[i]> max)
                {
                    max = data[i];
                }
            }
            bool maxNegative = max < zero;
            bool minNegative = min < zero;
            if (maxNegative || (!minNegative && !maxNegative))
            {
                sameSignCountingSort(data, start, end, shift, maxNegative);
            }
            else
            {
                int mid = all.Count;
                foreach (var item in positives)
                {
                    all.Add(item);
                }
                for (int i = start; i <= end; i++)
                {
                    data[i] = all[i - start];
                }
                sameSignCountingSort(data, start, start + mid - 1, shift, true);
                sameSignCountingSort(data, mid, end, shift, false);
            }
        }
        private static void sameSignCountingSort(List<T> data, int start, int end, int shift, bool isNegative)
        {
            if (start >= end)
            {
                return;
            }
            int[] count = new int[2];
            for (int i = 0; i < data.Count; i++)
            {
                ++count[computeCountIndex(data[i], shift, isNegative)];
            }
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }
            T[] result = new T[data.Count];
            for (int j = data.Count - 1; j >= 0; j--)
            {
                int idx = computeCountIndex(data[j], shift, isNegative);
                --count[idx];
                result[count[idx]] = data[j];
            }
            for (int i = 0; i < data.Count; i++)
            {
                data[i] = isNegative ? result[data.Count - i - 1] : result[i];
            }
        }
        private static int computeCountIndex(T value, int shift, bool isNegative)
        {
            checked 
            {
                var bitValue = isNegative ?
                         (-value) & intAsT(1 << shift) :
                         value & intAsT(1 << shift);
                return bitValue != intAsT(0) ? 1 : 0;
            }
        }

        private static T intAsT(int val)
        {
            return (T)(object)val;
        }
    }
}
