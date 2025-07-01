using skiena.datastructures.trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3
{
    public class MyPartialSumAVL : MyAvlTree<KeyValue<int,long>>
    {
        protected override MyPartialSumAVLNode createNode(KeyValue<int, long> data)
        {
            return new MyPartialSumAVLNode(null, data);
        }
        public long getPartialSum(int key) 
        {
            var foundKey = findKey(key);
            if (foundKey != null) 
            {
                return foundKey.getPartialSum();
            }
            return 0;
        }

        public void insert(int key, long val) 
        {
            add(new KeyValue<int, long>(key, val));
        }

        public void remove(int key)
        {
            root = root?.removeFirst(new KeyValue<int, long>(key, 0));
        }
        public MyPartialSumAVLNode? findKey(int key) 
        {
            var curr = (MyPartialSumAVLNode?)root;
            while (curr != null)
            {
                int compResult = curr.Value.key.CompareTo(key);
                if (compResult > 0)
                {
                    curr = (MyPartialSumAVLNode?)curr.getLeft();
                }
                else if (compResult < 0)
                {
                    curr = (MyPartialSumAVLNode?)curr.getRight();
                }
                else
                {
                    return curr;
                }
            }
            return null;
        }
        public void addTo(int key, long val) 
        {
           
            var curr = (MyPartialSumAVLNode?)root;
            var tmpData = new KeyValue<int, long>(key, val);
            Stack<MyPartialSumAVLNode> visitedNodes = new Stack<MyPartialSumAVLNode>();
            bool foundKey = false;
            while (curr != null) 
            {
                int compResult = curr.Value.CompareTo(tmpData);
                visitedNodes.Push(curr);
                if (compResult > 0)
                {
                    curr = (MyPartialSumAVLNode?)curr.getLeft();
                }
                else if (compResult < 0)
                {
                    curr = (MyPartialSumAVLNode?)curr.getRight();
                }
                else 
                {
                    curr.Value.associatedValue += val;
                    foundKey = false;
                    break;
                }
            }
            if (foundKey)
            {
                while (visitedNodes.Count > 0) 
                {
                    visitedNodes.Pop().update();
                }
            }
        }
    }
}
