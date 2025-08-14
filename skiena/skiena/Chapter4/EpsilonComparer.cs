using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    class EpsilonComparer : IComparer<double>
    {
        private readonly double epsilon;
        public EpsilonComparer(double epsilon) => this.epsilon = epsilon;

        public int Compare(double x, double y)
        {
            if (Math.Abs(x - y) < epsilon) return 0;
            return x < y ? -1 : 1;
        }
    }
}
