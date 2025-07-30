using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public class SortedElement : IComparable<SortedElement>, IEquatable<SortedElement>
    {
        public int val { get; }
        public int listIdx { get; }
        public SortedElement(int value, int listIdx) 
        {
            this.val = value;
            this.listIdx = listIdx;
        }

        public int CompareTo(SortedElement? other)
        {
            if(other == null) return 1;
            return val.CompareTo(other.val);
        }

        public bool Equals(SortedElement? other)
        {
            if (other == null) 
            {
                return false;
            }
            if(other == this) 
            {
                return true;
            }
            return val == other.val;
        }
    }
}
