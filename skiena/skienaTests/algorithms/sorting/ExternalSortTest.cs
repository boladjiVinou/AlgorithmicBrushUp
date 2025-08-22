using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public class ExternalSortTest
    {
        [TestMethod]
        public void whenTryingToSortFileOfInt_thenWeShouldGenerateFileWithSortedData() 
        {
            Random random = new Random();
            File.Delete(@".\data.txt");
            File.Delete(@".\result.txt");

            int size = 1000;
            int parts = 10;
            File.WriteAllLines(@".\data.txt", Enumerable.Range(1, size).Select(x => random.Next(x).ToString()));

            ExternalSort<int>.sort(@".\data.txt", @".\", @".\result.txt", size/ parts);

            Assert.IsTrue(File.Exists(@".\result.txt"));
            var result = ExternalSort<int>.enumerateData(@".\result.txt");
            Assert.IsTrue(result.Any());
            int prev = int.MinValue;
            foreach (var elem in result) 
            {
                Assert.IsTrue(prev <= elem);
                prev = elem;
            }

            File.Delete(@".\data.txt");
            File.Delete(@".\result.txt");
        }
    }
}
