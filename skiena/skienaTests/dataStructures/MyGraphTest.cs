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

            Assert.IsTrue(adjencyMatrix.Keys.All(x => tmpMatrix.ContainsKey(x)));
            Assert.IsTrue(adjencyMatrix.Keys.All(x => adjencyMatrix[x].Keys.All(y => tmpMatrix[x].Keys.Contains(y) &&
            adjencyMatrix[x][y] == tmpMatrix[x][y])));
        }
        [TestMethod]
        public void givenAGraph_TheAdjencyListShouldMatchItsState() 
        {
            var tmpList = graph.getAdjencyList();

            Assert.IsTrue(adjencySet.Keys.All(x => tmpList.ContainsKey(x)));
            Assert.IsTrue(adjencySet.Keys.All(x => adjencySet.All(y => tmpList[x].Contains(y))));
        }
        [TestMethod]
        public void givenAnAdjencyMatrix_WhenConvertingToAdjencyList_TheListSholdMatchTheState() 
        {
            var tmpList = Graph<int>.convertToAdjacencyList(adjencyMatrix);

            Assert.AreEqual(adjencySet, tmpList);
        }
        [TestMethod]
        public void givenAnAdjencyList_WhenConvertingToIncidenceMatrix_TheMatrixShouldMatchTheState() 
        {
           var matrix = Graph<int>.convertToIncidenceMatrix(adjencySet);

           Dictionary<int, Dictionary<int, int>> expectedResult = new Dictionary<int, Dictionary<int, int>>();
           var possibleNeighbors = adjencySet.Values.SelectMany(x => x);
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

            Assert.AreEqual(expectedResult, matrix);
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
            List<Tuple<int,int>> expectedEdges = new List<Tuple<int,int>>();
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

            var result = graph.getEdges();
            Assert.AreEqual(expectedEdges.Count, result.Count);
            Assert.IsTrue(expectedEdges.All(x => result.Any(y => x.Item1 == y.Item1 && x.Item2 == y.Item2)));
        }
        [TestMethod]
        public void givenAGraph_WhenGettingTheVertices_TheRightValueShouldBeReturned() 
        {
            var tmpVertices = graph.getVertices();

            Assert.IsTrue(vertices.All(x => tmpVertices.Contains(x)));
            Assert.IsTrue(tmpVertices.All(x=> vertices.Contains(x)));)
        }
        [TestMethod]
        public void givenAGraph_WhenGettingTheNeighborsOfANode_TheRightSetShouldBeReturned() 
        {
            var rand = new Random();
            int node = vertices[rand.Next() % vertices.Count];

            var result = graph.getNeighbors(node);

            Assert.AreEqual(adjencySet[node], result);
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
            for (int i = 0; i < 20; i += 2)
            {
                nonBiPartiteGraph.connect(i, i + 1);
                nonBiPartiteGraph.connect(i, i + 2);
            }

            Dictionary<int, int> colorByNode = new Dictionary<int, int>();
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
        public void whenANodeIsConnected_TheInDegreeShouldBeUpdated() 
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(2, tmpGraph.getInDegree(1));
            Assert.AreEqual(0,tmpGraph.getInDegree(2));
            Assert.AreEqual(0, tmpGraph.getInDegree(0));
        }

        [TestMethod]
        public void whenANodeIsConnected_TheOutDegreeShouldBeUpdated()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(0, tmpGraph.getInDegree(1));
            Assert.AreEqual(1, tmpGraph.getInDegree(2));
            Assert.AreEqual(1, tmpGraph.getInDegree(0));
        }

        [TestMethod]
        public void givenAGraph_ThePossibleRootsShouldBeCorrect()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1).connect(1,2).connect(3,2);

            var possibleRoots = tmpGraph.getPossibleRoots();

            Assert.IsTrue(possibleRoots.Contains(0));
            Assert.IsTrue(possibleRoots.Contains(3));
        }
        [TestMethod]
        public void givenAGraph_TheDeletionOrderShouldBeCorrect() 
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1).connect(1, 2);

            var deletionOrder = tmpGraph.getDeletionOrder().Select(x=>x.Value).ToList();

            var expectedOrder = new List<int>() { 2,1,0 };

            Assert.AreEqual(expectedOrder, deletionOrder);
        }

        protected abstract Graph<int> createGraph();
    }
}
