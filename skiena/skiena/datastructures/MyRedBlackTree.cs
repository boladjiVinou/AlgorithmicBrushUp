using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyRedBlackTree<T> : MyBST<T> where T : IEquatable<T>, IComparable<T>
    {
        protected override MyRedBlackNode<T> createNode(T val)
        {
            return new MyRedBlackNode<T>(null, val);
        }
    }
}
