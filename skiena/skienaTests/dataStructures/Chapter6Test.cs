using skiena.Chapter6;
using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class Chapter6Test
    {
        [TestMethod]
        public void givenAGraphWithNonMSTedge_WeShouldFindSmallestChangeForNewMST()
        {
            WeightedUndirectedGraph<char, int> graph = new();
            graph.connect('a', 'b', 1)
                .connect('b', 'c', 1)
                .connect('a', 'c', 2);

            var result = Chapter6.getMinimalChangeToGetDifferentMST(graph);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Item3);
            Assert.IsTrue(result.Item1 != 'b' && result.Item2 != 'b');
        }
        [TestMethod]
        public void givenMstWithMaxWeightOutsideCycle_WeShouldFindSmallestChangeForNewMst() 
        {
            WeightedUndirectedGraph<char, int> graph = new();
            graph.connect('a', 'b', 1)
                .connect('b', 'c', 1)
                .connect('a', 'c', 2)
                .connect('e', 'a', 1000)
                .connect('c','f',1999);

            var result = Chapter6.getMinimalChangeToGetDifferentMST(graph);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Item3);
            Assert.IsTrue(result.Item1 != 'b' && result.Item2 != 'b');
        }
    }
}
