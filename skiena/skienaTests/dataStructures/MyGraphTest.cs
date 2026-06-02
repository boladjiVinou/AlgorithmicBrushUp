using MoreLinq.Extensions;
using skiena.Chapter5;
using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public abstract class MyGraphTest
    {
        protected Graph<int> graph;
        protected Dictionary<int, Dictionary<int, bool>> adjencyMatrix;
        protected Dictionary<int, HashSet<int>> adjencySet;
        protected Dictionary<int, Dictionary<int, int>> incidenceMatrix;
        protected List<Tuple<int, int>> edges;
        protected List<int> vertices;

       [TestInitialize]
        public abstract void setup();

        [TestMethod]
        public void givenAGraph_TheAdjencyMatrixShouldMatchItsState() 
        {
            var tmpMatrix = graph.getAdjencyMatrice();

            Assert.IsTrue(adjencyMatrix.Keys.All(tmpMatrix.ContainsKey));
            Assert.IsTrue(adjencyMatrix.Keys.All(x=> adjencyMatrix[x].Keys.Count == tmpMatrix[x].Keys.Count));
            foreach (var u in vertices)
            {
                foreach (var v in vertices) 
                {
                    Assert.AreEqual(adjencyMatrix[u][v], tmpMatrix[u][v]);
                }
            }
        }
        [TestMethod]
        public void givenAGraph_TheAdjencyListShouldMatchItsState() 
        {
            var tmpList = graph.getAdjencyList();

            Assert.IsTrue(adjencySet.Keys.All(x => tmpList.ContainsKey(x)));
            Assert.IsTrue(tmpList.Keys.All(x => adjencySet.ContainsKey(x)));
            Assert.IsTrue(adjencySet.Keys.All(x => adjencySet[x].All(y => tmpList[x].Contains(y))));
            Assert.IsTrue(tmpList.Keys.All(x => adjencySet[x].All(y => adjencySet[x].Contains(y))));
        }
        [TestMethod]
        public void givenAnAdjencyMatrix_WhenConvertingToAdjencyList_TheListSholdMatchTheState() 
        {
            var tmpList = Graph<int>.convertToAdjacencyList(adjencyMatrix);

            Assert.AreEqual(adjencySet.Keys.Count, tmpList.Keys.Count);
            Assert.IsTrue(adjencySet.Keys.All(x => tmpList.ContainsKey(x)));
            Assert.IsTrue(adjencySet.Keys.All(x=> adjencySet[x].Count == tmpList[x].Count));
            Assert.IsTrue(adjencySet.Keys.All(x => adjencySet[x].All(y => tmpList[x].Contains(y))));
        }
        [TestMethod]
        public void givenAnAdjencyList_WhenConvertingToIncidenceMatrix_TheMatrixShouldMatchTheState() 
        {
           var matrix = Graph<int>.convertToIncidenceMatrix(adjencySet);

           Dictionary<int, Dictionary<int, int>> expectedResult = new Dictionary<int, Dictionary<int, int>>();
           var possibleNeighbors = adjencySet.Values.SelectMany(x => x).Distinct();
            foreach (var item in vertices)
            {
                if (!expectedResult.ContainsKey(item)) 
                {
                    expectedResult.Add(item, new Dictionary<int, int>());
                }
                foreach (var neighbor in possibleNeighbors)
                {
                    expectedResult[item].Add(neighbor, adjencySet[item].Contains(neighbor) ? 1 : 0);
                }
            }

            Assert.AreEqual(expectedResult.Keys.Count, matrix.Keys.Count);
            Assert.IsTrue(expectedResult.Keys.All(x => matrix.Keys.Contains(x)));
            Assert.IsTrue(expectedResult.Keys.All(x => expectedResult[x].Keys.All(y => matrix[x][y] == expectedResult[x][y])));
        }
        [TestMethod]
        public void givenAConnectedGraph_WhenGettingTheEdges_TheRightResultShouldBeReturned() 
        {
            var tmpEdges = graph.getEdges();

            Assert.IsTrue(edges.All(x => tmpEdges.Contains(x)));
        }
        [TestMethod]
        public void givenADisconnectedGraph_WhenGettingTheEdges_TheRightResultShouldBeReturned() 
        {
            var tmpGraph = createGraph();
            for (int i = 0; i <= 10; i++) 
            {
                tmpGraph.insertNode(i);
            }
            var rand = new Random();
            HashSet<Tuple<int,int>> expectedEdges = new HashSet<Tuple<int,int>>();
            for (int i = 0; i < 3; i++) 
            {
                int u = rand.Next() % 5;
                int v = rand.Next() % 5 + 5;
                tmpGraph.connect(u,v);
                expectedEdges.Add(new Tuple<int, int>(u,v));
                if (graph is UndirectedGraph<int>) 
                {
                    expectedEdges.Add(new Tuple<int, int>(v, u));
                }
            }

            var result = tmpGraph.getEdges();
            Assert.AreEqual(expectedEdges.Count, result.Count);
            Assert.IsTrue(expectedEdges.All(x => result.Any(y => x.Item1 == y.Item1 && x.Item2 == y.Item2)));
        }
        [TestMethod]
        public void givenAGraph_WhenGettingTheVertices_TheRightValueShouldBeReturned() 
        {
            var tmpVertices = graph.getVertices();

            Assert.IsTrue(vertices.All(x => tmpVertices.Contains(x)));
            Assert.IsTrue(tmpVertices.All(x=> vertices.Contains(x)));
        }
        [TestMethod]
        public void givenAGraph_WhenGettingTheNeighborsOfANode_TheRightSetShouldBeReturned() 
        {
            var rand = new Random();
            int node = vertices[rand.Next() % vertices.Count];

            var result = graph.getNeighbors(node);

            Assert.IsTrue(adjencySet[node].All(result.Contains));
            Assert.IsTrue(result.All(adjencySet[node].Contains));
        }

        [TestMethod]
        public void givenABipartiteGraph_WhenCheckingIfBipartite_WeShouldReturnTheRightValue() 
        {
            var bipartiteGraph = createGraph();
            for (int i = 0; i < 20; i += 2) 
            {
                bipartiteGraph.connect(i, i + 1);
            }

            Dictionary<int, int> colorByNode = new Dictionary<int, int>();
            Assert.IsTrue(bipartiteGraph.isBipartite(out colorByNode));
        }
        [TestMethod]
        public void givenANonBipartiteGraph_WhenCheckingIfBipartite_WeShouldReturnTheRightValue() 
        {
            var nonBiPartiteGraph = createGraph();
            nonBiPartiteGraph.connect(0,1);
            nonBiPartiteGraph.connect(0,2);
            nonBiPartiteGraph.connect(2, 1);

            Dictionary<int, int> colorByNode = [];
            Assert.IsFalse(nonBiPartiteGraph.isBipartite(out colorByNode));
        }
        [TestMethod]
        public void givenAGraph_WhenConnectingTwoVertices_theyShouldBeConnected() 
        {
            var tmpGraph = createGraph();

            tmpGraph.connect(0, 1);

            Assert.IsTrue(tmpGraph.areNodeConnected(0, 1));
        }

        [TestMethod]
        public void givenAGraphWithConnectedNodes_WhenDisconnectingTwoNodes_TheyShouldBeDisconnected() 
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);

            tmpGraph.disconnect(0, 1);

            Assert.IsFalse(tmpGraph.areNodeConnected(0, 1));
        }

        [TestMethod]
        public void givenConnectedNode_WhenItIsNoLongerConnectedToAnything_ItShouldBeRemoved() 
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);

            tmpGraph.disconnect(0, 1);

            var tmpVertices = tmpGraph.getVertices();
            Assert.IsFalse(tmpVertices.Contains(0));
            Assert.IsFalse(tmpVertices.Contains(1));
        }

        [TestMethod]
        public void whenInsertingASingleNode_ItShouldBePartOfTheGraph() 
        {
            var tmpGraph = createGraph();

            tmpGraph.insertNode(0);
            tmpGraph.insertNode(199);

            var tmpVertices = tmpGraph.getVertices();
            Assert.IsTrue(tmpVertices.Contains(0));
            Assert.IsTrue(tmpVertices.Contains(199));
        }
        [TestMethod]
        public void givenAGraph_TheDeletionOrderShouldBeCorrect() 
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1).connect(1, 2);

            var deletionOrder = tmpGraph.getDeletionOrder().Select(x=>x.Value).ToList();

            var expectedOrder = new List<int>() { 2,1,0 };

            Assert.AreEqual(expectedOrder.Count, deletionOrder.Count);
            for (int i = 0; i < expectedOrder.Count; i++) 
            {
                Assert.AreEqual(expectedOrder[i], deletionOrder[i]);
            }
        }

        protected abstract Graph<int> createGraph();


        protected void registerNodeIfNeeded(int u)
        {
            if (!incidenceMatrix.ContainsKey(u))
            {
                incidenceMatrix.Add(u, []);
            }
            if (!adjencySet.ContainsKey(u))
            {
                adjencySet.Add(u, []);
            }
            if (!adjencyMatrix.ContainsKey(u))
            {
                adjencyMatrix.Add(u, []);
            }
        }

    }
}
