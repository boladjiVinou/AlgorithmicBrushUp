using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trie
{
    public class MyTrieNode
    {
        private char c;
        private int endOfStringCount = 0;
        private int nbOccurence = 1;
        public Dictionary<char, MyTrieNode> children = new Dictionary<char, MyTrieNode>();

        public MyTrieNode(string word, int idx) 
        {
            c = word[idx];
            if (word.Length > idx+ 1)
            {
                insert(word, idx+1);
            }
            else 
            {
                endOfStringCount = 1;
            }
        }
        public void insert(string word, int idx)
        {
            ++nbOccurence;
            if (idx >= word.Length)
            {
                return;
            }

            if (!children.ContainsKey(word[idx]))
            {
                children.Add(word[idx], new MyTrieNode(word, idx));
            }
            else 
            {
                children[word[idx]].endOfStringCount += (idx == word.Length - 1) ? 1 : 0;
                children[word[idx]].nbOccurence += 1;
                children[word[idx]].insert(word, idx + 1);
            }
        }

        private bool isEndOfString() 
        {
            return endOfStringCount > 0;
        }
        public bool hasOccurence() 
        {
            return nbOccurence > 0;
        }

        public bool containsWord(string word, int i)
        {
            if (i == word.Length)
            {
                return isEndOfString();
            }
            if (children.ContainsKey(word[i]))
            {
                return children[word[i]].containsWord(word, i + 1);
            }
            return false;
        }

        public bool removeFirst(string word, int idx)
        {
            if (!children.ContainsKey(word[idx])) 
            {
                return false;
            }
            if (idx + 1 < word.Length)
            {
                bool nextPartRemoved = children[word[idx]].removeFirst(word, idx + 1);
                if (!nextPartRemoved)
                {
                    return false;
                }
                if (children[word[idx]].nbOccurence == 0)
                {
                    children.Remove(word[idx]);
                }
            }
            else 
            {
                --endOfStringCount;
            }
            --nbOccurence;
            return true;
        }
    }
}
