using skiena.datastructures.lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyQueue<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private LinkedNode<T>? root;
        private LinkedNode<T>? last;
        private int size = 0;
        public void enqueue(T val) 
        {
            var next = new LinkedNode<T>(val);
            if (last == null)
            {
                root = next;
                last = root;
            }
            else
            {
                last.Next = next;
                last = next;
            }
            ++size;
        }

        public T dequeue() 
        {
            if (root == null) 
            {
                throw new InvalidOperationException("Empty queue");
            }
            T val = root.Value;
            bool updateLast = root == last;
            root = root.Next;
            if (updateLast) 
            {
                last= root;
            }
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
