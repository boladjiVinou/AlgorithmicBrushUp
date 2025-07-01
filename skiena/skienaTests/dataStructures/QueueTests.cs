using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void whenAQueueIsEmpty_thenItShouldTellIt()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.enqueue(1);
            queue.dequeue();

            Assert.IsTrue(queue.getSize() == 0);
        }
        [TestMethod]
        public void whenInsertingDataInAQueue_thenTheItemShouldBePoppedInInsertionOrder()
        {
            MyQueue<int> queue = new MyQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.enqueue(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(queue.dequeue(), i);
            }
        }
        [TestMethod]
        public void whenInsertingDataInAQueue_ThenTheSizeShouldBeUpdated()
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.enqueue(1);
            queue.enqueue(2);

            Assert.AreEqual(queue.getSize(), 2);

        }
    }
}
