using skiena.datastructures.graph;
using skiena.datastructures.graph.interfaces;
using skiena.datastructures.graph.specificgraph;
using skiena.datastructures.graph.specificgraph.util;

namespace skienaTests.dataStructures.graph.specificgraph.util
{
    [TestClass]
    public class BellmanFordTest
    {
        [TestMethod]
        public void givenAStandarWeightedDirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createStandarGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.IsTrue(adjencyList[3].Contains(4));
            Assert.AreEqual(10, distance);
        }
        [TestMethod]
        public void givenAStandarNegativeWeightedDirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createStandarGraph(graph);
            graph.setWeight(1,3,-10);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.IsTrue(adjencyList[3].Contains(4));
            Assert.AreEqual(-1, distance);
        }

        [TestMethod]
        public void givenADisconnectedWeightedDirectedGraph_whenUsingBellmanWeShouldNotFindShortestPath()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createDisconnectedGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 3, out int distance);

            Assert.IsTrue(path.getVertices().Count == 0);
            Assert.AreEqual(int.MaxValue, distance);

        }

        [TestMethod]
        public void givenADenseWeightedDirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createDenseGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(2));
            Assert.IsTrue(adjencyList[2].Contains(4));
            Assert.AreEqual(7, distance);

        }

        [TestMethod]
        public void givenAGraph_whenUsingBellmanForNonExistingNode_WeShouldNotFindAPath()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createStandarGraph(graph);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 100, out int distance);

            Assert.AreEqual(int.MaxValue, distance);
            Assert.AreEqual(0, path.getVertices().Count);
        }
        [TestMethod]
        public void givenADirectedGraphWithMultipleShortPath_WhenUsingBellman_WeShouldRespectVisitOrder()
        {
            WeightedDirectedGraph<int, int> graph = new();
            createMultiPathGraph(graph);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 3, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.AreEqual(10, distance);
        }

        // ****************************************************

        [TestMethod]
        public void givenAStandarWeightedUndirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createStandarGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.IsTrue(adjencyList[3].Contains(4));
            Assert.AreEqual(10, distance);

        }

        [TestMethod]
        public void givenAStandarNegativeWeightedUndirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createStandarGraph(graph);
            graph.setWeight(1, 3, -10);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.IsTrue(adjencyList[3].Contains(4));
            Assert.AreEqual(-1, distance);
        }

        [TestMethod]
        public void givenADisconnectedWeightedUndirectedGraph_whenUsingBellmanWeShouldNotFindShortestPath()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createDisconnectedGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 3, out int distance);

            Assert.IsTrue(path.getVertices().Count == 0);
            Assert.AreEqual(int.MaxValue, distance);

        }

        [TestMethod]
        public void givenADenseWeightedUndirectedGraph_whenUsingBellmanWeShouldFindShortestPath()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createDenseGraph(graph);


            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 4, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(2));
            Assert.IsTrue(adjencyList[2].Contains(4));
            Assert.AreEqual(7, distance);

        }

        [TestMethod]
        public void givenAnUdirectedGraph_whenUsingBellmanForNonExistingNode_WeShouldNotFindAPath()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createStandarGraph(graph);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 100, out int distance);

            Assert.AreEqual(int.MaxValue, distance);
            Assert.AreEqual(0, path.getVertices().Count);
        }
        [TestMethod]
        public void givenAnUndirectedGraphWithMultipleShortPath_WhenUsingBellman_WeShouldRespectVisitOrder()
        {
            WeightedUndirectedGraph<int, int> graph = new();
            createMultiPathGraph(graph);

            var path = GraphUtils<int, int>.bellmanFordShortestPath(graph, 0, 3, out int distance);
            var adjencyList = path.getAdjencyList();

            Assert.IsTrue(adjencyList[0].Contains(1));
            Assert.IsTrue(adjencyList[1].Contains(3));
            Assert.AreEqual(10, distance);
        }

        // ****************************************************

        /*
           (4)
         0 -- 1
      (8)|  / | (1)
         | /  |
         2 -- 3
     (6) |  /
         | / (5)
         4
        */
        private void createStandarGraph(IWeightedGraph<int, int> graph)
        {
            graph.connect(0, 1, 4)
              .connect(0, 2, 8)
              .connect(1, 2, 3)
              .connect(1, 3, 1)
              .connect(2, 3, 2)
              .connect(2, 4, 6)
              .connect(3, 4, 5);
        }

        private void createDisconnectedGraph(IWeightedGraph<int, int> graph)
        {
            graph.connect(0, 1, 2).connect(2, 3, 4);
        }

        private void createDenseGraph(IWeightedGraph<int, int> graph)
        {
            graph.connect(0, 2, 4)
                .connect(0, 3, 7)
                .connect(0, 4, 12)
                .connect(1, 3, 4)
                .connect(1, 4, 8)
                .connect(2, 3, 2)
                .connect(2, 4, 5)
                .connect(3, 4, 2)
                .connect(4, 1, 4)
                .connect(4, 2, 3)
                .connect(4, 3, 1);

        }

        private void createMultiPathGraph(IWeightedGraph<int, int> graph)
        {
            graph.connect(0, 1, 5)
                .connect(0, 2, 5)
                .connect(1, 3, 5)
                .connect(2, 3, 5);
        }


    }
}
