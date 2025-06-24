using skiena.Chapter3;
using skiena.datastructures;
using skiena.datastructures.trees;
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
                Assert.IsFalse(tree.contains(k - nbDeleted));
                ++nbDeleted;
            }
        }
        [TestMethod]
        public void whenASmallerTreeIsMergedIntoAGreaterOneAllDataShouldBeThere() 
        {
            MyBST<int> t1 = new MyBST<int>();
            for (int i = 0; i < 10; i++) 
            {
                t1.add(i);
            }
            MyBST<int> t2 = new MyBST<int>();
            for (int i = 10; i < 20; i++) 
            {
                t2.add(i);
            }


            Chapter3.mergeSmallerTree(t1, t2);


            for (int i = 0; i < 20; i++)
            {
                Assert.IsTrue(t2.contains(i));
            }
        }

        [TestMethod]
        public void whenASmallerTreeIsMergedIntoAGreaterOneAllDataShouldBeSorted()
        {
            MyBST<int> t1 = new MyBST<int>();
            for (int i = 0; i < 10; i++)
            {
                t1.add(i);
            }
            MyBST<int> t2 = new MyBST<int>();
            for (int i = 10; i < 20; i++)
            {
                t2.add(i);
            }



            Chapter3.mergeSmallerTree(t1, t2);


            int counter = 0;
            foreach(int val in t2.inOrderIteration())
            {
                Assert.AreEqual(counter, val);
                counter++;
            }
        }

        [TestMethod]
        public void whenTwoTreesAreMergedThenDataShouldBeSorted() 
        {
            List<int> allData = new List<int>();
            MyBST<int> t1 = new MyBST<int>();
            MyBST<int> t2 = new MyBST<int>();
            Random random = new Random();
            for (int i = 0; i < 10; i++) 
            {
                int tmp = random.Next(500);
                allData.Add(tmp);
                t1.add(tmp);

                tmp = random.Next(500);
                allData.Add(tmp);
                t2.add(tmp);
            }

            MyBST<int> mergedTree = MyBST<int>.merge(t1, t2);


            allData.Sort();
            int c = 0;
            var insertedData = mergedTree.inOrderIteration().ToList();
            foreach (int v in mergedTree.inOrderIteration()) 
            {
                Assert.AreEqual(allData[c], v);
                c++;
            }
            Assert.AreEqual(allData.Count,c);
        }
    }
}
