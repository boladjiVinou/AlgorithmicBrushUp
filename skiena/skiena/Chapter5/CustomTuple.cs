using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter5
{
    public class CustomTuple<T> : IEquatable<CustomTuple<T>> where T:IComparable<T>
    {
        public T item1 { get; set; }
        public T item2 { get; set; }

        public CustomTuple(T item1, T item2) 
        {
            this.item1 = item1;
            this.item2 = item2;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is CustomTuple<T>)
            {
                return this.Equals((CustomTuple<T>)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            T min, max;
            if (item1.CompareTo(item2) < 0)
            {
                min = item1;
                max = item2;
            }
            else 
            {
                min = item2;
                max = item1;
            }
             return HashCode.Combine(min,max);
        }

        public bool Equals(CustomTuple<T>? other)
        {
            if (other == null) return false;
            return ((item1?.Equals(other.item1) ?? false)
                        && (item2?.Equals(other.item2) ?? false))
                || ((item2?.Equals(other.item1) ?? false)
                        && (item1?.Equals(other.item2) ?? false));
        }

    }
}
