using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena
{
    public class Chapter3
    {
        /*
         A common problem for compilers and text editors is determining whether the
        parentheses in a string are balanced and properly nested. For example, the string
        ((())())() contains properly nested pairs of parentheses, which the strings )()( and
        ()) do not. Give an algorithm that returns true if a string contains properly nested
        and balanced parentheses, and false if otherwise. For full credit, identify the position
        of the first offending parenthesis if the string is not properly nested and balanced.
         */
        public static Tuple<bool, int> parenthesisDetector(string input) 
        {
            Stack<int> parenthesisIndexes = new Stack<int>();
            for (int i = 0; i < input.Length; i++) 
            {
                if (parenthesisIndexes.Count > 0 && 
                    (input[parenthesisIndexes.Peek()] + 1 == input[i]))
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
         3-2. [3] Write a program to reverse the direction of a given singly-linked list. In other
        words, after the reversal all pointers should now point backwards. Your algorithm
        should take linear time.
         */
        public static void reverseList(MySingleLinkedList<int> list) 
        {
            list.reverse();
        }
    }
}
