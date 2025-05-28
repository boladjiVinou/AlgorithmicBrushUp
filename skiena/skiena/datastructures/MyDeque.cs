using skiena.datastructures.lists;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyDeque<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private LinkedNode<T>? root;
        private LinkedNode<T>? last;
        private int size = 0;
        public void pushEnd(T val)
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
                next.Previous = last;
                last = next;
            }
            ++size;
        }
        public void pushFront(T val)
        {
            var next = new LinkedNode<T>(val);
            if (last == null)
            {
                root = next;
                last = root;
            }
            else
            {
                next.Next = root;
                root.Previous = next;
                root = next;
            }
            ++size;
        }

        public T popFront()
        {
            if (root == null)
            {
                throw new InvalidOperationException("Empty deque");
            }
            T val = root.Value;
            bool updateLast = root == last;
            root = root.Next;
            if (root != null) 
            {
                root.Previous = null;
            }
            if (updateLast)
            {
                last = root;
            }
            --size;// cant be negative an exception will be thrown earlier
            return val;
        }

        public T popEnd() 
        {
            if (root == null)
            {
                throw new InvalidOperationException("Empty deque");
            }
            T val = last.Value;
            bool updateRoot = root == last;
            last = last.Previous;
            if (last != null) 
            {
                last.Next = null;
            }
            if (updateRoot)
            {
                root = last;
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
