using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public sealed class ListTests
    {
        [TestMethod]
        public void GivenAlistIfAValueIsAddedTheCountShouldIncrease()
        {
            MySingleLinkedList<int> data = new MySingleLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                data.add(i);
            }

            Assert.AreEqual(10, data.count());
        }
        [TestMethod]
        public void GivenASortedListWhenReverseIsCalledThenTheOrderShouldBeInverse()
        {
            MySingleLinkedList<int> data = new MySingleLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                data.add(i);
            }

            data.reverse();

            Assert.AreEqual(10, data.Count());

            int expected = 9;
            foreach (var curr in data)
            {
                Assert.AreEqual(expected, curr);
                --expected;
            }
        }
        [TestMethod]
        public void GivenAListWithDuplicateNumberWhenRemoveIsCalledAllOccurenceShouldBeRemoved()
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

        [TestMethod]
        public void GivenASortedDoubleLinkedListWhenReverseIsCalledThenTheOrderShouldBeInverse()
        {
            var data = new MyDoubleLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                data.add(i);
            }

            data.reverse();

            Assert.AreEqual(10, data.Count());

            int expected = 9;
            foreach (var curr in data)
            {
                Assert.AreEqual(expected, curr);
                --expected;
            }
        }
        [TestMethod]
        public void GivenADoubleLinkedListWithDuplicateNumberWhenRemoveIsCalledAllOccurenceShouldBeRemoved()
        {
            var data = new MyDoubleLinkedList<int>();
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
