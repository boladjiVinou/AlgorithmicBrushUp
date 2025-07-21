using Microsoft.Testing.Platform.Extensions.Messages;
using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms
{
    [TestClass]
    public class CountingSortTest
    {
        [TestMethod]
        public void usingCountingSortShouldProperlySortInput() 
        {
            List<int> data = new List<int>();
            for (int i = 11; i >= 0; i--) 
            {
                data.Add(i);
            }

            CountingSort<int>.sort(data);

            for (int i = 0; i < 12; i++) 
            {
                Assert.AreEqual(i, data[i]);
            }
        }

        [TestMethod]
        public void usingCountingSortShouldProperlySortInputWithNegativeValues()
        {
            List<int> data = new List<int>();
            for (int i = 10; i > 0; i--)
            {
                data.Add(-i);
            }

            CountingSort<int>.sort(data);

            for (int i = 10; i >0; i--)
            {
                Assert.AreEqual(-i, data[data.Count-i]);
            }
        }



        [TestMethod]
        public void usingCountingSortShouldProperlySortInputWithNegativeAndPositiveValues()
        {
            List<int> data = new List<int>();
            Random random = new Random();
            for (int i = 20; i > 0; i--)
            {
                data.Add(random.Next(50));
                if (random.Next(2) > 0) 
                {
                    data[data.Count - 1] *= -1;
                }
            }
            List<int> expected = data.OrderBy(x => x).ToList();

            CountingSort<int>.sort(data);

            for (int i = 0; i< data.Count;i++)
            {
                Assert.AreEqual(expected[i], data[i]);
            }
        }
    }
}
