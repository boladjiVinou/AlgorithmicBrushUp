using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class MyMinStackTest: StackTest
    {
        [TestMethod]
        public void whenPushingDataInTheStack_TheCorrectMinimumShouldBeGiven() 
        {
            MyMinStack<int> st = new MyMinStack<int>();
            Random random = new Random();
            List<int> insertedData = new List<int>();
            for (int i = 0; i < 10; i++) 
            {
                insertedData.Add(random.Next(1000));
                st.push(insertedData.Last());

                Assert.AreEqual(insertedData.Min(), st.getMin());
            }
        }

        [TestMethod]
        public void whenPopingDataInTheStack_TheCorrectMinimumShouldBeGiven()
        {
            MyMinStack<int> st = new MyMinStack<int>();
            Random random = new Random();
            List<int> insertedData = new List<int>();
            Stack<int> expectedMin = new Stack<int>();
            for (int i = 0; i < 10; i++)
            {
                insertedData.Add(random.Next(1000));
                st.push(insertedData.Last());
                expectedMin.Push(insertedData.Min());
            }

            for (int i = 0; i < insertedData.Count; i++) 
            {
                Assert.AreEqual(expectedMin.Peek(), st.getMin());
                expectedMin.Pop();
                st.pop();
            }
        }
    }
}
