using skiena.Chapter3;
using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class MySegmentTreeTests
    {
        [TestMethod]
        public void givenASegmentTreeWithDataTheRangeQueryShouldReturnTheRightData() 
        {
            int[] data = { 1, 2, 5, 4, 2, 6, 4, 2 };
            var segmentTree = new MySegmentTree<int>((e1, e2) =>
            {
                int compRes = e1.CompareTo(e2);
                if (compRes <= 0)
                {
                    return e1;
                }
                return e2;
            }, data);

            for (int i = 0; i < data.Length; i++)
            {
                int min = data[i];
                for (int j = i; j < data.Length; j++)
                {
                    if (min > data[j])
                    {
                        min = data[j];
                    }

                    Assert.AreEqual(min, segmentTree.getResultBetween(i, j));
                }
            }
        }

        [TestMethod]
        public void givenASegmentTreeWithDataWhenIdxIsUpdatedTheRangeQueryShouldReturnTheRightData()
        {
            int[] data = { 1, 2, 5, 4, 2, 6, 4, 2 };
            var segmentTree = new MySegmentTree<int>((e1, e2) =>
            {
                int compRes = e1.CompareTo(e2);
                if (compRes <= 0)
                {
                    return e1;
                }
                return e2;
            }, data);

            Random rand = new Random();
            for (int i = 0; i < data.Length; i++) 
            {
                int tmp = rand.Next(100);
                segmentTree.updateAt(tmp,i);
                data[i] = tmp;
            }

            for (int i = 0; i < data.Length; i++)
            {
                int min = data[i];
                for (int j = i; j < data.Length; j++)
                {
                    if (min > data[j])
                    {
                        min = data[j];
                    }

                    Assert.AreEqual(min, segmentTree.getResultBetween(i, j));
                }
            }
        }
    }
}
