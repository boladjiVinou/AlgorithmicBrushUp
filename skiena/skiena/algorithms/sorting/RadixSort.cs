using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class RadixSort<T>  where T : IBinaryInteger<T>,
       IDivisionOperators<T, T, T>
    {
        public static void sort(List<T> data)
        {
            if (data.Count == 0) 
            {
                return;
            }
            T max = data.Max();
            for (int exp = 1; (max/(T)(object)exp).CompareTo(0) != 0;) 
            {
                sortImpl(data, 0, data.Count - 1,exp);
                checked 
                {
                    try
                    {
                        exp *= 10;
                        T test = (T)(object)exp;
                    }
                    catch (OverflowException e)
                    {
                        break;
                    }
                }
            }
        }

        private static void sortImpl(List<T> data, int start, int end, int exp)
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
                sameSignCountingSort(data, start, end,exp, maxNegative);
            }
            else
            {
                int mid = all.Count;
                all.AddRange(positives);
                for (int i = start; i <= end; i++)
                {
                    data[i] = all[i - start];
                }
                sameSignCountingSort(data, start, start + mid - 1, exp, true);
                sameSignCountingSort(data, mid, end, exp, false);
            }
        }
        private static void sameSignCountingSort(List<T> data, int start, int end, int exp, bool isNegative)
        {
            if (start >= end)
            {
                return;
            }
            int[] count = new int[10];
            for (int i = 0; i < data.Count; i++)
            {
                ++count[computeCountIndex(data[i], exp, isNegative)];
            }
            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }
            T[] result = new T[data.Count];
            for (int j = data.Count - 1; j >= 0; j--)
            {
                int idx = computeCountIndex(data[j], exp, isNegative);
                --count[idx];
                result[count[idx]] = data[j];
            }
            for (int i = 0; i < data.Count; i++)
            {
                data[i] = isNegative ? result[data.Count - i - 1] : result[i];
            }
        }
        private static int computeCountIndex(T value, int exp, bool isNegative)
        {
            checked 
            {
                return (int)(object)(isNegative ?
                         ((-value) / (T)(object)exp % (T)(object)10) :
                         (value / (T)(object)exp) % (T)(object)10);
            }
        }
    }
}
