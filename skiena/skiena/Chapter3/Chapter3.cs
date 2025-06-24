using skiena.datastructures.lists;
using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3
{
    public class Chapter3
    {
        /*
         3-1
         */
        public static Tuple<bool, int> parenthesisDetector(string input) 
        {
            Stack<int> parenthesisIndexes = new Stack<int>();
            for (int i = 0; i < input.Length; i++) 
            {
                if (parenthesisIndexes.Count > 0 && 
                    input[parenthesisIndexes.Peek()] + 1 == input[i])
                {
                    parenthesisIndexes.Pop();
                }
                else 
                {
                    parenthesisIndexes.Push(i);
                }
            }
            bool success = parenthesisIndexes.Count == 0;
            while (parenthesisIndexes.Count > 1) 
            {
                parenthesisIndexes.Pop();
            }
            int idx = -1;
            if (parenthesisIndexes.Count == 1) 
            {
                idx =  parenthesisIndexes.Pop();
            }
            return new Tuple<bool, int>(success,idx );
        }
        /*
         3-2
         */
        public static void reverseList(MySingleLinkedList<int> list) 
        {
            list.reverse();
        }

        /**
         3.8
         */
        public static MyCustomAvlTree<int> buildCustomTree()
        {
            return new MyCustomAvlTree<int>();
        }
        /*
         3.9
         */
        public static void mergeSmallerTree(MyBST<int> smaller, MyBST<int> bigger)
        {
           bigger.mergeSmallerTree(smaller);
        }
        public static MyBST<int> merge(MyBST<int> t1, MyBST<int> t2)
        {
            return MyBST<int>.merge(t1, t2);
        }
    }
}
