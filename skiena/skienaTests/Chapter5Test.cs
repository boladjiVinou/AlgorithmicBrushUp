using Microsoft.Testing.Platform.Extensions.Messages;
using skiena.Chapter5;
using skiena.datastructures.graph;
using skiena.datastructures.trees;

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
                if (originalIdxByInsertedIdx.ContainsValue(idx)) 
                {
                    continue;
                }
                originalIdxByInsertedIdx.Add(triangles.Count, idx);
                triangles.Add(possibleTriangles[idx]);
            }




            var connectedGraph = Chapter5.createDualGraph(triangles);


            for (int i = 0; i < triangles.Count; i++) 
            {
                int t1OriginalIdx = originalIdxByInsertedIdx[i];
                for(int j = 0; j< triangles.Count; j++) 
                {
                    if (i == j) 
                    {
                        continue;
                    }
                    int t2OriginalIdx = originalIdxByInsertedIdx[j];
                    bool isConnected = Math.Abs(t1OriginalIdx - t2OriginalIdx) == 1;
                    Assert.AreEqual(isConnected, connectedGraph.areNodeConnected(i, j));
                }
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
        [TestMethod]
        public void givenATree_WhenSearchingForTheVertexCover_WeShouldComputeTheRightValue() 
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0,1).connect(0,2).connect(1,3).connect(1,4).connect(2,5).connect(2,6);

            var cover = Chapter5.minimumSizeVerticesA(tree);

            for (int i = 0; i < 3; i++) 
            {
                Assert.IsTrue(cover.Contains(i));
            }
            for (int i = 3; i < 7; i++)
            {
                Assert.IsFalse(cover.Contains(i));
            }
        }

        [TestMethod]
        public void givenATreeOfDegrees_WhenSearchingForTheVertexCover_WeShouldComputeTheMnimumWeightCover()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(1, 3).connect(1, 4).connect(2, 5).connect(2, 6);

            var cover = Chapter5.minimumSizeVertexVersionB(tree);

            Assert.IsTrue(tree.getEdges().All(e => cover.Contains(e.Item1) || cover.Contains(e.Item2)));
        }

        [TestMethod]
        public void givenATree_WhenSearchingForTheMinimumDegreeVertexCover_WeShouldComputeTheMnimumDegreeCover()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);

            var cover = Chapter5.minimumSizeVertexVersionB(tree);

            Assert.IsTrue(cover.Count == 1);
            Assert.IsTrue(tree.getEdges().All(e => cover.Contains(e.Item1) || cover.Contains(e.Item2)));
        }


        [TestMethod]
        public void givenATree_WhenSearchingForTheMinimumWeighteVertexCover_WeShouldComputeTheMnimumWeightCover()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);
            Dictionary<int, int> weightByNode = new();
            weightByNode.Add(0, 1000);
            weightByNode.Add(1, 1);
            weightByNode.Add(2, 2);
            weightByNode.Add(3, 3);
            weightByNode.Add(4, 4);

            var cover = Chapter5.minimumWeightCover(tree, weightByNode);

            Assert.IsTrue(cover.Count == 4);
            Assert.IsTrue(tree.getEdges().All(e => cover.Contains(e.Item1) || cover.Contains(e.Item2)));
        }
        [TestMethod]
        public void givenATree_WhenSearchingForMaxIndependentSet_WeShouldFindIt()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);

            var set = Chapter5.computeMaxIndependentSetA(tree);

            Assert.IsTrue(set.Count == 4);
            Assert.IsTrue(tree.getEdges().All(e => !set.Contains(e.Item1) || !set.Contains(e.Item2)));
        }

        [TestMethod]
        public void givenATree_WhenSearchingForMaxDegreeIndependentSet_WeShouldFindIt()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);

            var set = Chapter5.computeMaxIndependentSetB(tree);

            Assert.IsTrue(set.Count == 1);
            Assert.IsTrue(tree.getEdges().All(e => !set.Contains(e.Item1) || !set.Contains(e.Item2)));
        }


        [TestMethod]
        public void givenATree_WhenSearchingForMaxWeightIndependentSet_WeShouldFindIt()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);
            Dictionary<int, int> weightByNode = [];
            weightByNode.Add(0, 1000);
            weightByNode.Add(1, 500);
            weightByNode.Add(2, 501);
            weightByNode.Add(3, 1);
            weightByNode.Add(4, 1);

            var set = Chapter5.computeMaxIndependentSetC(tree, weightByNode);

            Assert.IsTrue(set.Count == 4);
            Assert.IsTrue(tree.getEdges().All(e => !set.Contains(e.Item1) || !set.Contains(e.Item2)));
        }
        [TestMethod]
        public void givenATreeWithOneCentralNodeWithHighestValue_WhenSearchingForMaxWeightIndependentSet_WeShouldFindIt()
        {
            UndirectedGraph<int> tree = new UndirectedGraph<int>();
            tree.connect(0, 1).connect(0, 2).connect(0, 3).connect(0, 4);
            Dictionary<int, int> weightByNode = [];
            weightByNode.Add(0, 10000);
            weightByNode.Add(1, 500);
            weightByNode.Add(2, 501);
            weightByNode.Add(3, 1);
            weightByNode.Add(4, 1);

            var set = Chapter5.computeMaxIndependentSetC(tree, weightByNode);

            Assert.IsTrue(set.Count == 1);
            Assert.IsTrue(tree.getEdges().All(e => !set.Contains(e.Item1) || !set.Contains(e.Item2)));
        }

        [TestMethod]
        public void givenAGraphWithATriangle_whenSearchingForTriangle_WeShouldFindIt() 
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            graph.connect(0, 1).connect(0, 2).connect(2,1);

            Assert.IsTrue(Chapter5.findTriangleVersionB(graph));
        }
        [TestMethod]
        public void givenAGraphWithoutATriangle_whenSearchingForTriangle_WeShouldFindNotIt()
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            graph.connect(0, 1).connect(0, 2).connect(1, 3);

            Assert.IsFalse(Chapter5.findTriangleVersionB(graph));
        }

        [TestMethod]
        public void givenAGraphWithoutATriangleV2_whenSearchingForTriangle_WeShouldFindNotIt()
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            graph.connect(0, 1).connect(0, 2).connect(2, 3);

            Assert.IsFalse(Chapter5.findTriangleVersionB(graph));
        }

        [TestMethod]
        public void whenItIsImpossibleToHaveAMovieOneDayOnly_WeShouldDetectIt() 
        {
            List<Tuple<int, int>> desiredMovies = [];
            desiredMovies.Add(new Tuple<int, int>(0, 1));
            desiredMovies.Add(new Tuple<int, int>(2, 3));
            desiredMovies.Add(new Tuple<int, int>(4,5));


            desiredMovies.Add(new Tuple<int, int>(1, 0));
            var possibleSchedule = Chapter5.findSchedule(desiredMovies);

            Assert.IsTrue(possibleSchedule.Keys.Count == 0);
        }
        [TestMethod]
        public void whenItIsPossibleToHaveAScheduleForEachDay_WeShouldDetectIt() 
        {
            List<Tuple<int, int>> desiredMovies = [];
            desiredMovies.Add(new Tuple<int, int>(0, 1));
            desiredMovies.Add(new Tuple<int, int>(2, 1));
            desiredMovies.Add(new Tuple<int, int>(0, 3));

            var possibleSchedule = Chapter5.findSchedule(desiredMovies);

            Assert.IsTrue(possibleSchedule.Keys.Count == 4);
        }


        public static IEnumerable<object[]> graphDiameterTestInput()
        {
            yield return new object[] { new List<Tuple<int, int>>(), 0 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1) }, 1 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) },1 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(2, 3), new(3, 4)}, 4 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(2, 3), new(3, 4), new(4,0) }, 4 };
        }
        [TestMethod]
        [DynamicData(nameof(graphDiameterTestInput), DynamicDataSourceType.Method)]
        public void whenComputingTheDiameterOfATree_WeShouldGetTheRightValue(List<Tuple<int, int>> connections, int expectedDiameter) 
        {
            var graph = new DirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Assert.AreEqual(expectedDiameter, Chapter5.computeDiameter(graph));
        }

        [TestMethod]
        public void givenThereIsAMaxInducedSubGraph_WeShouldFindIt()
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            graph.connect(0, 1).connect(0, 2).connect(0, 3).connect(1, 4).connect(1, 5);

            var maxGraph = Chapter5.computeMaximumInducedSubgraph(graph, 3);

            Assert.IsTrue(maxGraph.getVertices().All(x => maxGraph.getInDegree(x) >= 3));
        }


        [TestMethod]
        public void givenThereIsNoMaxInducedSubGraph_WeShouldNotFindIt()
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            graph.connect(0, 1).connect(0, 2).connect(1, 4).connect(1, 5);

            var maxGraph = Chapter5.computeMaximumInducedSubgraph(graph, 3);

            Assert.IsFalse(maxGraph.getVertices().Any(x => maxGraph.getInDegree(x) >= 3));
        }

        public static IEnumerable<object[]> numberOfShortestPathTestInput()
        {
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }, 2, 0, 0 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }, 0, 2, 1 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(2, 3), new(3, 4), new (2, 4) }, 2, 4 ,1 };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(0, 3), new(3, 2) }, 0, 2, 2 };
        }
        [TestMethod]
        [DynamicData(nameof(numberOfShortestPathTestInput), DynamicDataSourceType.Method)]
        public void whenComputingTheNumberOfShortestPathFromUToV_WeShouldComputeTheRightValue(List<Tuple<int, int>> connections, int u, int v,int expectedNumber) 
        {
            var graph = new DirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Assert.AreEqual(expectedNumber, Chapter5.findNumberOfShortestPath(graph, u, v));
        }

        public static IEnumerable<object[]> reducingGraphTestInput()
        {
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }};
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }};
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(2, 3), new(3, 4), new(2, 4) } };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(0, 3), new(3, 2) }};
        }
        [TestMethod]
        [DynamicData(nameof(reducingGraphTestInput), DynamicDataSourceType.Method)]
        public void whenReducingAGraph_WeShouldNotFindNodeOfDegree2(List<Tuple<int, int>> connections)
        {
            UndirectedGraph<int> graph = new UndirectedGraph<int>();
            foreach (var item in connections)
            {
                graph.connect(item.Item1, item.Item2);
            }

            Chapter5.reduceEdges(graph);

            bool reduced = !graph.getVertices().Any(x => graph.getInDegree(x) >= 2);
            bool graphNotEmpty = graph.getVertices().Any();

            Assert.IsTrue(graphNotEmpty && reduced);
        }
        public static IEnumerable<object[]> illBehavedChildrenTestInput()
        {
            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>() },
                {3, new HashSet<int> {1 } }},true };

            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>{ 1} }},false };

            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>{ 3} },
                {3, new HashSet<int> {1 } }},false };

            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int> { 2 ,3,4} },
                { 2, new HashSet<int>() },
                {3, new HashSet<int> () },
                {4,new HashSet<int>() } },true };


            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int>() },
                { 2, new HashSet<int>() },
                {3, new HashSet<int> () },
                {4,new HashSet<int>() } },true };

            yield return new object[] { new Dictionary<int, HashSet<int>> { 
                { 1, new HashSet<int>{ 2,3,4,5} },
                { 2, new HashSet<int>{ 3,4,5} },
                {3, new HashSet<int> {4,5 } },
                {4,new HashSet<int>{ 5}} },true };
        }

        [TestMethod]
        [DynamicData(nameof(illBehavedChildrenTestInput), DynamicDataSourceType.Method)]
        public void whenTryingToArrangeIllBehavedChildren_WeShouldFindTheRightOrder(Dictionary<int, HashSet<int>> relations, bool expectedResult) 
        {
            var graph = new DirectedGraph<int>();
            foreach(var item in relations.Keys) 
            {
                graph.insertNode(item);
                foreach(var val in relations[item]) 
                {
                    graph.connect(item, val);
                }
            }
            

            var order =  Chapter5.getLineOrderA(graph);

            Assert.AreEqual(expectedResult, order.Any());
        }


        public static IEnumerable<object[]> illBehavedChildrenTestInputB()
        {
            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>() },
                { 3, new HashSet<int> {1 } }}, 3 };

            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>{ 1} }} ,-1 };

            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int> { 2 } },
                { 2, new HashSet<int>{ 3} },
                {3, new HashSet<int> {1 } }}, -1 };

            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int> { 2 ,3,4} },
                { 2, new HashSet<int>() },
                {3, new HashSet<int> () },
                {4,new HashSet<int>() } }, 2 };


            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int>() },
                { 2, new HashSet<int>() },
                {3, new HashSet<int> () },
                {4,new HashSet<int>() } }, 1 };

            yield return new object[] { new Dictionary<int, HashSet<int>> {
                { 1, new HashSet<int>{ 2,3,4,5} },
                { 2, new HashSet<int>{ 3,4,5} },
                {3, new HashSet<int> {4,5 } },
                {4,new HashSet<int>{ 5}} },5 };
        }

        [TestMethod]
        [DynamicData(nameof(illBehavedChildrenTestInputB), DynamicDataSourceType.Method)]
        public void whenTryingToArrangeIllBehavedChildren_WeShouldFindTheRightMinimumNumberOfRows(Dictionary<int, HashSet<int>> relations, int expectedResult)
        {
            var graph = new DirectedGraph<int>();
            foreach (var item in relations.Keys)
            {
                graph.insertNode(item);
                foreach (var val in relations[item])
                {
                    graph.connect(item, val);
                }
            }

            var nbRows = Chapter5.getLineOrderB(graph);

            Assert.AreEqual(expectedResult, nbRows);
        }

        public static IEnumerable<object[]> arborescenceTestInput()
        {
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(0, 3), new(0, 4) }, true};
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(0, 2), new(4, 3), new(5, 4) }, false};
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(2, 3), new(3, 4), new(2, 4) },true };
            yield return new object[] { new List<Tuple<int, int>>() { new(0, 1), new(1, 2), new(0, 3) }, true};
        }

        [TestMethod]
        [DynamicData(nameof(arborescenceTestInput), DynamicDataSourceType.Method)]
        public void whenAGraphContainsAnArborescence_WeShouldFindIt(List<Tuple<int, int>> connections, bool containsArborescence) 
        {
            DirectedGraph<int> graph = new DirectedGraph<int>();

            foreach (var rel in connections) 
            {
                graph.connect(rel.Item1, rel.Item2);
            }

            Assert.AreEqual(containsArborescence, Chapter5.graphContainsAnArborescence(graph));
        }

    }
}
