using skiena.datastructures.lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyStack<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private LinkedNode<T>? root;
        private int size = 0;
        public void push(T val)
        {
            var next = new LinkedNode<T>(val);
            if (root == null)
            {
                root = next;
            }
            else
            {
                next.Next = root;
                root = next;
            }
            ++size;
        }

        public T pop()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Empty stack");
            }
            T val = root.Value;
            root = root.Next;
            --size;// cant be negative an exception will be thrown earlier
            return val;
        }

        public int getSize()
        {
            return size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var tmp = root;
            while (tmp != null)
            {
                yield return tmp.Value;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
