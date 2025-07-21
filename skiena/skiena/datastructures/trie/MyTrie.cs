using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trie
{
    public class MyTrie
    {
        private Dictionary<char, MyTrieNode> roots = new Dictionary<char, MyTrieNode>();
        public void insertWord(string word) 
        {
            if (!roots.ContainsKey(word[0]))
            {
                roots.Add(word[0], new MyTrieNode(word, 0));
            }
            else 
            {
                roots[word[0]].insert(word, 1);
            }
        }
        public bool contains(string word) 
        {
            return roots.ContainsKey(word[0]) && roots[word[0]].containsWord(word,1);
        }
        public bool removeFirst(string word) 
        {
            bool removed = roots.ContainsKey(word[0]) && roots[word[0]].removeFirst(word, 0);
            if (removed && !roots[word[0]].hasOccurence()) 
            {
                roots.Remove(word[0]);
            }
            return removed;
        }
    }
}
