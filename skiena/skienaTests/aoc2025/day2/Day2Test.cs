using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static skiena.aoc2025.day2.Day2;

namespace skienaTests.aoc2025.day2
{
    [TestClass]
    public sealed class Day2Test
    {
        [TestMethod]
        public void sampleTestPart1() 
        {
            List<MyRange> list = [];
            list.Add(new MyRange(11,22));
            list.Add(new MyRange(95, 115));
            list.Add(new MyRange(998, 1012));
            list.Add(new MyRange(1188511880 , 1188511890));
            list.Add(new MyRange(222220 , 222224));
            list.Add(new MyRange(1698522 , 1698528));
            list.Add(new MyRange(446443 , 446449));
            list.Add(new MyRange(38593856 , 38593862));
            list.Add(new MyRange(565653 , 565659));
            list.Add(new MyRange(824824821 ,824824827));
            list.Add(new MyRange(2121212118 , 2121212124));

           Assert.AreEqual((ulong)1227775554, addInvalidCodes(list));

        }
        [TestMethod]
        public void sampleTestPart2()
        {
            List<MyRange> list = [];
            list.Add(new MyRange(11, 22));
            list.Add(new MyRange(95, 115));
            list.Add(new MyRange(998, 1012));
            list.Add(new MyRange(1188511880, 1188511890));
            list.Add(new MyRange(222220, 222224));
            list.Add(new MyRange(1698522, 1698528));
            list.Add(new MyRange(446443, 446449));
            list.Add(new MyRange(38593856, 38593862));
            list.Add(new MyRange(565653, 565659));
            list.Add(new MyRange(824824821, 824824827));
            list.Add(new MyRange(2121212118, 2121212124));

            Assert.AreEqual((ulong)4174379265, addInvalidCodesV2(list)); 
        }
        [TestMethod]
        public void invalidCodeDetectionTest() 
        {
            List<string> codes = new List<string>() {"11","22","99","111","999","1010", "1188511885", "222222",
                "446446", "38593859", "565656","824824824", "2121212121" };
            foreach (var code in codes) 
            {
                Assert.AreEqual(true, isInvalidV3(code));
            }
        }
    }
}
