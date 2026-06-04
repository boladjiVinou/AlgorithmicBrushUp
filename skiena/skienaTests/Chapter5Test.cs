using skiena.Chapter5;
using skiena.datastructures.graph;
using skiena.datastructures.trees;
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

        [TestMethod]
        public void givenPreOrderAndInOrder_WeShouldComputeTheRightBst() 
        {
            var preorderList = new List<int>() { 50, 30, 20, 40, 70, 60, 80 };
            var inOrderList = new List<int>() { 20, 30, 40, 50, 60, 70, 80 };
            var postOrderList = new List<int>() { 20, 40, 30, 60, 80, 70, 50 };

            var bst = Chapter5.reconstructTreeWithPreOrderAndInOrder(preorderList, inOrderList);

            var resultPreOrder = bst.preOrderIteration().ToList();
            var resultInOrder = bst.inOrderIteration().ToList();
            var resultPostOrder = bst.postOrderIteration().ToList();
            Assert.IsTrue(preorderList.Count == resultPreOrder.Count);
            Assert.IsTrue(inOrderList.Count == resultInOrder.Count);
            Assert.IsTrue(postOrderList.Count == resultPostOrder.Count);

            for (int i = 0; i < preorderList.Count; i++) 
            {
                Assert.AreEqual(preorderList[i], resultPreOrder[i]);
                Assert.AreEqual(inOrderList[i], resultInOrder[i]);
                Assert.AreEqual(postOrderList[i], resultPostOrder[i]);
            }
        }

        [TestMethod]
        public void givenPostOrderAndInOrder_WeShouldComputeTheRightBst()
        {
            var preorderList = new List<int>() { 50, 30, 20, 40, 70, 60, 80 };
            var inOrderList = new List<int>() { 20, 30, 40, 50, 60, 70, 80 };
            var postOrderList = new List<int>() { 20, 40, 30, 60, 80, 70, 50 };

            var bst = Chapter5.reconstructTreeWithPostOrderAndInOrder(postOrderList, inOrderList);

            var resultPreOrder = bst.preOrderIteration().ToList();
            var resultInOrder = bst.inOrderIteration().ToList();
            var resultPostOrder = bst.postOrderIteration().ToList();
            Assert.IsTrue(preorderList.Count == resultPreOrder.Count);
            Assert.IsTrue(inOrderList.Count == resultInOrder.Count);
            Assert.IsTrue(postOrderList.Count == resultPostOrder.Count);

            for (int i = 0; i < preorderList.Count; i++)
            {
                Assert.AreEqual(preorderList[i], resultPreOrder[i]);
                Assert.AreEqual(inOrderList[i], resultInOrder[i]);
                Assert.AreEqual(postOrderList[i], resultPostOrder[i]);
            }
        }

        [TestMethod]
        public void givenAnArithmeticExpressionAsTree_WeShouldComputeTheRightResult() 
        {
            MyBT<string> expressionTree = new MyBT<string>();
            /*
             ((8 + 92 ) * (399 - 99))/ 10

                                             /


                         *                                   *

               +                  -                  *               * 
            8     82        399        99        10      1       1       1

             */

            expressionTree.add("/");
            expressionTree.add("*");
            expressionTree.add("*");
            expressionTree.add("+");
            expressionTree.add("-");
            expressionTree.add("*");
            expressionTree.add("*");
            expressionTree.add("8");
            expressionTree.add("92");
            expressionTree.add("399");
            expressionTree.add("99");
            expressionTree.add("10");
            expressionTree.add("1");
            expressionTree.add("1");
            expressionTree.add("1");

            Assert.AreEqual(3000, Chapter5.evaluateExpression(expressionTree));
        }
        [TestMethod]
        public void givenAListOfTriangles_WeShouldComputeTheRightTriangleGraph() 
        {
            /*
             0 1 2 : t1
             1 2 3 : t2
             2 3 4 : t3
             3 4 5 : t4
             */
            int[][] possibleTriangles = new int[50][];
            for (int i = 0; i < 50; i++) 
            {
                int[] vertices = [i, i + 1, i + 2];
                possibleTriangles[i] = vertices;
            }
            List<int[]> triangles = [];
            Dictionary<int,int> originalIdxByInsertedIdx = [];
            Random rand = new Random();
            for (int i = 0; i < 30; i++) 
            {
                int idx = rand.Next() % 50;
                if (originalIdxByInsertedIdx.ContainsKey(idx)) 
                {
                    continue;
                }
                originalIdxByInsertedIdx.Add(triangles.Count, idx);
                triangles.Add(possibleTriangles[idx]);
            }
            var insertedTriangles = originalIdxByInsertedIdx.Keys.Order().ToList();




            var connectedGraph = Chapter5.createDualGraph(triangles);





            for (int i = 1; i < insertedTriangles.Count; i++) 
            {
                int t1OriginalIdx = originalIdxByInsertedIdx[insertedTriangles[i]];
                int t2OriginalIdx = originalIdxByInsertedIdx[insertedTriangles[i - 1]];
                bool isConnected = t1OriginalIdx - t2OriginalIdx == 1;
                Assert.AreEqual(isConnected, connectedGraph.areNodeConnected(t1OriginalIdx, t2OriginalIdx));
            }
        }

        [TestMethod]
        public void givenAdjencyList_WeShouldComputeTheRightSquaredGraph() 
        {
            DirectedGraph<int> graph = new DirectedGraph<int>();
            graph.connect(1, 2).connect(2, 3).connect(3, 4).connect(4, 5);

            var squaredGraph = Chapter5.generateSquaredGraph(graph.getAdjencyList());


            Assert.IsTrue(squaredGraph[1].Count ==2 && squaredGraph[1].Contains(3));
            Assert.IsTrue(squaredGraph[2].Count == 2 && squaredGraph[2].Contains(4));
            Assert.IsTrue(squaredGraph[3].Count == 2 && squaredGraph[3].Contains(5));
            Assert.IsTrue(squaredGraph[1].Contains(2));
            Assert.IsTrue(squaredGraph[2].Contains(3));
            Assert.IsTrue(squaredGraph[3].Contains(4));
            Assert.IsTrue(squaredGraph[4].Contains(5));

        }
        [TestMethod]
        public void givenAdjencyMatrix_WeShouldComputeTheRightSquaredGraph()
        {
            DirectedGraph<int> graph = new DirectedGraph<int>();
            graph.connect(1, 2).connect(2, 3).connect(3, 4).connect(4, 5);

            var squaredGraph = Chapter5.generateSquaredGraph(graph.getAdjencyMatrice());

            Assert.IsFalse(squaredGraph[1][4]);
            Assert.IsFalse(squaredGraph[1][5]);
            Assert.IsFalse(squaredGraph[2][5]);
            foreach (var u in graph.getVertices()) 
            {
                foreach (var v in graph.getNeighbors(u)) 
                {
                    foreach (var w in graph.getNeighbors(v))
                    {
                        Assert.AreEqual(true, squaredGraph[u][w]);
                    }                      
                }
            }

        }
    }
}
