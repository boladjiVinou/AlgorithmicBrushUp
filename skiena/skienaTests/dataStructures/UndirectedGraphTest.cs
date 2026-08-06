using skiena.datastructures.graph;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class UndirectedGraphTest : MyGraphTest
    {
        [TestInitialize]
        public override void setup()
        {
            graph = createGraph();
            adjencyMatrix = [];
            adjencySet =[];
            incidenceMatrix =[];
            edges = [];
            vertices = [];

            for (int i = 0; i < 20; i++) 
            {
                vertices.Add(i);
            }

            Random rand = new Random();
            foreach (var u in vertices)
            {
                graph.insertNode(u);

                registerNodeIfNeeded(u);
                foreach (var v in vertices)
                {
                    registerNodeIfNeeded(v);

                    if (!incidenceMatrix[u].ContainsKey(v)) 
                    {
                        incidenceMatrix[u].Add(v, 0);
                    }
                    if (!adjencyMatrix[u].ContainsKey(v)) 
                    {
                        adjencyMatrix[u].Add(v, false);
                    }
                   
                    if (!incidenceMatrix[v].ContainsKey(u))
                    {
                        incidenceMatrix[v].Add(u, 0);
                    }
                    if (!adjencyMatrix[v].ContainsKey(u))
                    {
                        adjencyMatrix[v].Add(u, false);
                    }


                    if (u == v) { continue; }


                    if (rand.Next() % 100 > 40)
                    {
                        edges.Add(new Tuple<int, int>(u, v));
                        edges.Add(new Tuple<int, int>(v, u));
                        incidenceMatrix[u][v] = 1;
                        incidenceMatrix[v][u] = 1;
                        adjencySet[u].Add(v);
                        adjencySet[v].Add(u);
                        adjencyMatrix[u][v] = true;
                        adjencyMatrix[v][u] = true;
                        graph.connect(u, v);
                    }
                }
            }
        }
        [TestMethod]
        public void whenANodeIsConnected_TheInDegreeShouldBeUpdated()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(2, tmpGraph.getInDegree(1));
            Assert.AreEqual(1, tmpGraph.getInDegree(2));
            Assert.AreEqual(1, tmpGraph.getInDegree(0));
        }

        [TestMethod]
        public void whenANodeIsConnected_TheOutDegreeShouldBeUpdated()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(2, tmpGraph.getOutDegree(1));
            Assert.AreEqual(1, tmpGraph.getOutDegree(2));
            Assert.AreEqual(1, tmpGraph.getOutDegree(0));
        }
        [TestMethod]
        public void givenAGraph_ThePossibleRootsShouldBeCorrect()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1).connect(1, 2).connect(3, 2);

            var possibleRoots = tmpGraph.getPossibleCommonRoot();

            Assert.IsTrue(possibleRoots.Contains(0) || possibleRoots.Contains(3));
        }

        [TestMethod]
        public void givenAWeightedGraph_WhenUsingPrimAlgorithm_WeShouldComputeTheRightTree()
        {
            var tmpGraph = new WeightedUndirectedGraph<int,int>();
            tmpGraph.connect(0, 1, 1).connect(1, 2, 2).connect(2, 3, 3).connect(3, 1, 2);

            WeightedUndirectedGraph<int,int> mst =  tmpGraph.primMinimumSpanningTree(0);

            var edges =  mst.getEdges();
            var vertices = mst.getVertices();
           
            Assert.IsTrue(vertices.All(v =>tmpGraph.contains(v)));
            Assert.IsFalse(mst.areNodeConnected(2, 3));
            Assert.IsTrue(mst.areNodeConnected(0, 1));
            Assert.IsTrue(mst.areNodeConnected(1, 2));
            Assert.IsTrue(mst.areNodeConnected(3, 1));
        }


        [TestMethod]
        public void givenAWeightedGraph_WhenUsingKruskalAlgorithm_WeShouldComputeTheRightTree()
        {
            var tmpGraph = new WeightedUndirectedGraph<int, int>();
            tmpGraph.connect(0, 1, 1).connect(1, 2, 2).connect(2, 3, 3).connect(3, 1, 2);

            WeightedUndirectedGraph<int, int> mst = tmpGraph.kruskalMinimumSpanningTree();

            var edges = mst.getEdges();
            var vertices = mst.getVertices();

            Assert.IsTrue(vertices.All(v => tmpGraph.contains(v)));
            Assert.IsFalse(mst.areNodeConnected(2, 3));
            Assert.IsTrue(mst.areNodeConnected(0, 1));
            Assert.IsTrue(mst.areNodeConnected(1, 2));
            Assert.IsTrue(mst.areNodeConnected(3, 1));
        }

        protected override UndirectedGraph<int> createGraph()
        {
           return new UndirectedGraph<int>();
        }
    }
}
