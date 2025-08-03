using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public class SignComparer : Comparer<int> 
    {
        public override int Compare(int x, int y)
        {
            return Math.Sign(x).CompareTo(Math.Sign(y));
        }
    }
}
