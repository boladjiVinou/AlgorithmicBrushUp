using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyArray<T> where T : IEquatable<T>
    {
        private int capacity = 1;
        private int size = 0;
        private T[] data;
        public MyArray()
        {
            data = new T[capacity];
        }
        public void Add(T element)
        {
            if (size == capacity)
            {
                ensureCapacity(size+1);
            }
            data[size++] = element;
        }
        public void Remove(T element)
        {

            if (element == null)
            {
                data = data.Take(size).Where(x => x != null).ToArray();
            }
            else
            {
                data = data.Take(size).Where(x => !element.Equals(x)).ToArray();
            }
            size = data.Length;
            capacity = Math.Max(1, size);
        }

        public int getLength()
        {
            return size;
        }
        public void ensureCapacity(int wantedSize)
        {
            if (wantedSize >= size && wantedSize >= capacity)
            {
                capacity = Math.Max(2 * capacity, wantedSize);
                T[] newData = new T[capacity];
                Array.Copy(data, newData, size);
                data = newData;
            }
        }

        public int getCapacity()
        {
            return capacity;
        }
        public T this[int i]
        {
            get
            {
                if (i >= size) 
                {
                    throw new IndexOutOfRangeException();
                }
                return data[i];
            }

            set 
            {

                if (i >= size)
                {
                    throw new IndexOutOfRangeException();
                }
                data[i] = value;
            }
        }
    }
}
