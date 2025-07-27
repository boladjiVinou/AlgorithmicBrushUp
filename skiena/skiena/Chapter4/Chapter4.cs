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
            return new Tuple<int, int>(numbers[first], numbers[second]);
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
        public static List<PhoneData> findWhoDidntPaidBill(List<PhoneData> phoneBills, List<PhoneCheck> checks)
        {
            HashSet<int> checkSet = [.. checks.Select(x=>x.phoneId)];
            return phoneBills.Where(x => !checkSet.Contains(x.id)).ToList();
        }
        public static Dictionary<string, int> findNumberOfBookPerCompany(List<BookMetadata> books, List<string> publishers) 
        {
            Dictionary<string, int> nbBookPerPublisher = [];
            for (int i = 0; i < publishers.Count; i++) 
            {
                nbBookPerPublisher.Add(publishers[i], 0);
            }

            for (int i = 0; i < books.Count; i++) 
            {
                if (nbBookPerPublisher.ContainsKey(books[i].publisher))
                {
                    ++nbBookPerPublisher[books[i].publisher];
                }
            }
            return nbBookPerPublisher;
        }

        public static int findNbDistinctPersonHavingCheckout(List<string> checkouts) 
        {
            return checkouts.Distinct().Count();
        }
        //4.8
        public static bool findTwoElementsEqualSumInUnsortedSet(ISet<double> set, double target) 
        {
            var sortedSet = set.Order().ToList();
            for(int i = 0; i< sortedSet.Count;i++)
            {
                int searchResult = -1;
                searchResult = sortedSet.BinarySearch(0,i,target - sortedSet[i],null);
                if (searchResult  >= 0) 
                {
                    return true;
                }
                searchResult = sortedSet.BinarySearch(i+1, sortedSet.Count - i, target - sortedSet[i], null);
                if (searchResult >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool findTwoElementsEqualSumInSortedSet(List<double> set, double target)
        {
            int p1 = 0;
            int p2 = set.Count-1;
            for (; p1 < p2;)
            {
                double sum = set[p1] + set[p2];
                if (sum < target)
                {
                    ++p1;
                }
                else if (sum > target)
                {
                    --p2;
                }
                else 
                {
                    return true;
                }
            }
            return false;
        }
        // 4.9
        public static HashSet<int> findIntersectionInUnsortedSet(ISet<int> set1, ISet<int> set2) 
        {
            var sortedSet2 = set2.Order().ToList();
            HashSet<int> intersection = new HashSet<int>();
            foreach (var elem in set1) 
            {
                int searchResult = sortedSet2.BinarySearch(elem);
                if (searchResult >= 0 && !intersection.Contains(sortedSet2[searchResult])) 
                {
                    intersection.Add(sortedSet2[searchResult]);
                }
            }
            return intersection;
        }
        public static List<int> findIntersectionInSortedSet(List<int> set1, List<int> set2) 
        {
            List<int> result = new List<int>();
            int p1 = 0;
            int p2 = 0;
            for (;p1 < set1.Count && p2 < set2.Count;) 
            {
                if (set1[p1] < set2[p2])
                {
                    ++p1;
                }
                else if (set1[p1] > set2[p2])
                {
                    ++p2;
                }
                else if (result.Count == 0 || (result.Count > 0 && result[result.Count - 1] < set1[p1]))
                {
                    result.Add(set1[p1]);
                }
            }
            return result;
        }
        // 4.10
        public static bool doesKNumberInSetAddUpToTarget(List<int> sortedNumbers, int idx,long currSum,int k, int target) 
        {
            long sum = currSum + sortedNumbers[idx];
            if (k == 1)
            {
                int numberToFind = (int)(target - sum);
                return sortedNumbers.BinarySearch(idx+1,sortedNumbers.Count-idx-1,numberToFind,null) >= 0;
            }
            else if(k > 1)
            {
                for (int i = idx; i < sortedNumbers.Count - 1; i++)
                {
                    if (doesKNumberInSetAddUpToTarget(sortedNumbers, i + 1, sum, k - 1, target)) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // 4.11
        public static List<int> findElementsThatAppearMoreThanKTime(List<int> numbers, int k) 
        {
            // Boyer moore algorithm
            List<int>  result = new List<int>();
            if (k > 0) 
            {
                int[] candidates = new int[k - 1];
                int[] count = new int[k - 1];

                Array.Fill(candidates, int.MinValue);

                int tresholdCount = numbers.Count / k;

                for (int i = 0; i < numbers.Count; i++)
                {
                    bool foundCandidate = false;
                    // increase weight of 1st matching candidate
                    for (int j = 0; j < candidates.Length && !foundCandidate; j++) 
                    {
                        if (numbers[i] == candidates[j])
                        {
                            ++count[j];
                            foundCandidate = true;
                        }
                    }
                    // if no candidate found , search for 1st candidate with weight null and replace it by current number
                    for (int j = 0; j < count.Length && !foundCandidate; j++)
                    {
                        if (count[j] == 0)
                        {
                            ++count[j];
                            candidates[j] = numbers[i];
                            foundCandidate = true;
                        }
                    }
                    // if no candidate replaced, decrease the weight of all the current candidates
                    if (!foundCandidate)
                    {
                        for (int j = 0; j < count.Length; j++)
                        {
                            --count[j];
                        }
                    }
                }
                // among candidates seach for the one exceeding the treshold
                for (int j = 0; j < count.Length; j++) 
                {
                    if (count[j] > tresholdCount) 
                    {
                        result.Add(candidates[j]);
                    }
                }
            }
            return result;
        }
        
    }
}
