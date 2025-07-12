using skiena.Chapter3;
using skiena.Chapter3.applicationOfTree;
using skiena.datastructures;
using skiena.datastructures.lists;
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
            foreach (int v in mergedTree.inOrderIteration()) 
            {
                Assert.AreEqual(allData[c], v);
                c++;
            }
            Assert.AreEqual(allData.Count,c);
        }
        [TestMethod]
        public void whenSolvingTheBinPackingProblemWithBestFit_ThenTheExpectedResultShouldBeOk() 
        {
            // 0.6  || 1
            // 0.5  || 0.9 || 1
            // 0.5

            double[] weights = { 0.1,0.2,0.3,0.5, 0.4, 0.4,0.3,0.2,0.1};

            var bins = Chapter3.binPackingProblem(weights, Chapter3.searchBestFitBin);

            Assert.AreEqual(3,bins.inOrderIteration().Count());
            double[] expectedSpace = { 0d,0d,0.5d };
            int i = 0;
            foreach (var bin in bins.inOrderIteration()) 
            {
                Assert.IsTrue(Math.Abs(expectedSpace[i] - bin.getSpace()) < Bin.TRESHOLD);
                ++i;
            }
        }

        [TestMethod]
        public void whenSolvingTheBinPackingProblemWithWorstFit_ThenTheExpectedResultShouldBeOk()
        {
            // 0.6 || 1
            // 0.5 || 0.9
            // 0.6

            double[] weights = { 0.1, 0.2, 0.3, 0.5, 0.4, 0.4, 0.3, 0.2, 0.1 };

            var bins = Chapter3.binPackingProblem(weights, Chapter3.searchWorstFitBin);

            Assert.AreEqual(3, bins.inOrderIteration().Count());
            double[] expectedSpace = { 0d, 0.1d, 0.4d };
            int i = 0;
            foreach (var bin in bins.inOrderIteration())
            {
                Assert.IsTrue(Math.Abs(expectedSpace[i] - bin.getSpace()) < Bin.TRESHOLD);
                ++i;
            }
        }

        [TestMethod]
        public void whenUsingMatrixToFindSmallestThenTheValueShouldBeCorrect() 
        {
            int[] data = { 1, 2, 5, 4, 2, 6, 4, 2 };
            var smallestsMatrix = Chapter3.buildNSquareDataStructureToFindSmallest(data);

            for (int i = 0; i < data.Length; i++) 
            {
                int min = data[i];
                for (int j = i; j < data.Length; j++) 
                {
                    if (min > data[j]) 
                    {
                        min = data[j];
                    }

                    Assert.AreEqual(min, smallestsMatrix[i][j]);
                }
            }
        }

        [TestMethod]
        public void whenUsingSegmentTreeToFindSmallestThenTheValueShouldBeCorrect()
        {
            int[] data = { 1, 2, 5, 4, 2, 6, 4, 2 };
            var segmentTree = Chapter3.buildSegmentTreeToFindSmallest(data);

            for (int i = 0; i < data.Length; i++)
            {
                int min = data[i];
                for (int j = i; j < data.Length; j++)
                {
                    if (min > data[j])
                    {
                        min = data[j];
                    }

                    Assert.AreEqual(min, segmentTree.getResultBetween(i,j));
                }
            }
        }
        [TestMethod]
        public void givenAPartialSumTreeWhenInsertingDataTheSumShouldBeCalculated()
        {
            var tree = Chapter3.buildPartialSumCustomStructure();
            tree.insert(0,1);
            tree.insert(1, 2);
            tree.insert(2, 3);
            tree.insert(3, 4);
            tree.insert(4, 5);

            Assert.AreEqual(10, tree.getPartialSum(4));
        }

        [TestMethod]
        public void givenAPartialSumTreeWhenUpdatingAValueTheSumShouldBeRecalculated()
        {
            var tree = Chapter3.buildPartialSumCustomStructure();
            tree.insert(0, 1);
            tree.insert(1, 2);
            tree.insert(2, 3);
            tree.insert(3, 4);
            tree.insert(4, 5);

            tree.addTo(1,-2);

            Assert.AreEqual(8, tree.getPartialSum(4));
        }

        [TestMethod]
        public void givenAPartialSumTreeWhenRemovingAKeyTheSumShouldBeRecalculated()
        {
            var tree = Chapter3.buildPartialSumCustomStructure();
            tree.insert(0, 1);
            tree.insert(1, 2);
            tree.insert(2, 3);
            tree.insert(3, 4);
            tree.insert(4, 5);

            tree.remove(2);
            Assert.IsFalse(tree.contains(new KeyValue<int, long>(2, 3)));
            Assert.AreEqual(7, tree.getPartialSum(4));
        }
        [TestMethod]
        public void whenANumberIsInsertedItShouldBePresent() 
        {
            var dict = Chapter3.createIntegerDictionary(100, 10);
            Random rand = new Random();
            List<uint> insertedData = new List<uint>();
            for (int i = 0; i < 10; i++) 
            {
                uint tmp = (uint)rand.Next(100);
                insertedData.Add(tmp);
                dict.insert(tmp);
            }

            for (int i = 0; i < insertedData.Count; i++) 
            {
                Assert.IsTrue(dict.Contains(insertedData[i]));
            }
        }

        [TestMethod]
        public void whenANumberIsRemovedItShouldNotBePresent()
        {
            var dict = Chapter3.createIntegerDictionary(100, 10);
            Random rand = new Random();
            List<uint> insertedData = new List<uint>();
            for (int i = 0; i < 10; i++)
            {
                uint tmp = (uint)rand.Next(100);
                insertedData.Add(tmp);
                dict.insert(tmp);
                dict.Remove(tmp);
            }

            for (int i = 0; i < insertedData.Count; i++)
            {
                Assert.IsFalse(dict.Contains(insertedData[i]));
            }
        }

        [TestMethod]
        public void givenAClassicSentenceThenWordsShouldBeReversed() 
        {
            char[] sentence = "My Name is Chris".ToCharArray();
            Chapter3.reverseWords(sentence);
            Assert.AreEqual("Chris is Name My", new string(sentence));
        }


        [TestMethod]
        public void givenASpacedSentenceThenWordsShouldBeReversed()
        {
            char[] sentence = "My       life".ToCharArray();
            Chapter3.reverseWords(sentence);
            Assert.AreEqual("life       My", new string(sentence));
        }
        [TestMethod]
        public void givenAListWithoutLoopWhenSearchingForLoopThenWeShouldFindNothing() 
        {
            LinkedNode<int>[] nodes = new LinkedNode<int>[10];
            for (int i = 0; i < nodes.Length; i++) 
            {
                nodes[i] = new LinkedNode<int>(i);
                if (i > 0) 
                {
                    nodes[i-1].Next = nodes[i];
                }
            }
            MySingleLinkedList<int> data = new MySingleLinkedList<int>(nodes[0]);

            Assert.AreEqual(null, data.searchLoopNode());
        }


        [TestMethod]
        public void givenAListWithLoopWhenSearchingForLoopThenWeShouldFindIt()
        {
            LinkedNode<int>[] nodes = new LinkedNode<int>[10];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new LinkedNode<int>(i);
                if (i > 0)
                {
                    nodes[i - 1].Next = nodes[i];
                }
            }
            MySingleLinkedList<int> data = new MySingleLinkedList<int>(nodes[0]);


            Random random = new Random();
            int loopIdx = random.Next(nodes.Length);
            nodes[9].Next = nodes[loopIdx];

            Assert.AreEqual(nodes[loopIdx], data.searchLoopNode());
        }
    }
}
