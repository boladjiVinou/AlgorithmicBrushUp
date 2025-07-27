using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public enum Color
    {
        Red = 0,
        Blue=1,
        Yellow=2
    }
    public class ColorComparer : IComparer<Color>
    {
        public int Compare(Color x, Color y)
        {
            return ((int)x).CompareTo((int)y);
        }
    }
}
