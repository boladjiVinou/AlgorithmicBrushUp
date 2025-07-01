using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class MyDequeTests
    {
        [TestMethod]
        public void whenADequeIsEmpty_thenItShouldTellIt()
        {
            MyDeque<int> deque = new MyDeque<int>();
            deque.pushFront(1);
            deque.popEnd();

            Assert.IsTrue(deque.getSize() == 0);
        }

        [TestMethod]
        public void whenInsertingDataInADequeFromFront_thenTheItemShouldBeFrontPoppedInReversedOrder()
        {
            MyDeque<int> deque = new MyDeque<int>();
            for (int i = 0; i < 10; i++)
            {
                deque.pushFront(i);
            }

            for (int i = 9; i>= 0; --i)
            {
                Assert.AreEqual(deque.popFront(), i);
            }
        }

        [TestMethod]
        public void whenInsertingDataInADequeFromFront_thenTheItemShouldBeBackPoppedInOrder()
        {
            MyDeque<int> deque = new MyDeque<int>();
            for (int i = 0; i < 10; i++)
            {
                deque.pushFront(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(deque.popEnd(), i);
            }
        }
        [TestMethod]
        public void whenInsertingDataInADeque_ThenTheSizeShouldBeUpdated()
        {
            MyDeque<int> deque = new MyDeque<int>();
            deque.pushFront(1);
            deque.pushFront(2);

            Assert.AreEqual(deque.getSize(), 2);
        }

        [TestMethod]
        public void whenInsertingDataInADequeFromBack_thenTheItemShouldBeFrontPoppedInOrder()
        {
            MyDeque<int> deque = new MyDeque<int>();
            for (int i = 0; i < 10; i++)
            {
                deque.pushEnd(i);
            }

            for (int i = 0; i <10; ++i)
            {
                Assert.AreEqual(deque.popFront(), i);
            }
        }


        [TestMethod]
        public void whenInsertingDataInADequeFromBack_thenTheItemShouldBeBackPoppedInReversedOrder()
        {
            MyDeque<int> deque = new MyDeque<int>();
            for (int i = 0; i < 10; i++)
            {
                deque.pushEnd(i);
            }

            for (int i =9; i>=0; --i)
            {
                Assert.AreEqual(deque.popEnd(), i);
            }
        }
    }
}
