using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{

    [TestClass]
    public class RedBlackTreeTests : BSTTests
    {
        protected override MyBST<int> createTree()
        {
            return new MyRedBlackTree<int>();
        }

        [TestMethod]
        public void whenInsertingDataInRedBlackTree_ThenTheTreeShouldNotContainLoop()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
                Assert.IsFalse(tree.containsLoop());
            }
        }
        [TestMethod]
        public void whenInsertingDataInRedBlackTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            Assert.IsTrue(tree.isTreeValid());
        }


        [TestMethod]
        public void whenRemovingLeafInRedBlackTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(5);

            Assert.IsTrue(tree.isTreeValid());
        }

        [TestMethod]
        public void whenRemovingLeftBlackNodeWithRedParentAndRedChild_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(2);

            Assert.IsTrue(tree.isTreeValid());
        }

        [TestMethod]
        public void whenRemovingRightBlackNodeWithRedParentAndRedChild_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(78);

            Assert.IsTrue(tree.isTreeValid());
        }

        [TestMethod]
        public void whenRemovingRootINTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyRedBlackTree<int> tree;
            createFilledRedBlackTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            Assert.IsTrue(tree.getRootValue() == 41);
            tree.remove(41);

            Assert.IsTrue(tree.isTreeValid());
        }



        private void createFilledRedBlackTree(out List<int> data, out MyRedBlackTree<int> tree)
        {
            data = new List<int>() { 41,
            67,
            34,
            1,
            69,
            24,
            78,
            58,
            62,
            64,
            5,
            45,
            81,
            27,
            61,
            91,
            95,
            42,
            27,
            36,
            91,
            4,
            2,
            53,
            92,
            82,
            21,
            16,
            18,
            95,
            47,
            26,
            71,
            38,
            69,
            12,
            67,
            99,
            35,
            94,
            3,
            11,
            22,
            33,
            73,
            64,
            41,
            11,
            53,
            68};
            tree = (MyRedBlackTree<int>)createTree();
        }
    }
}
