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
        public void append(string s)
        {
            flushCharBuffer();
            data.Add(s);
        }

        public void appendLine(string s)
        {
            append(s);
            data.Add($"{Environment.NewLine}");
        }
        private void flushCharBuffer()
        {
            if (charBuffer.Any())
            {
                data.Add(new string(charBuffer.ToArray()));
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
    }
}
