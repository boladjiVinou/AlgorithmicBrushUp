using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class MyDisjointSetTests
    {
        [TestMethod]
        public void whenTwoNodesAreConnected_ThenTheyShouldHaveSameRoot() 
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyDisjointSet<int> disjointSet = new MyDisjointSet<int>(data);
            disjointSet.connect(2, 10);

            Assert.IsTrue(disjointSet.areConnected(2, 10));
            Assert.AreEqual(disjointSet.findRoot(2), disjointSet.findRoot(10));
        }
        [TestMethod]
        public void whenTwoNodesArentConnected_ThenTheyShouldNotHaveSameRoot()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyDisjointSet<int> disjointSet = new MyDisjointSet<int>(data);
            disjointSet.connect(2, 10);
            disjointSet.connect(1, 3);
            disjointSet.connect(7, 6);
            disjointSet.connect(8, 5);

            Assert.IsTrue(disjointSet.areConnected(2, 10));
            Assert.AreNotEqual(disjointSet.findRoot(4), disjointSet.findRoot(8));
        }
    }
}
