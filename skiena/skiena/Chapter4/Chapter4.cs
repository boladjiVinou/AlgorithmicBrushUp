using Microsoft.VisualBasic;
using skiena.algorithms.sorting;
using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
            for (int i = 0; i < data.Count / 2; i++) 
            {
                partition.Add(new Tuple<int, int>(data[i], data[data.Count - i - 1]));
            }
            return partition;
        }
        // 4.4
        public static void sortByColorAndNumber(List<Tuple<int, Color>> sortedByNumberData)
        {
            Dictionary<Color, List<Tuple<int, Color>>> elemByColor = new Dictionary<Color, List<Tuple<int, Color>>>();
            for (int i = 0; i < sortedByNumberData.Count; i++)
            {
                if (!elemByColor.ContainsKey(sortedByNumberData[i].Item2))
                {
                    elemByColor.Add(sortedByNumberData[i].Item2, new List<Tuple<int, Color>>());
                }
                elemByColor[sortedByNumberData[i].Item2].Add(sortedByNumberData[i]);
            }
            int j = 0;
            if (elemByColor.ContainsKey(Color.Red))
            {
                foreach (var elem in elemByColor[Color.Red])
                {
                    sortedByNumberData[j] = elem;
                    ++j;
                }
            }
            if (elemByColor.ContainsKey(Color.Blue))
            { 
                foreach (var elem in elemByColor[Color.Blue])
                {
                    sortedByNumberData[j] = elem;
                    ++j;
                }
            }
            if (elemByColor.ContainsKey(Color.Yellow))
            {
                foreach (var elem in elemByColor[Color.Yellow])
                {
                    sortedByNumberData[j] = elem;
                    ++j;
                }
            }
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
                while (start <= end)
                {
                    mid = start + (end - start) / 2;
                    int sum = list2[mid] + list1[i];
                    if (sum > target)
                    {
                        end = mid - 1;
                    }
                    else if (sum < target)
                    {
                        start = mid + 1;
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
            var checkSet = checks.ToLookup(x => x.phoneId);
            return phoneBills.Where(x => !checkSet.Contains(x.id) || x.price > checkSet[x.id].First().amount).ToList();
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
                searchResult = sortedSet.BinarySearch(0,i,target - sortedSet[i], new EpsilonComparer(0.000001) );
                if (searchResult  >= 0) 
                {
                    return true;
                }
                searchResult = sortedSet.BinarySearch(i+1, sortedSet.Count - i-1, target - sortedSet[i], new EpsilonComparer(0.000001));
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
                else
                {
                    result.Add(set1[p1]);
                    ++p1;
                    ++p2;
                }
            }
            return result;
        }
        // 4.10
        public static bool doesKNumberInSetAddUpToTarget(List<int> sortedNumbers, int idx,long currSum,int k, int target) 
        {
            if (k == 0 && target == 0) 
            {
                return true;
            }
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
                for (int i = 0; i < count.Length; i++) 
                {
                    count[i] = 0;
                }
                for (int i = 0; i < numbers.Count; i++) 
                {
                    for (int j = 0; j < candidates.Length; j++) 
                    {
                        if (numbers[i] == candidates[j]) 
                        {
                            ++count[j];
                            break;
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
        // 4.12
        //Constraint: Should be in O(n + klogn)
        public static List<int> findKSmallestFromUnsortedSet(IEnumerable<int> data, int k) 
        {
            MyHeap<int> heap = new MyHeap<int>();
            foreach (var elem in data) 
            {
                heap.insert(-elem);
            }

            List<int> result = new List<int>();
            for (int i = 0; i < k; i++) 
            {
                result.Add(-heap.removeTop());
            }

            return result;
        }

        /*
         4.13
        a) Doesnt matter, with both structure we can get the maximum in constant time, but with heap if max we cant 
        get the minimum in constant time
        b) The sorted array is better, the heap doesnt allow the removal of a random element , we can only remove the top 
        element
        c) The heap can be built in O(n), the sorted array in O(nlogn) in general or O(n) if using specialized algorithms
        depending on the kind of number set
        d) Doesnt matter, with both structure we can get the minimum in constant time, but with heap if min we cant
        get the maximum in constant time

         */
        // 4.14
        public static List<int> mergeKSortedLists(List<List<int>> sortedLists) 
        {
            List<int> result = new List<int>();
            int[] indexes = new int[sortedLists.Count];
            MyHeap<SortedElement> heap = new MyHeap<SortedElement>();
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] == sortedLists[i].Count)
                {
                    continue;
                }
                heap.insert(new SortedElement(-sortedLists[i][indexes[i]], i));
            }
            while (heap.getSize() > 0) 
            {
                var elem = heap.removeTop();
                result.Add(-elem.val);
                ++indexes[elem.listIdx];
                if (indexes[elem.listIdx] < sortedLists[elem.listIdx].Count)
                {
                    heap.insert(new SortedElement(-sortedLists[elem.listIdx][indexes[elem.listIdx]], elem.listIdx));
                }
            }
            return result;

        }

        // 4.15
        /*
         Creating a heap from data at initialisation is better,
        because we iterate from n/2 to 0 and 2 comparisons
        this lead to n comparison in total,
        then poping k biggest key, after a key removal
        2*log(n-1) comparison are done
        which give us n + 2klog(n-k) comparisons in total,
        yes the algorithm determine the largest then second largest etc
         */
        public static List<int> getKBiggest(IEnumerable<int> data, int k)
        {
            MyHeap<int> myHeap = new MyHeap<int>(data);
            List<int> result = new List<int>();
            for (int i = 0; i < k && myHeap.getSize() > 0; i++)
            {
                result.Add(myHeap.removeTop());
            }
            return result;
        }
        // 4.16
        public static int findMedian(List<int> data) 
        {
            return Quicksort<int>.quickSelect(data, data.Count / 2);
        }
        //4.17
        /*
         a)
        T(n) = n + 2T(n/2) -> O(nlogn) in worst case from master theorem
         
         b) 
        T(n) = n + T(n/3) + T(2n/3)
        cant be solved with master theorem
        we know that at each level we have to do n comparison,
        let s find the height
        the largest subset is 2n/3
        the largest subset of this subset will be 4n/9
        we can deduce that at the bottom level (2/3) ^ h * n
        (2/3)^h =1/n
        log((2/3) ^ h) = log(1/n)
        h log(2/3) = log(1/n)
        h = -log(n) / log(2/3)
        h = log(n)/log(3/2)
        h = log3/2(n)
        The worst case complexity is nlog3/2(n)
         */

        // 4.18
        // 0: red, 1: white, 2: blue
        public static void threeColorSorting(List<int> data) 
        {
            if (data.Any(x => x < 0 || x > 2)) 
            {
                throw new ArgumentOutOfRangeException();
            }
            int firstPivot = Quicksort<int>.partition(data, 0, data.Count - 1);
            if (data[firstPivot] == 2 && firstPivot-1 > 0) 
            {
                Quicksort<int>.partition(data, 0, firstPivot - 1);
            }
            else if (data[firstPivot] == 1 && data.Count-1 > firstPivot+1)
            {
                Quicksort<int>.partition(data, firstPivot + 1, data.Count - 1);
            }
        }
        //4.19

        //4.20
        // O(n)
        public static void sortPositiveAndNegative(List<int> data) 
        {
            int pivot = Quicksort<int>.partition(data, 0, data.Count - 1, new SignComparer());
            if (data[pivot] > 0)
            {
                Quicksort<int>.partition(data, 0, pivot - 1);
            }
            else if (data[pivot] == 0 && data.Count - 1 > pivot + 1)
            {
                Quicksort<int>.partition(data, pivot + 1, data.Count - 1);
            }
        }
        //4.21
        /*
         when merging always take the number from left side on equality
         */
        /*
         4.22
        By using radix sort we can achieve the complexity of  nlogk
        we know that the number of bits of  k = ceil(log2(k))
        we can thus do ceil(log2(k)) passes of radix sort, for each bit of k
        leading to a complexity of nlogk

         */
        // 4.23
        // the worst complexity is o(n) which is still below the worst complexity requested
        public static void sortDataContainingDuplicates(List<int> data) 
        {
            var hist = data
                .GroupBy(x => x)
                .Select(x => new { value = x.First(), count = x.Count()})
                .ToDictionary(x => x.value, y => y.count);
            var distincts = hist.Keys.ToList();//n

            Quicksort<int>.sort(distincts);// logn*loglogn if logn distincts

            for (int i = 1; i < distincts.Count; i++) 
            {
                hist[distincts[i]] += hist[distincts[i - 1]];
            }
            int[] result = new int[data.Count];
            for (int i = data.Count-1; i >=0; i--) 
            {
                --hist[data[i]];
                result[hist[data[i]]] = data[i];
            }
            for (int i = 0; i < data.Count; i++)
            {
                data[i] = result[i];
            }
        }
        /*
        4.24
         n-sqrt(n) first elements are sorted, we just need to sort the sqrt(n) remaining numbers
        they can be sorted in sqrt(n)Log(sqrt(n)) -> sqrt(n)Log(n)
        they can also be sorted in sqrt(n) complexity with counting sort,
        we then need to merge this subpart with the rest, which give us an overall complexity of o(n)
         
        4.25
        The algorithm of 4.23 can be reused here
         a) a quicksort's partition from middle by putting element x<y to left and the rest to right
        if the value at the returnt pivot is 0, rerun the partition on interval[pivot+1, n-1]
        b)


        4.28

        We can achieve it by sorting sqrt(n) elements sqrt(n) times
        and then merge each subset of size sqrt(n) by using algorithm done in  4.14 k being sqrt(n)

        4.29
        If this algorithm exists it means that heap sort will have a worst case complexity of o(n)
        which is impossible

        4.30
        40% connect 60% of time
        Option1:
        all names sorted -> n names sorted -> binary search log(n) -> 14 calls
        Option2:
        4n/10 names sorted | 6n/10 names sorted -> 60% of time binary search log(4n/10), 40% time binary search log(4n/10) + log(6n/10)
        60% of time 12 calls required and for 40% others  13 +12 calls -> 25 calls in total

        The gain is not significant with option2 on this example size, option1 is still the best

        if the list is not sorted at all the option2 is the best, because in worst case with option1 we will do n calls
        but with option2 in 60% of cases we will only do 4000 calls and in 40% of cases we will do 10000 calls which is the worst case of option 1


        4.31
        * if k is known the greatest number is at index k-1
        * 
         */
        public static int findMaxInCircularlyShiftedList(List<int>data) 
        {
            int start = 0;
            int end = data.Count - 1;
            int mid = 0;
            int max = int.MinValue;
            while (start <= end) 
            {
                mid = start + (end - start) / 2;
                max = Math.Max(max, data[mid]);
                if (data[mid] >= data[start])
                {
                    start = mid + 1;
                }
                else if (data[start] >= data[end])
                {
                    end = mid - 1;
                }
                else 
                {
                    throw new Exception("Unplanned scenario");
                }
            }
            return max;
        }

        /*
         4.32 a) If n is known the best strategy is to do a binary search of the value between 1 and n
        b)if n is not known the best strategy is to make  a guess and ask if it is greater than the chosen value
        if yes, upperBound = value, nextValue = Math.Max(lowerBound+1, value*2)
        if smaller, lowerBound = value,nextValue = Math.Min(upperBound-1, value*2), 
        if equal we found it
         */
        //4.33
        public static int findIdxBeingEqualToAssignedValueInSortedArray(List<int>data) 
        {
            int start = 0;
            int end = data.Count - 1;
            int mid = 0;
            while (start <= end) 
            {
                mid = start + ((end - start) / 2);
                if (data[mid] < 0) 
                {
                    start = mid + 1;
                    continue;
                }
                if (mid - 1 >= 0 && data[mid] <= mid - 1)
                {
                    start = mid + 1;
                }
                else if (mid + 1 <= data.Count-1 && data[mid] >= mid + 1)
                {
                    end = mid - 1;
                }
                else
                {
                    return mid; // means mid-1 <data[mid]<mid+1
                }
            }
            return -1;
        }
        // 4.34
        public static int findSmallestAbsentNumberFromSortedList(List<int> data, int m) 
        {
            if (data.Count == 0) 
            {
                return -1;
            }
            if (data[0] > 1) 
            {
                return 1;
            }
            int start = 0;
            int end = data.Count - 1;
            int mid = 0;
            while (start < end)
            {
                mid = start + ((end - start) / 2);
                if ( data[mid] > mid + 1)
                {
                    end = mid - 1;
                }
                else if (data[mid] <= mid + 1)
                {
                    start = mid + 1;
                }
            }
            if (start > 0 && (data[start] - data[start - 1]) > 1)
            {
                return data[start - 1] + 1;
            }
            return data[start] + 1;
        }
        //4.35 complexity [log(m*n)]^2
        public static Tuple<int,int> findPositionOfValue(int[][] matrix, int value) 
        {
            if (matrix.Length == 0 || matrix[0].Length == 0) 
            {
                return new Tuple<int, int>(-1, -1);
            }
            return findPositionOfValue(matrix, value, 0, matrix.Length - 1, 0, matrix[0].Length - 1);
        }
        private static Tuple<int, int> findPositionOfValue(int[][] matrix, int value, int rStart, int rEnd, int colStart, int colEnd)
        {
            int rowStart = rStart;
            int rowEnd = rEnd;
            int columnStart = colStart;
            int columnEnd = colEnd;
            int rowMid = 0;
            int colMid = 0;
            while ((rowStart < rowEnd && colStart <= colEnd) || (rowStart <= rowEnd && colStart < colEnd))
            {
                rowMid = rowStart + (rowEnd - rowStart) / 2;
                colMid = columnStart + (columnEnd - columnStart) / 2;
                if (matrix[rowMid][colMid] > value)
                {
                    var tmpCol = Array.BinarySearch(matrix[rowMid], colStart, colMid, value);
                    if (tmpCol >= 0)
                    {
                        return Tuple.Create(rowMid, tmpCol);//same row left result
                    }
                    var bottomLeftCornerResult = findPositionOfValue(matrix, value, rowMid+1, rowEnd, colStart, colMid);
                    if (bottomLeftCornerResult.Item1 > 0 &&  bottomLeftCornerResult.Item2  > 0)
                    {
                        return bottomLeftCornerResult;
                     
                    }
                    rowEnd = rowMid - 1;
                }
                else if (matrix[rowMid][colMid] < value)
                {
                    if (colEnd > colMid) 
                    {
                        var tmpCol = Array.BinarySearch(matrix[rowMid], colMid, colEnd - colMid + 1, value);
                        if (tmpCol >= 0)
                        {
                            return Tuple.Create(rowMid, tmpCol);// same row right result
                        }
                    }
                    var topRightCornerResult = findPositionOfValue(matrix, value, rowStart, rowMid-1, colMid, colEnd);
                    if (topRightCornerResult.Item1 >= 0 || topRightCornerResult.Item2>= 0)
                    {
                        return topRightCornerResult;
                    }
                    rowStart = rowMid + 1;
                }
                else
                {
                    return new Tuple<int, int>(rowMid, colMid);
                }
            }
            if (rowStart == rowEnd && colStart == colEnd && matrix[rowStart][colStart] == value)
            {
                return Tuple.Create(rowStart, colStart);
            }
            return new Tuple<int, int>(-1, -1);
        }
    }
}
