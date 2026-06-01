using skiena.datastructures.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests.dataStructures
{
    [TestClass]
    public class DirectedGraphTest : MyGraphTest
    {
        public override void setup()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void whenANodeIsConnected_TheInDegreeShouldBeUpdated()
        {
            var tmpGraph = createGraph();
            tmpGraph.connect(0, 1);
            tmpGraph.connect(2, 1);

            Assert.AreEqual(2, tmpGraph.getInDegree(1));
            Assert.AreEqual(1, tmpGraph.getInDegree(2));
            Assert.AreEqual(1, tmpGraph.getInDegree(0));
        }

        protected override Graph<int> createGraph()
        {
            throw new NotImplementedException();
        }
    }
}
