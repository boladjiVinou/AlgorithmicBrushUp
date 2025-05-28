using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void whenAStackIsEmpty_thenItShouldTellIt()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.push(1);
            stack.pop();

            Assert.IsTrue(stack.getSize() == 0);
        }
        [TestMethod]
        public void whenInsertingDataInAStack_thenTheItemShouldBePoppedInReversedOrder() 
        {
            MyStack<int> stack = new MyStack<int>();
            for (int i = 0; i < 10; i++) 
            {
                stack.push(i);
            }

            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(stack.pop(), i);
            }
        }
        [TestMethod]
        public void whenInsertingDataInAStack_ThenTheSizeShouldBeUpdated()
        {
            MyStack<int> stack = new MyStack<int>();
            stack.push(1);
            stack.push(2);

            Assert.AreEqual(stack.getSize(), 2);

        }

    }
}
