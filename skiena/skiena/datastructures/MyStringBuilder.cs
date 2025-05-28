using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures
{
    public class MyStringBuilder
    {
        private List<string> data = [];
        private List<char> charBuffer = [];
        private int size;

        public void append(string s)
        {
            flushCharBuffer();
            data.Add(s);
            size += data[data.Count-1].Length;
        }

        public void appendLine(string s)
        {
            append(s);
            append($"{Environment.NewLine}");
        }
        private void flushCharBuffer()
        {
            if (charBuffer.Any())
            {
                data.Add(new string(charBuffer.ToArray()));
                size += data[data.Count - 1].Length;
                charBuffer.Clear();
            }
        }

        public void append(char c)
        {
            charBuffer.Add(c);
        }

        public string toString() 
        {
            flushCharBuffer();
            return string.Join("", data);
        }

        public void clear() 
        {
            data.Clear();
            charBuffer.Clear();
        }

        public int getSize() 
        {
            return size;
        }
    }
}
