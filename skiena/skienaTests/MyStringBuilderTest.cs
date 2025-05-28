using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public class MyStringBuilderTest
    {
        [TestMethod]
        public void whenAppendingTwoStringsWithStringBuilder_thenTheBuiltStringShouldContainThem()
        {
            var sb = new MyStringBuilder();
            sb.append("abcd");
            sb.append("efgh");

            var result = sb.toString();
            var expected = "abcdefgh";

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void whenAppendingTwoStringLinesWithStringBuilder_thenTheBuiltStringShouldContainThem()
        {
            var sb = new MyStringBuilder();
            sb.appendLine("abcd");
            sb.append("efgh");

            var result = sb.toString();
            var expected = $"abcd{Environment.NewLine}efgh";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void whenAppendingTwoStringWithStringBuilder_thenTheSizeShouldBeUpdated()
        {
            var sb = new MyStringBuilder();
            sb.append("abcd");
            sb.append("efgh");

            Assert.AreEqual(8, sb.getSize());
        }
    }
}
