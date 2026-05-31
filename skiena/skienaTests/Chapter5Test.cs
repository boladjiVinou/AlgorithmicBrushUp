using skiena.Chapter5;
using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class Chapter5Test
    {
        public static IEnumerable<object[]> getChromaticNumberTestInput() 
        {
            yield return new object[] { new List<Tuple<int, int>>(), 0 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1) }, 2};
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }, 2 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1,2), new(2,3), new(3,4), new (4,0) }, 3 };
        }

        [TestMethod]
        [DynamicData(nameof(getChromaticNumberTestInput), DynamicDataSourceType.Method)]
        public void givenADirectedGraph_TheChromaticNumberShouldBeCorrect(List<Tuple<int, int>> connections, int expectedChromaticNumber) 
        {
            var graph = new DirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Assert.AreEqual(expectedChromaticNumber, Chapter5.computeChromaticNumber(graph));
        }
        [TestMethod]
        [DynamicData(nameof(getChromaticNumberTestInput), DynamicDataSourceType.Method)]
        public void givenUndirectedGraph_TheChromaticNumberShouldBeCorrect(List<Tuple<int, int>> connections, int expectedChromaticNumber) 
        {
            var graph = new UndirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Assert.AreEqual(expectedChromaticNumber, Chapter5.computeChromaticNumber(graph));
        }
    }
}
