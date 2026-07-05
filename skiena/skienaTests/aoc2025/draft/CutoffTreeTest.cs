using skiena.aoc2025.draft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.aoc2025.draft
{
    [TestClass]
    public class CutoffTreeTest
    {
        [TestMethod]
        public void test() 
        {
            IList<IList<int>> forest = [
                new List<int>() { 4,2,3}, 
                new List<int>() { 0,0,1 },
                new List<int>() {7,6,5 } ];

            var res = CutOffTreeProblem.CutOffTree(forest);
        }
    }
}
