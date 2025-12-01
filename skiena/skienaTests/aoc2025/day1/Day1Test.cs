using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static skiena.aoc2025.day1.Day1;

namespace skienaTests.aoc2025.day1
{
    [TestClass]
    public sealed  class Day1Test
    {
        [TestMethod]
        public void whenRotatingToRightWWithHugeValueWeShouldComputeRightValue() 
        {
            Dial dial = new Dial(0, 39,0 );
            dial.rotateRight(60);
            Assert.AreEqual(20, dial.getCurrentIndex());
        }
        [TestMethod]
        public void whenRotatingToLeftWWithHugeValueWeShouldComputeRightValue()
        {
            Dial dial = new Dial(0, 39, 0);
            dial.rotateLeft(60);
            Assert.AreEqual(20, dial.getCurrentIndex());
        }
        [TestMethod]
        public void sampleTestPart1()
        {
            Dial dial = new Dial(0, 99, 50);
            int nb = dial.countZero(new List<string>() { "L68","L30","R48","L5","R60","L55","L1","L99","R14","L82"});

            Assert.AreEqual(3,nb);
        }

        [TestMethod]
        public void whenRotatingToRightWWithHugeValueWeShouldCorrectlyCountAnyEncounteredZero()
        {
            Dial dial = new Dial(0, 39, 15);
            var res = dial.countAnyZero(new List<string>() { "R56" });
            Assert.AreEqual(1,res); 
        }
        [TestMethod]
        public void whenRotatingToLeftWWithHugeValueWeShouldCorrectlyCountAnyEncounteredZero()
        {
            Dial dial = new Dial(0, 39, 0);
            int res = dial.countAnyZero(new List<string>() { "L60" });
            Assert.AreEqual(1, res);
        }


        [TestMethod]
        public void sampleTestPart2()
        {
            Dial dial = new Dial(0, 99, 50);
            int nb = dial.countAnyZero(new List<string>() { "L68", "L30", "R48", "L5", "R60", "L55", "L1", "L99", "R14", "L82" });

            Assert.AreEqual(6, nb);
        }


        [TestMethod]
        public void sampleTest2Part2()
        {
            Dial dial = new Dial(0, 99, 50);
            int nb = dial.countAnyZero(new List<string>() { "L1000" });

            Assert.AreEqual(10, nb);
        }
    }
}
