using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.trees
{
     public class MySegmentTree<T> where T : IEquatable<T>, IComparable<T>
    {
        private MySegmentNode<T>? root;

		public MySegmentTree(Func<T,T,T> joinFunction, T[] data) 
        {
            root = MySegmentNode<T>.buildSubTree(joinFunction,data, 0, data.Length-1);
		}

        public T? getResultBetween(int start, int end) 
        {
            if (root == null) 
            {
                return default;
            }
            return root.getResultBetween(start, end);
        }
        public void updateAt(T value, int idx)
        {
            if (root != null) 
            {
                root.updateAt(value, idx);
            }
        }
    }
}
