using skiena.Chapter3;
using skiena.datastructures;
namespace skienaTests
{
   
    [TestClass]
    public sealed class Chapter3Tests
    {
        [TestMethod]
        public void givenABalancedInputTheExercice1ShoulDetectValidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector("((())())()");
            Assert.IsTrue(result.Item1);
            Assert.IsTrue(result.Item2 < 0);
        }
        [TestMethod]
        public void givenAnUnbalancedInputAtStartTheExercice1ShoulDetectInvalidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector(")()(");
            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == 0);
        }
        [TestMethod]
        public void givenAnUnbalancedInputAtEndTheExercice1ShoulDetectInvalidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector("())");
            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == 2);
        }

        [TestMethod]
        public void whenTheKthElementIsAskedToBeRemovedThenItShouldBeRemovedFromTree() 
        {
            var tree = Chapter3.buildCustomTree();
            for (int i = 1; i <= 15; i++) 
            {
                tree.add(i);
            }
            tree.deleteKthSmallest(8);
            // 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsFalse(tree.contains(8));
        }


        [TestMethod]
        public void whenKthElementAreAskedToBeRemovedThenTheyShouldBeRemovedFromTree()
        {
            var tree = Chapter3.buildCustomTree();
            HashSet<int> data = new HashSet<int>();
            for (int i = 1; i <= 15; i++)
            {
                tree.add(i);
                data.Add(i);
            }
            int nbDeleted = 0;
            foreach(int k in data)
            {
                tree.deleteKthSmallest(k);
                Assert.IsTrue(tree.isRootBalanced());
                Assert.IsFalse(tree.contains(k- nbDeleted));
                ++nbDeleted;
            }
        }
    }
}
