using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public class RadixSortTest : BaseSortTest
    {
        protected override void sort(List<int> data)
        {
            RadixSort<int>.sort(data,32);
        }
    }
}
