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
           
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1) }, 2};
        }

        [TestMethod]
        [DynamicData(nameof(getChromaticNumberTestInput), DynamicDataSourceType.Method)]
        public void chromaticNumberTest(List<Tuple<int, int>> connections, int expectedChromaticNumber) 
        {
            var graph = new DirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Assert.AreEqual(expectedChromaticNumber, graph.computeChromaticNumberGreedy());
        }
    }
}
