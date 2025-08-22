using skiena.algorithms.sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.algorithms.sorting
{
    [TestClass]
    public class ExternalSortTest:BaseSortTest
    {
        [TestMethod]
        public void whenTryingToSortFileOfInt_thenWeShouldGenerateFileWithSortedData() 
        {
            deleteTempFiles(".");

            Random random = new Random();

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

            deleteTempFiles(".");
        }

        protected override void sort(List<int> data)
        {
            int size = data.Count;
            var folder = $@".\{Guid.NewGuid().ToString()}";
            Directory.CreateDirectory(folder);
            File.WriteAllLines($@"{folder}\data.txt", data.Select(x => x.ToString()));

            ExternalSort<int>.sort($@"{folder}\data.txt", $@"{folder}\", $@"{folder}\result.txt", Math.Min(5, size));

            Assert.IsTrue(File.Exists($@"{folder}\result.txt"));
            var result = ExternalSort<int>.enumerateData($@"{folder}\result.txt");
            
            data.Clear();
            data.AddRange(result);

            Directory.Delete(folder, true);
        }

        private static void deleteTempFiles(string baseFolder)
        {
            if (File.Exists($@"{baseFolder}\data.txt"))
            {
                File.Delete($@"{baseFolder}\data.txt");
            }
            if (File.Exists($@"{baseFolder}\data.txt"))
            {
                File.Delete($@"{baseFolder}\result.txt");
            }
        }
    }
}
