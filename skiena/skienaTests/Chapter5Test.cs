using skiena.Chapter5;
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

        }
    }
}
