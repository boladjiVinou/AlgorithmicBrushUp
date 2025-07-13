using skiena.datastructures.trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class TrieTest
    {
        [TestMethod]
        public void whenInsertingDataInATrieItShouldBePresent() 
        {
            var input = new List<string>() { "test" , "testing" , "testable", "bicycle" };
            var trie = new MyTrie();

            for (int i = 0; i < input.Count; i++) 
            {
                trie.insertWord(input[i]);
            }

            for (int i = 0; i < input.Count; i++)
            {
                Assert.IsTrue(trie.contains(input[i]));
            }
        }

        [TestMethod]
        public void whenInsertingDataInATrieAnAbsentWordShouldNotBePresent()
        {
            var input = new List<string>() { "test", "testing", "testable", "bicycle" };
            var trie = new MyTrie();

            for (int i = 0; i < input.Count; i++)
            {
                trie.insertWord(input[i]);
            }

            Assert.IsFalse(trie.contains("tes"));
            Assert.IsFalse(trie.contains("bic"));
        }


        [TestMethod]
        public void givenATrieWithDataWhenAWordIsRemovedItShouldNotBePresent()
        {
            var input = new List<string>() { "test", "testing", "testable", "bicycle" };
            var trie = new MyTrie();

            for (int i = 0; i < input.Count; i++)
            {
                trie.insertWord(input[i]);
            }

            Random rand = new Random();
            int idx = rand.Next(input.Count);

            Assert.IsTrue(trie.removeFirst(input[idx]));
            Assert.IsFalse(trie.contains(input[idx]));
        }

        [TestMethod]
        public void givenATrieWithDataWhenADuplicateIsRemovedItShouldStillBePresent()
        {
            var input = new List<string>() { "test", "testing", "testable", "bicycle" };
            var trie = new MyTrie();

            for (int i = 0; i < input.Count; i++)
            {
                trie.insertWord(input[i]);
            }

            Random rand = new Random();
            int idx = rand.Next(input.Count);
            trie.insertWord(input[idx]);

            Assert.IsTrue(trie.removeFirst(input[idx]));
            Assert.IsTrue(trie.contains(input[idx]));
        }
    }
}
