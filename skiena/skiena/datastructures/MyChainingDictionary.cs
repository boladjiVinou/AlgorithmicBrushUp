using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyChainingDictionary<T> : IEnumerable<T> where T : IEquatable<T>
    {
        private MySingleLinkedList<T>[] dataByKeys = Array.Empty<MySingleLinkedList<T>>();
        private double loadFactor = 0.75;
        private int size = 0;

        public MyChainingDictionary() 
        {
        }

        public MyChainingDictionary(int capacity) 
        {
            dataByKeys = new MySingleLinkedList<T>[capacity];
        }
        public void Add(T elem) 
        {
            resize();
            int idx = elem.GetHashCode() % dataByKeys.Length;
            if (dataByKeys[idx] == null)
            {
                dataByKeys[idx] = new MySingleLinkedList<T>();
            }
            if (dataByKeys[idx].AddIfNotEquals(elem)) 
            {
                ++size;
            }
        }

        private void resize() 
        {
            if (dataByKeys.Length > 0 && ((size + 1) / (double)dataByKeys.Length < loadFactor))
            {
                return;
            }

            var newKeysHolder = new MySingleLinkedList<T>[2*(size+1)];

            var iterator = GetEnumerator();
            while (iterator.MoveNext()) 
            {
                int idx = iterator.Current.GetHashCode() % newKeysHolder.Length;
                if (newKeysHolder[idx] == null)
                {
                    newKeysHolder[idx] = new MySingleLinkedList<T>();
                }
                newKeysHolder[idx].add(iterator.Current);
            }

            dataByKeys = newKeysHolder;
        }


        public void Remove(T elem) 
        {
            int idx = elem.GetHashCode() % dataByKeys.Length;
            if (dataByKeys[idx] != null) 
            {
                size -= dataByKeys[idx].remove(elem);
            }
        }

        public bool Contains(T elem) 
        {
            int idx = elem.GetHashCode() % dataByKeys.Length;
            if (dataByKeys[idx] != null) 
            {
                return dataByKeys[idx].Any(x => x.Equals(elem));
            }
            return false;
        }

        public int getSize() 
        {
            return size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var entry in dataByKeys) 
            {
                if (entry == null) 
                {
                    continue;
                }
                foreach (var item in entry) 
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
