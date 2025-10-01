using skiena.datastructures.lists;
using System;
using System.Collections;

namespace skiena.datastructures.trees
{
    public class MyBT<T> : IEnumerable<T> where T : IEquatable<T>
    {
        protected MyBTNode<T>? root { get; set; }

        public virtual void add(T val)
        {
            if (root == null)
            {
                root = createNode(val);
            }
            else
            {
                root = root.insert(val);
            }
        }

        protected virtual MyBTNode<T> createNode(T val)
        {
            return new MyBTNode<T>(null, val);
        }


        public virtual void remove(T val)
        {
            if (root != null)
            {
                root = root.removeFirst(val);
            }
        }



        public IEnumerable<T> inOrderIteration()
        {
            if (root == null)
            {
                yield break;
            }
            foreach (var val in inOrderIterationFrom(root))
            {
                yield return val;
            }
        }

        public static IEnumerable<T> inOrderIterationFrom(MyBTNode<T>? start)
        {
            Stack<MyBTNode<T>> visitStack = new Stack<MyBTNode<T>>();
            HashSet<MyBTNode<T>?> visited = new HashSet<MyBTNode<T>?>();
            visitStack.Push(start);
            while (visitStack.Count > 0)
            {
                var node = visitStack.Peek();
                while (node != null && node.getLeft() != null && !visited.Contains(node.getLeft()))
                {
                    node = node.getLeft();
                    if (node != null)
                    {
                        visitStack.Push(node);
                    }
                }

                var tmp = visitStack.Pop();
                yield return tmp.Value;
                visited.Add(tmp);


                var tmpRightChild = tmp.getRight();
                if (tmpRightChild != null && !visited.Contains(tmpRightChild))
                {
                    visitStack.Push(tmpRightChild);
                }
            }
        }

        public IEnumerable<T> postOrderIteration()
        {
            if (root == null)
            {
                yield break;
            }
            Stack<MyBTNode<T>> visitStack = new Stack<MyBTNode<T>>();
            HashSet<MyBTNode<T>?> visited = new HashSet<MyBTNode<T>?>();
            visitStack.Push(root);
            while (visitStack.Count > 0)
            {
                var node = visitStack.Peek();
                while (node != null && node.getLeft() != null && !visited.Contains(node.getLeft()))
                {
                    node = node.getLeft();
                    if (node != null)
                    {
                        visitStack.Push(node);
                    }
                }

                var tmpRightChild = visitStack.Peek().getRight();
                if (tmpRightChild != null && !visited.Contains(tmpRightChild))
                {
                    visitStack.Push(tmpRightChild);
                }

                var tmp = visitStack.Peek();
                var tmpLeft = tmp.getLeft();
                var tmpRight = tmp.getRight();
                if (!visited.Contains(tmp) &&
                    (tmpLeft == null || visited.Contains(tmpLeft)) &&
                    (tmpRight == null || visited.Contains(tmpRight)))
                {
                    yield return tmp.Value;
                    visited.Add(tmp);
                    visitStack.Pop();
                }
            }
        }
        public IEnumerable<T> preOrderIteration()
        {
            if (root == null)
            {
                yield break;
            }
            Stack<MyBTNode<T>?> visitStack = new Stack<MyBTNode<T>?>();
            visitStack.Push(root);
            while (visitStack.Count > 0)
            {
                var curr = visitStack.Pop();
                if (curr != null)
                {
                    yield return curr.Value;


                    if (curr.hasRightChild())
                    {
                        visitStack.Push(curr.getRight());
                    }

                    if (curr.hasLeftChild())
                    {
                        visitStack.Push(curr.getLeft());
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return inOrderIteration().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public bool contains(T data)
        {
            return root?.contains(data) ?? false;
        }

       
        public MySingleLinkedList<T> convertAsLinkedList()
        {
            MySingleLinkedList<T> linkedList = new MySingleLinkedList<T>();
            foreach (var n in inOrderIterationFrom(root))
            {
                linkedList.add(n);
            }
            return linkedList;
        }
    }
}
