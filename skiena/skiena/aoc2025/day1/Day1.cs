using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static skiena.aoc2025.day1.Day1;

namespace skiena.aoc2025.day1
{
    public class Day1
    {
        public class Dial 
        {
            private int currPos;
            private int min;
            private int max;
            private readonly int range;
            public Dial(int min, int max, int start) 
            {
                this.min = min;
                this.max = max;
                this.currPos = start;
                range = max - min + 1;
            }
            public void rotateLeft(int nb) 
            {
                int initialPos = currPos;
                if (nb < range)
                {
                    currPos -= nb;
                    if (currPos < min)
                    {
                        currPos += max +1;
                    }
                }
                else 
                {
                     rotateLeft(nb % range);
                }
            }
            public void rotateRight(int nb)
            {
                int initialPos = currPos;
                if (nb < range)
                {
                    currPos += nb;
                    if (currPos > max)
                    {
                        currPos -= max + 1;
                    }
                }
                else
                {
                     rotateRight(nb % range);
                }
            }

            private int getNbIgnoredZeroAfterRighttRotation(int originalNb, int initialPos) 
            {
                int val = 0;
                if (currPos > 0 && initialPos > currPos)
                {
                    ++val;
                }
                val += (originalNb / range);
                val += getCurrentIndex() == 0 ? 1 : 0;
                return val;
            }
            private int getNbIgnoredZeroAfterLeftRotation(int originalNb, int initialPos)
            {
                int val = 0;
                if (initialPos > 0 && initialPos < currPos)
                {
                    ++val;
                }
                val += (originalNb / range);
                val += getCurrentIndex() == 0 ? 1 : 0;
                return val;
            }

            public int getCurrentIndex() 
            {
                return currPos;
            }

            public int countZero(List<string> instructions) 
            {
                int nbPointingAtZero = 0;
                foreach(var line in instructions.Select(x=>x.Trim())) 
                {
                    if (!int.TryParse(line.Substring(1), out int nb))
                    {
                        continue;
                    }
                    if (line[0] == 'R')
                    {
                        rotateRight(nb);
                    }
                    else if (line[0] == 'L')
                    {
                        rotateLeft(nb);
                    }
                    if (currPos == 0)
                    {
                        ++nbPointingAtZero;
                    }
                }
                return nbPointingAtZero;
            }
            public int countAnyZero(List<string> instructions) 
            {
                int nbPointingAtZero = 0;
                foreach (var line in instructions.Select(x => x.Trim()))
                {
                    if (!int.TryParse(line.Substring(1), out int nb) || nb ==0)
                    {
                        continue;
                    }
                    int initialPos = currPos;
                    if (line[0] == 'R')
                    {
                        rotateRight(nb);
                        nbPointingAtZero +=   getNbIgnoredZeroAfterRighttRotation(nb, initialPos);
                    }
                    else if (line[0] == 'L')
                    {
                        rotateLeft(nb);
                        nbPointingAtZero +=  getNbIgnoredZeroAfterLeftRotation(nb, initialPos);
                    }
                }
                return nbPointingAtZero;
            }
        }

        public static void part1() 
        {
            Dial dial = new Dial(0,99,50);

            Console.WriteLine( dial.countZero( File.ReadAllLines(@"D:\AlgorithmLearning\Repository\AlgorithmicBrushUp\skiena\skiena\aoc2025\day1\input1.txt").ToList()));
            Console.ReadLine();
        }
        public static void part2() 
        {
            Dial dial = new Dial(0, 99, 50);

            Console.WriteLine(dial.countAnyZero(File.ReadAllLines(@"D:\AlgorithmLearning\Repository\AlgorithmicBrushUp\skiena\skiena\aoc2025\day1\input1.txt").ToList()));
            Console.ReadLine();
        }
    }
}
