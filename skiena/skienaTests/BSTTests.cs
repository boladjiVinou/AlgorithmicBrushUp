using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public sealed class BSTTests
    {
        [TestMethod]
        public void givenABSWithDataWhenIteratingThenTheOrderShouldBeRespected()
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();
            
            for (int j = 0; j < 10; j++) 
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }
            vals.Sort();

            int i = 0;
            foreach (int val in bst)
            {
                Assert.AreEqual(vals[i], val);
                ++i;
            }
        }
        [TestMethod]
        public void givenABSTWithDataWhenDeletingAValueThenTheBSTShouldStayABST() 
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }

            bst.remove(vals[random.Next(vals.Count)]);

            Assert.IsTrue(bst.isABST());
        }

        [TestMethod]
        public void givenABSTWhenInsertingAValueThenTheBSTShouldStayABST()
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }

            Assert.IsTrue(bst.isABST());
        }

        [TestMethod]
        public void givenABSTWithDataTheMaximumShouldBeTheGreatestValue() 
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }

            Assert.AreEqual(vals.Max(), bst.getMaximum());
        }

        [TestMethod]
        public void givenABSTWithDataTheMinimumShouldBeTheLowestValue()
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }

            Assert.AreEqual(vals.Min(), bst.getMinimum());
        }

        [TestMethod]
        public void givenABSTWithDataWhenIteratingInPreorderThenThePreorderShouldBeValid() 
        {
            MyBST<int> bst = new MyBST<int>();
            bst.add(10);
            bst.add(5);
            bst.add(15);
            bst.add(8);
            bst.add(2);
            bst.add(12);
            bst.add(17);

            List<int> preorder = new List<int>() { 10, 5, 2, 8, 15, 12, 17 };
            int i = 0;
            foreach (int val in bst.preOrderIteration()) 
            {
                Assert.AreEqual(preorder[i], val);
                i++;
            }

        }


        [TestMethod]
        public void givenABSTWithDataWhenIteratingInPostorderThenThePreorderShouldBeValid()
        {
            MyBST<int> bst = new MyBST<int>();
            bst.add(10);
            bst.add(5);
            bst.add(15);
            bst.add(8);
            bst.add(2);
            bst.add(12);
            bst.add(17);

            List<int> postOrder = new List<int>() { 2,8,5,12,17,15,10};
            int i = 0;
            foreach (int val in bst.postOrderIteration())
            {
                Assert.AreEqual(postOrder[i], val);
                i++;
            }
        }

        [TestMethod]
        public void givenABSTWithDataWhenIteratingInOrderThenThePreorderShouldBeValid()
        {
            MyBST<int> bst = new MyBST<int>();
            Random random = new Random();
            List<int> vals = new List<int>();

            for (int j = 0; j < 10; j++)
            {
                int tmp = random.Next(1000);
                vals.Add(tmp);
                bst.add(tmp);
            }
            vals.Sort();

            int i = 0;
            foreach (int val in bst.inOrderIteration())
            {
                Assert.AreEqual(vals[i], val);
                ++i;
            }
        }
    }
}
