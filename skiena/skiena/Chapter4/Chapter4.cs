using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public class Chapter4
    {
        public static void exercice_4_1(List<int> players) 
        {
            players.Sort();// worst team 0...n-1, best team n...2n-1
        }
        public static Tuple<int,int> findMaxPairFromUnsortedArray(List<int> numbers) 
        {
            return new(numbers.Max(), numbers.Min());
        }
        public static Tuple<int, int> findMaxPairFromSortedArray(List<int> numbers) 
        {
            return new Tuple<int, int>(numbers[numbers.Count - 1], numbers[0]);
        }
        public static Tuple<int, int> findMinPairFromUnsortedArray(List<int> numbers) 
        {
            List<int> data = new(numbers);
            data.Sort((x,y) => Math.Abs(x-y));
            return new(data[0], data[1]);
        }
        public static Tuple<int, int> findMinPairFromSortedArray(List<int> numbers) 
        {
            int first = 0;
            int second = 0;
            int dist = int.MaxValue;
            for (int i = 1; i < numbers.Count; i++) 
            {
                int delta = numbers[i] - numbers[i - 1];
                if (delta< dist) 
                {
                    dist = delta;
                    first = i - 1;
                    second = i;
                }
            }
            return new Tuple<int, int>(first, second);
        }
        // 4.3
        public static List<Tuple<int, int>> partitionList(List<int> numbers) 
        {
            List<int> data = new List<int>(numbers);
            data.Sort();
            List<Tuple<int, int>> partition = [];
            for (int i = 0; i < numbers.Count / 2; i++) 
            {
                partition.Add(new Tuple<int, int>(numbers[i], numbers[numbers.Count - i - 1]));
            }
            return partition;
        }
        // 4.4
        public static void sortByColorAndNumber(List<Tuple<int, Color>> data)
        {
            data.Sort((x, y) =>
            {
                int colorComparison = x.Item2.CompareTo(y.Item2);
                if (colorComparison == 0)
                {
                    return x.Item1 - y.Item1;
                }
                return colorComparison;
            });
        }
        //4.5
        public static int findTheMode(List<int> numbers) 
        {
            Dictionary<int,int> numbersByOccurence = new Dictionary<int,int>();
            int mode = 0;
            int maxCount = 0;
            for (int i = 0; i < numbers.Count; i++) 
            {
                if (!numbersByOccurence.ContainsKey(numbers[i])) 
                {
                    numbersByOccurence.Add(numbers[i], 0);
                }
                ++numbersByOccurence[numbers[i]];
                if (numbersByOccurence[numbers[i]] > maxCount) 
                {
                    maxCount = numbersByOccurence[numbers[i]];
                    mode = numbers[i];
                }
            }
            return mode;
        }
        //4.6
        public static bool findPairEqualTargetInNSquare(List<int> list1, List<int> list2, int target)
        {
            for (int i = 0; i < list1.Count; i++) 
            {
                for (int j = 0; j < list2.Count; j++) 
                {
                    if (list1[i] + list2[j] == target) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool findPairEqualTargetInNLogN(List<int> list1, List<int> list2, int target)
        {
            list2.Sort();
            for (int i = 0; i < list1.Count; i++)
            {
                int start = 0;
                int end = list2.Count - 1;
                int mid = 0;
                while (start < end)
                {
                    mid = start + (end - start) / 2;
                    int sum = list2[mid] + list1[i];
                    if (sum > target)
                    {
                        start = mid + 1;
                    }
                    else if (sum < target)
                    {
                        end = mid - 1;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool findPairEqualTargetInN(List<int> list1, List<int> list2, int target)
        {
            HashSet<int> data = [.. list1];
            for (int j = 0; j < list2.Count; j++)
            {
                if (data.Contains(target - list2[j]))
                {
                    return true;
                }
            }
            return false;
        }
        // 4.7


    }
}
