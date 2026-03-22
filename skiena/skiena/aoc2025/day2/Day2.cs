using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static skiena.aoc2025.day1.Day1;

namespace skiena.aoc2025.day2
{
    public class Day2
    {
        public class MyRange
        {
            public ulong start { get; set; }
            public ulong end { get; set; }
            public bool isValid { get; set; }
            public MyRange(string code)
            {
                var parts = code.Split('-');
                isValid = parts[0][0] != '0' && parts[1][0] != '0';
                if (isValid)
                {
                    start = ulong.Parse(parts[0]);
                    end = ulong.Parse(parts[1]);
                }
            }
            public MyRange(ulong start, ulong end)
            {
                isValid = true;
                this.start = start;
                this.end = end;
            }
        }

        public static void part1()
        {
            var ranges = File.ReadAllText(@"D:\AlgorithmLearning\Repository\AlgorithmicBrushUp\skiena\skiena\aoc2025\day2\input.txt")
                .Split(',')
                .Select(x => new MyRange(x))
                .Where(x => x.isValid)
                .ToList();
            Console.WriteLine(addInvalidCodes(ranges));

            Console.ReadLine();
        }

        public static void part2()
        {
            var ranges = File.ReadAllText(@"D:\AlgorithmLearning\Repository\AlgorithmicBrushUp\skiena\skiena\aoc2025\day2\input.txt")
                .Split(',')
                .Select(x => new MyRange(x))
                .Where(x => x.isValid)
                .ToList();
            Console.WriteLine(addInvalidCodesV2(ranges));

            Console.ReadLine();
        }

        public static ulong addInvalidCodes(List<MyRange> ranges)
        {
            List<ulong> invalids = new List<ulong>();
            foreach (var range in ranges)
            {
                for (ulong i = range.start; i <= range.end; i++)
                {
                    if (isInvalid(i.ToString()))
                    {
                        invalids.Add(i);
                    }
                }
            }
            ulong sum = 0;
            for (int i = 0; i < invalids.Count; i++)
            {
                sum += invalids[i];
            }
            return sum;
        }

        static bool isInvalid(string code)
        {
            if (code.Length % 2 != 0)
            {
                return false;
            }
            int mid = code.Length / 2;
            for (int i = 0; i < mid; i++)
            {
                if (code[i] != code[mid + i])
                {
                    return false;
                }
            }
            return true;
        }


        public static ulong addInvalidCodesV2(List<MyRange> ranges)
        {
            List<ulong> invalids = new List<ulong>();
            Regex reg = new Regex(@"^(\d{1,})\1{1,}$");
            foreach (var range in ranges)
            {
                for (ulong i = range.start; i <= range.end; i++)
                {
                    string code = i.ToString();
                  
                    if (reg.Match(code).Success)
                    {
                        invalids.Add(i);
                    }
                }
            }
            ulong sum = 0;
            for (int i = 0; i < invalids.Count; i++)
            {
                sum += invalids[i];
            }
            return sum;
        }
        public static bool isInvalidV3(string code) 
        {
            StringBuilder buffer = new StringBuilder();
            int readIdx = 0;
            int nbReset = 1;
            int lastInserted = -1;
            for (int i = 0; i < code.Length; i++) 
            {
                if (buffer.Length == 0 || buffer[readIdx] != code[i])
                {
                    if (lastInserted >=0 && (i - lastInserted) > 1)
                    {
                        buffer.Append(code.Substring(lastInserted + 1, i - lastInserted-1));
                    }
                    buffer.Append(code[i]);
                    lastInserted = i;
                    readIdx = 0;
                    nbReset = 1;
                }
                else if (buffer[readIdx] == code[i])
                {
                    ++readIdx;
                    if (readIdx >= buffer.Length)
                    {
                        readIdx = 0;
                        ++nbReset;
                    }
                }
            }
            return nbReset > 1 && (nbReset * buffer.Length) == code.Length;
        }
        public static bool isInvalidV2(string code, int start, int end, int depth)
        {
            if ((end-start) <2 || end <= start)
            {
                return false;
            }
            int mid = start + (end -start)/ 2;
            for (int i = start,  j = mid ; i < mid && j <end; i++, j++)
            {
                if (depth < 2 && code[i] != code[j])
                {
                    return isInvalidV2(code, start, i, depth) || isInvalidV2(code, j, end, depth);
                }
            }
            ++depth;
            return depth > 1 || isInvalidV2(code,start,mid, depth);
        }
    }
}
