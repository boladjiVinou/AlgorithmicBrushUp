using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.lists
{
    public class MySingleLinkedList<T>  : IEnumerable<T> , IEquatable<MySingleLinkedList<T>> where T : IEquatable<T>
    {
        public LinkedNode<T>? root { get; set; }
        public MySingleLinkedList():this(null)
        {
        }
        public MySingleLinkedList(LinkedNode<T>? root) 
        {
            this.root = root;
        }

        public int count() 
        {
            LinkedNode<T>? curr = root;
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
            LinkedNode<T>? curr = root;
            LinkedNode<T>? prev = null;
            while (curr != null) 
            {
                var tmp = curr.Next;
                curr.Next = prev;
                prev = curr;
                curr = tmp;
            }
            root = prev;
        }

        public bool isEmpty()
        {
            return root == null;
        }

        public void add(T value) 
        {
            if (value == null) 
            {
                return;
            }
            var tmp = new LinkedNode<T>(value);
            if (root == null)
            {
                root = tmp;
            }
            else 
            {
                LinkedNode<T>? curr = root;
                LinkedNode<T>? prev = null;
                while (curr != null)
                {
                    prev = curr;
                    curr = curr.Next;
                }
                prev.Next = tmp;
            }
        }

        public bool AddIfNotEquals(T value) 
        {
            if (value == null)
            {
                return false;
            }
            var tmp = new LinkedNode<T>(value);
            if (root == null)
            {
                root = tmp;
            }
            else
            {
                LinkedNode<T>? curr = root;
                LinkedNode<T>? prev = null;
                while (curr != null)
                {
                    if (curr.Value.Equals(value)) 
                    {
                        return false;
                    }
                    prev = curr;
                    curr = curr.Next;
                }
                prev.Next = tmp;
            }
            return true;
        }

        public int remove(T value) 
        {
            if (value == null)
            {
                return 0;
            }
            var curr = root;
            LinkedNode<T>? prev = null;
            int nbRemoved = 0;
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
                ++nbRemoved;
            }
            return nbRemoved;
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

        public bool Equals(MySingleLinkedList<T>? other)
        {
            if (other == null) 
            {
                return false;
            }
            var myEnumerator = GetEnumerator();
            var otherEnumerator = other.GetEnumerator();
            bool currHasNext = myEnumerator.MoveNext();
            bool otherHasNext = otherEnumerator.MoveNext();
            while (currHasNext && otherHasNext) 
            {
                if (myEnumerator.Current != null  && !myEnumerator.Current.Equals(otherEnumerator.Current))
                {
                    return false;
                }
                if (myEnumerator.Current == null && otherEnumerator.Current != null)
                {
                    return false;
                }
                currHasNext = myEnumerator.MoveNext();
                otherHasNext = otherEnumerator.MoveNext();
            }
            return !currHasNext && !otherHasNext;
        }

        public LinkedNode<T>? searchLoopNode() 
        {
            LinkedNode<T>? turtoise = root;
            LinkedNode<T>? hare = root;
            for (int i = 0; hare != null &&  i < 2; i++) 
            {
                if (hare != null)
                {
                    hare = hare.Next;
                }
            }
            bool loopFound = false;
            while (turtoise != null && hare != null) 
            {
                if (!loopFound  && turtoise == hare) 
                {
                    loopFound = true;
                }
                if (loopFound && turtoise == hare)
                {
                    break;
                }
                turtoise = turtoise.Next;
                hare = hare.Next;
                if (!loopFound && hare != null)
                {
                    hare = hare.Next;
                }
            }
            return loopFound ? turtoise : null;
        }
    }
}
