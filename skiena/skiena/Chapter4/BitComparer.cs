using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public class BitComparer : Comparer<int>
    {
        private int flag;
        public BitComparer(int flag) 
        {
            this.flag = flag;
        }
        public override int Compare(int x, int y)
        {
            int res1 = flag & x;
            int res2 = flag & y;
            return res1.CompareTo(res2);
        }
    }
}
