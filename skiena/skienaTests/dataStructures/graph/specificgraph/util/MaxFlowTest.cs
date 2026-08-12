using skiena.datastructures.graph.specificgraph;
using skiena.datastructures.graph.specificgraph.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures.graph.specificgraph.util
{

    [TestClass]
    public class MaxFlowTest
    {
        [TestMethod]
        public void givenAGraphWithSinglePath_WhenSearchingForMaxFlowWeShouldFindIt() 
        {
            WeightedDirectedGraph<char, int> graph = new();
            graph.connect('a', 'b', 10)
                .connect('b', 'c', 5);

            int maxFlow = GraphUtils<char, int>.computeMaximumFlow(graph, 'a', 'c');

            Assert.AreEqual(5,maxFlow);
        }

        [TestMethod]
        public void givenAGraphWithTwoPath_WeShouldFindMaxFlow() 
        {
            WeightedDirectedGraph<char, int> graph = new();
            graph.connect('a', 'b', 10)
                .connect('b', 'd', 5)
                .connect('a', 'c', 7)
                .connect('c', 'd', 7);

            int maxFlow = GraphUtils<char, int>.computeMaximumFlow(graph, 'a', 'd');

            Assert.AreEqual(12,maxFlow);
        }

        [TestMethod]
        public void givenAGraphWithMultiplePath_WeShouldFindTheMaxFlow() 
        {
            WeightedDirectedGraph<char, int> graph = new();
            graph.connect('s', 'a', 10)
                .connect('a', 'b', 5)
                .connect('b', 't', 7)
                .connect('s', 'c', 8)
                .connect('c', 'd', 10)
                .connect('d', 't', 10)
                .connect('a', 'c', 2)
                .connect('d', 'b', 8);

            int maxFlow = GraphUtils<char, int>.computeMaximumFlow(graph, 's', 't');

            Assert.AreEqual(15,maxFlow);
        }
    }
}
