using skiena.datastructures.lists;
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

       // public static void 
    }
}
