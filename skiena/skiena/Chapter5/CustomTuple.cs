using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter5
{
    public class CustomTuple<T> : IEquatable<CustomTuple<T>>
    {
        public T item1 { get; set; }
        public T item2 { get; set; }

        public CustomTuple(T item1, T item2) 
        {
            this.item1 = item1;
            this.item2 = item2;
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
