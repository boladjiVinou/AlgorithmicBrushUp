using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class DirectedGraphTest : MyGraphTest
    {
        [TestInitialize]
        public override void setup()
        {
            graph = createGraph();
            adjencyMatrix = [];
            adjencySet = [];
            incidenceMatrix = [];
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
                        incidenceMatrix[u][v] = 1;
                        adjencySet[u].Add(v);
                        adjencyMatrix[u][v] = true;
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
            Assert.AreEqual(0, tmpGraph.getInDegree(2));
            Assert.AreEqual(0, tmpGraph.getInDegree(0));
        }

        [TestMethod]
        public void whenANodeIsConnected_TheOutDegreeShouldBeUpdated()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(0, tmpGraph.getOutDegree(1));
            Assert.AreEqual(1, tmpGraph.getOutDegree(2));
            Assert.AreEqual(1, tmpGraph.getOutDegree(0));
        }

        [TestMethod]
        public void givenADirectedGraphWithTwoEntrie_ThePossibleRootsShouldNotExist()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1).connect(1, 2).connect(3, 2);

            var possibleRoots = tmpGraph.getPossibleRoot();

            Assert.IsTrue(!possibleRoots.Any());
        }

        [TestMethod]
        public void givenADirectedGraphWithOneEntry_ThePossibleRootsExist()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(1,2).connect(0,1).connect(2,1);

            var possibleRoots = tmpGraph.getPossibleRoot().ToList();

            Assert.IsTrue(possibleRoots.Contains(0));
        }

        protected override Graph<int> createGraph()
        {
            return new DirectedGraph<int>();
        }
    }
}
