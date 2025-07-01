using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    /*
     Comparison and equality only based on the key
     */
    public class KeyValue<T,S> : IEquatable<KeyValue<T, S>>, IComparable<KeyValue<T, S>>
        where T : IEquatable<T>, IComparable<T> 
        where S : IEquatable<S>, IComparable<S> , IAdditionOperators<S, S, S>
    {
        public T key { get; set; }
        public S associatedValue { get; set; }
        public KeyValue(T key, S value)
        {
            this.key = key;
            associatedValue = value;
        }

        public int CompareTo(KeyValue<T, S>? other)
        {
            if (other == null) 
            {
                return 1;
            }
            return key.CompareTo(other.key);
        }

        public bool Equals(KeyValue<T, S>? other)
        {
            if (other == null) 
            {
                return false;
            }
            return key.Equals(other.key);
        }
    }
}
