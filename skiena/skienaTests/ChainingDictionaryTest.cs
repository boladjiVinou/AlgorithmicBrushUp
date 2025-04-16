using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class ChainingDictionaryTest
    {
        [TestMethod]
        public void givenAChainingDictionaryWhenAnElementIsInsertedTwiceItShouldNotIncreaseTheSize() 
        {
            MyChainingDictionary<int> dict = new MyChainingDictionary<int>();

            dict.Add(1);
            dict.Add(2);
            dict.Add(1);

            Assert.AreEqual(2, dict.getSize());
        }

        [TestMethod]
        public void givenAChainingDictionaryWhenAnElementIsInsertedItShouldBeContained()
        {
            MyChainingDictionary<int> dict = new MyChainingDictionary<int>();

            dict.Add(1);

            Assert.IsTrue(dict.Contains(1));
        }


        [TestMethod]
        public void givenAChainingDictionaryWhenAnElementIsRemovedItShouldNotBeContained()
        {
            MyChainingDictionary<int> dict = new MyChainingDictionary<int>();
            dict.Add(1);

            dict.Remove(1);

            Assert.IsFalse(dict.Contains(1));
        }

        [TestMethod]
        public void givenAChainingDictionaryItShouldBeIterable()
        {
            MyChainingDictionary<int> dict = new MyChainingDictionary<int>();
            dict.Add(1);
            dict.Add(2);
            dict.Add(3);

            int n = 1;
            foreach (var item in dict) 
            {
                Assert.AreEqual(n, item);
                ++n;
            }
        }
    }
}
