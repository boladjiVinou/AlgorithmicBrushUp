using System.Collections;

namespace skiena.datastructures.lists
{
    public class MyDoubleLinkedList<T> : IEnumerable<T> where T : IEquatable<T>
    {
        public LinkedNode<T> root { get; set; }

        public int count()
        {
            LinkedNode<T> curr = root;
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
            if (isEmpty())
            {
                return;
            }
            LinkedNode<T> curr = root;
            LinkedNode<T> prev = null;
            while (curr != null)
            {
                var tmp = curr.Next;
                curr.Next = prev;
                if (prev != null) 
                {
                    prev.Previous = curr;
                }
                prev = curr;
                curr = tmp;
            }
            root = prev;
            root.Previous = null;
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
                LinkedNode<T> curr = root;
                LinkedNode<T> prev = null;
                while (curr != null)
                {
                    prev = curr;
                    curr = curr.Next;
                }
                prev.Next = tmp;
                tmp.Previous = prev;
            }
        }

        public void remove(T value)
        {
            if (value == null)
            {
                return;
            }
            var curr = root;
            LinkedNode<T> prev = null;
            while (curr != null)
            {
                if (!curr.Value.Equals(value))
                {
                    prev = curr;
                    curr = curr.Next;
                    continue;
                }
                curr = detachNode(curr);
                if (prev == null) 
                {
                    root = curr;
                }
            }
        }

        /*
         Break the link of the passed node with its previous and next. and return next
         */
        private static LinkedNode<T> detachNode(LinkedNode<T> node)
        {
            if (node == null) 
            {
                return null;
            }

            var prev = node.Previous;
            if (prev != null) 
            {
                prev.Next = node.Next;
            }
            node.Previous = null;

            var next = node.Next;
            if (next != null) 
            {
                next.Previous = prev;
            }
            node.Next = null;
            return next;
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

        public bool Equals(MyDoubleLinkedList<T>? other)
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
                if (myEnumerator.Current != null && !myEnumerator.Current.Equals(otherEnumerator.Current))
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
    }
}
