using skiena.datastructures.lists;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyMinStack<T>:MyStack<T> where T : IEquatable<T>,IComparable<T>
    {
        Stack<T> minStack = new();
        public void push(T val)
        {
            base.push(val);
            if (minStack.Count > 0 && minStack.Peek().CompareTo(val) < 0)
            {
                minStack.Push(minStack.Peek());
                return;
            }
            minStack.Push(val);
        }

        public T pop()
        {
            minStack.Pop();
            return base.pop();
        }

        public T getMin() 
        {
            return minStack.Peek();
        }
    }
}
