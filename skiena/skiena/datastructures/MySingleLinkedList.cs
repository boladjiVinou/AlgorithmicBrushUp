using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MySingleLinkedList<T>  : IEnumerable<T> where T : IEquatable<T>
    {
        public SingleLinkedNode<T> root { get; set; }

        public int count() 
        {
            SingleLinkedNode<T> curr = root;
            int count = 0;
            while (curr != null) 
            {
                ++count;
                curr = curr.Next;
            }
            return count;
        }

        public void reverse() 
        {
            SingleLinkedNode<T> curr = root;
            SingleLinkedNode<T> prev = null;
            while (curr != null) 
            {
                if (prev != null) 
                {
                    prev.Next = curr;
                }
                prev = curr;
                curr = curr.Next;
            }
            root = prev;
        }

        public void add(T value) 
        {
            if (value == null) 
            {
                return;
            }
            var tmp = new SingleLinkedNode<T>(value);
            if (root == null)
            {
                root = tmp;
            }
            else 
            {
                SingleLinkedNode<T> curr = root;
                SingleLinkedNode<T> prev = null;
                while (curr != null)
                {
                    prev = curr;
                    curr = curr.Next;
                }
                prev.Next = tmp;
            }
        }

        public void remove(T value) 
        {
            if (value == null)
            {
                return;
            }
            var curr = root;
            SingleLinkedNode<T> prev = null;
            while (curr != null) 
            {
                if (!curr.Value.Equals(value)) 
                {
                    prev = curr;
                    curr = curr.Next;
                    continue;
                }
                if (prev != null)
                {
                    prev.Next = curr.Next;
                }
                else 
                {
                    root = curr.Next;
                }
                curr = curr.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var curr = root;
            while (curr != null) 
            {
                yield return curr.Value;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class SingleLinkedNode<T>(T val) where T : IEquatable<T>
    {
        public T Value { get; set; } = val;
        public SingleLinkedNode<T> Next { get; set; }
    }
}
