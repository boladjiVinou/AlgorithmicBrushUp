using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyChainingDictionary<T> where T : IEquatable<T>
    {
        private MyArray<MySingleLinkedList<T>> dataByKeys = new MyArray<MySingleLinkedList<T>>();
        private int capacity = 1;
        private double loadFactor = 0.75;
        private int size = 0;

        public void Add(T elem) 
        {
            int idx = elem.GetHashCode() % dataByKeys.getLength();
            if (size / (double)capacity > loadFactor) 
            {
                resize();
            }
            if (dataByKeys[idx] == null) 
            {
                dataByKeys[idx] = new MySingleLinkedList<T>();
            }
            dataByKeys[idx].add(elem);
            ++size;

        }

        private void resize() 
        {
            // dataByKeys.ensureCapacity(idx + 1);
        }

        public void Remove(T elem) 
        {
            int idx = elem.GetHashCode() % dataByKeys.getLength();
            if (dataByKeys[idx] != null) 
            {
                dataByKeys[idx].remove(elem);
            }
        }
    }
}
