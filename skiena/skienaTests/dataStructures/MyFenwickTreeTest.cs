using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{

    [TestClass]
    public class MyFenwickTreeTest
    {
        [TestMethod]
        public void whenWeInitializeATreeWithData_thenTheSumShouldBeCalculated() 
        {
            List<int> data = new List<int>();
            for (int i = 1; i < 11; i++)
            {
                data.Add(i);
            }
            MyFenwickTree tree = new MyFenwickTree(data);
            Assert.AreEqual(5 * 6 / 2, tree.getSum(4));
        }
        [TestMethod]
        public void whenWeInitializeATreeWithoutData_thenTheSumShouldBeCalculated()
        {
            MyFenwickTree tree = new MyFenwickTree(10);
            for (int i = 0; i < 10; i++) 
            {
                tree.add(i, i+1);
            }
            
            Assert.AreEqual(5 * 6 / 2, tree.getSum(4));
        }
    }
}
