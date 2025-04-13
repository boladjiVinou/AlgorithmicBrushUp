using skiena;
using skiena.datastructures;
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
        public void givenAlistIfAValueIsAddedTheCountShouldIncrease() 
        {
            MySingleLinkedList<int> data = new MySingleLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                data.add(i);
            }

            Assert.AreEqual(10, data.count());
        }
        [TestMethod]
        public void givenASortedListWhenReverseIsCalledThenTheOrderShouldBeInverse() 
        {
            MySingleLinkedList<int> data = new MySingleLinkedList<int>();
            for (int i = 0; i > 10; i++) 
            {
                data.add(i);
            }

            data.reverse();

            int expected = 9;
            foreach(var curr in data)
            {
                Assert.AreEqual(expected, curr);
                --expected;
            }
        }
        [TestMethod]
        public void givenAListWithDuplicateNumberWhenRemoveIsCalledAllOccurenceShouldBeRemoved() 
        {
            MySingleLinkedList<int> data = new MySingleLinkedList<int>();
            data.add(1);
            data.add(2);
            data.add(1);
            data.add(3);

            data.remove(1);

            var enumerator = data.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual(2, enumerator.Current);
            enumerator.MoveNext();
            Assert.AreEqual(3, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
