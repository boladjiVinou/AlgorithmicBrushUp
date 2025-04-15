using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyChainingDictionary<T> where T : IEquatable<T>, IEnumerable<T>
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
            dataByKeys[idx].add(elem);
            ++size;
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
                dataByKeys[idx].remove(elem);
            }
            --size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var entry in dataByKeys) 
            {
                foreach (var item in entry) 
                {
                    yield return item;
                }
            }
        }
    }
}
