using Microsoft.Testing.Platform.Extensions.Messages;
using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public class CountingSortTest : BaseSortTest
    {
        protected override void sort(List<int> data) 
        {
            CountingSort<int>.sort(data);
        }
    }
}
