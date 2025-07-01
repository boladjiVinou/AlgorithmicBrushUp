using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public sealed class AVlTests : BSTTests
    {
        protected override MyBST<int> createTree()
        {
            return new MyAvlTree<int>();
        }
        [TestMethod]
        public void whenInsertingDataInAVLTree_ThenTheTreeShouldNotContainLoop()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
                Assert.IsFalse(tree.containsLoop());
            }
        }
        [TestMethod]
        public void whenInsertingDataInAVLTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsTrue(tree.areAllNodesBalanced());
        }


        [TestMethod]
        public void whenRemovingLeafInAVLTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(73);

            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsTrue(tree.areAllNodesBalanced());
        }

        [TestMethod]
        public void whenRightDoubleRotationIsDoneInAVLTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(22);

            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsTrue(tree.areAllNodesBalanced());
        }


        [TestMethod]
        public void whenLeftDoubleRotationIsDoneInAVLTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            tree.remove(2);
            tree.remove(0);
            tree.remove(3);

            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsTrue(tree.areAllNodesBalanced());
        }

        [TestMethod]
        public void whenRemovingRootInAVLTree_ThenTheTreeShouldBeBalanced()
        {
            List<int> data;
            MyAvlTree<int> tree;
            createFilledAVLTree(out data, out tree);
            foreach (var item in data)
            {
                tree.add(item);
            }

            Assert.IsTrue(tree.getRootValue() == 41);
            tree.remove(41);

            Assert.IsTrue(tree.isRootBalanced());
            Assert.IsTrue(tree.areAllNodesBalanced());
        }

        private void createFilledAVLTree(out List<int> data, out MyAvlTree<int> tree)
        {
            data = new List<int>() { 41,
            67,
            34,
            0,
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
            tree = (MyAvlTree<int>)createTree();
        }
    }
}
