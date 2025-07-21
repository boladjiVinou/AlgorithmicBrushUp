using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public abstract class BaseSortTest
    {
        [TestMethod]
        public void usingSortShouldProperlySortInput()
        {
            List<int> data = new List<int>();
            for (int i = 11; i >= 0; i--)
            {
                data.Add(i);
            }

            sort(data);

            for (int i = 0; i < 12; i++)
            {
                Assert.AreEqual(i, data[i]);
            }
        }

        [TestMethod]
        public void usingSortShouldProperlySortInputWithNegativeValues()
        {
            List<int> data = new List<int>();
            for (int i = 10; i > 0; i--)
            {
                data.Add(-i);
            }

            sort(data);

            for (int i = 10; i > 0; i--)
            {
                Assert.AreEqual(-i, data[data.Count - i]);
            }
        }



        [TestMethod]
        public void usingSortShouldProperlySortInputWithNegativeAndPositiveValues()
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

            sort(data);

            for (int i = 0; i < data.Count; i++)
            {
                Assert.AreEqual(expected[i], data[i]);
            }
        }

        protected abstract void sort(List<int> data);
    }
}
