using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class MyHeapTest
    {
        [TestMethod]
        public void givenNumbersAreInsertedInHeap_ThenTheSizeShouldBeExact()
        {
            MyHeap<int> heap = new MyHeap<int>();
            for (int i = 0; i < 10; i++)
            {
                heap.insert(i);
            }

            Assert.AreEqual(10, heap.getSize());
        }

        [TestMethod]
        public void givenElementIsRemovedInHeap_ThenTheSizeShouldBeExact()
        {
            MyHeap<int> heap = new MyHeap<int>();
            for (int i = 0; i < 10; i++)
            {
                heap.insert(i);
            }

            heap.removeTop();

            Assert.AreEqual(9, heap.getSize());
        }
        [TestMethod]
        public void givenNumbersAreInsertedInHeap_ThenTheGreatestShouldBeOnTop() 
        {
            MyHeap<int> heap = new MyHeap<int>();
            for (int i = 0; i < 10; i++) 
            {
                heap.insert(i);
            }

            Assert.AreEqual(9, heap.peekTop());
        }

        [TestMethod]
        public void givenTopIsRemovedFromHeap_ThenTheNextGreatestShouldBeOnTop()
        {
            MyHeap<int> heap = new MyHeap<int>();
            for (int i = 0; i < 10; i++)
            {
                heap.insert(i);
            }

            heap.removeTop();

            Assert.AreEqual(8, heap.peekTop());
        }

        [TestMethod]
        public void givenRandomNumbersAreInHeap_ThenTheElementsShouldBeRemovedInOrder()
        {
            Random random = new Random();
            MyHeap<int> heap = new MyHeap<int>();
            for (int i = 0; i < 20; i++)
            {
                int tmp = random.Next(100);
                heap.insert(tmp);
            }

            int prevMax = heap.removeTop();
            for (int i = 0; i < 19; i++) 
            {
                Assert.IsTrue(prevMax >= heap.peekTop());
                prevMax = heap.removeTop();
            }
        }
    }
}
