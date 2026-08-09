using skiena.datastructures.graph;
using skiena.datastructures.graph.specificgraph;
using skiena.datastructures.graph.specificgraph.util;

namespace skienaTests.dataStructures.graph.specificgraph.util
{
    [TestClass]
    public class FloydWarshallTest
    {
        [TestMethod]
        public void givenUndirectedGraph_whenSearchingForShortestPathAmongPairs_WeShouldFindIt() 
        {
            var graph = new WeightedUndirectedGraph<int, int>();
            graph.connect(0, 1, 5)
                .connect(0, 3, 10)
                .connect(1, 2, 3)
                .connect(2, 3, 1);
            var expectedDistanceMatrix = new Dictionary<int, Dictionary<int, int>>();
            for (int i = 0; i < 4; i++) 
            {
                expectedDistanceMatrix.Add(i, []);
            }
            expectedDistanceMatrix[0].Add(0, 0);
            expectedDistanceMatrix[0].Add(1, 5);
            expectedDistanceMatrix[0].Add(2, 8);
            expectedDistanceMatrix[0].Add(3, 9);

            expectedDistanceMatrix[1].Add(0, 5);
            expectedDistanceMatrix[1].Add(1, 0);
            expectedDistanceMatrix[1].Add(2, 3);
            expectedDistanceMatrix[1].Add(3, 4);

            expectedDistanceMatrix[2].Add(0, 8);
            expectedDistanceMatrix[2].Add(1, 3);
            expectedDistanceMatrix[2].Add(2, 0);
            expectedDistanceMatrix[2].Add(3, 1);

            expectedDistanceMatrix[3].Add(0, 9);
            expectedDistanceMatrix[3].Add(1, 4);
            expectedDistanceMatrix[3].Add(2, 1);
            expectedDistanceMatrix[3].Add(3, 0);

            var adjacencyMatrix = GraphUtils<int, int>.computeShortestPathBetweenAllPair(graph, out Dictionary<int,Dictionary<int,int>> distanceByPair);

            foreach(var v in expectedDistanceMatrix.Keys) 
            {
                foreach(var w in expectedDistanceMatrix.Keys) 
                {
                    Assert.AreEqual(expectedDistanceMatrix[v][w], distanceByPair[v][w]);
                }
            }
            Assert.AreEqual(1,adjacencyMatrix[0][2]);
            Assert.AreEqual(2,adjacencyMatrix[0][3]);
            Assert.AreEqual(2,adjacencyMatrix[1][3]);
            Assert.AreEqual(1, adjacencyMatrix[2][0]);
            Assert.AreEqual(2, adjacencyMatrix[3][0]);
            Assert.AreEqual(2, adjacencyMatrix[3][1]);
        }
        [TestMethod]
        public void givenAGraphWithUnreachableNode_whenSearchingForShortestPathAmongPairs_WeShouldNotOverflow()
        {
            var graph = new WeightedUndirectedGraph<int, int>();
            graph.connect(0, 1, 4);
            graph.insertNode(2);

            var adjacencyMatrix = GraphUtils<int, int>.computeShortestPathBetweenAllPair(graph, out Dictionary<int, Dictionary<int, int>> distanceByPair);

            for(int i = 0; i < 3; i++) 
            {
                Assert.AreEqual(0, distanceByPair[i][i]);
            }
            Assert.AreEqual(4, distanceByPair[0][1]);
            Assert.AreEqual(int.MaxValue, distanceByPair[0][2]);
            Assert.AreEqual(4, distanceByPair[1][0]);
            Assert.AreEqual(int.MaxValue, distanceByPair[1][2]);
            Assert.AreEqual(int.MaxValue, distanceByPair[2][0]);
            Assert.AreEqual(int.MaxValue, distanceByPair[2][1]);
        }

        [TestMethod]
        public void givenAGraphWithNegativeWeight_whenSearchingForShortestPathAmongPairs_WeShouldFindIt() 
        {
            var graph = new WeightedDirectedGraph<int, int>();
            graph.connect(0, 1, 4)
                .connect(0, 2, 11)
                .connect(1, 2, -7)
                .connect(2, 0, 5);

            var adjacencyMatrix = GraphUtils<int, int>.computeShortestPathBetweenAllPair(graph, out Dictionary<int, Dictionary<int, int>> distanceByPair);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(0, distanceByPair[i][i]);
            }
            Assert.AreEqual(4, distanceByPair[0][1]);
            Assert.AreEqual(-3, distanceByPair[0][2]);
            Assert.AreEqual(-2, distanceByPair[1][0]);
            Assert.AreEqual(-7, distanceByPair[1][2]);
            Assert.AreEqual(5, distanceByPair[2][0]);
            Assert.AreEqual(9, distanceByPair[2][1]);

        }

        [TestMethod]
        public void givenAGraphWithNegativeCycle_whenSearchingForShortestPathAmongPair_WeShouldNotFindIt()
        {
            var graph = new WeightedDirectedGraph<int, int>();
            graph.connect(0, 1, 1)
                .connect(1, 2, -3)
                .connect(2, 0, 1);

            var adjacencyMatrix = GraphUtils<int, int>.computeShortestPathBetweenAllPair(graph, out Dictionary<int, Dictionary<int, int>> distanceByPair);

            Assert.AreEqual(0, distanceByPair.Count);
            Assert.AreEqual(0, adjacencyMatrix.Count);
        }
    }
}
