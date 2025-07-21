using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public class QuickSortTest : BaseSortTest
    {
        protected override void sort(List<int> data)
        {
            Quicksort<int>.sort(data);
        }
    }
}
