using skiena.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public sealed class ArrayTests
    {
        [TestMethod]
        public void givenAnArrayIfAnElementIsAddedItShouldIncreaseTheCount() 
        {
            MyArray<int> data = new MyArray<int>();
            data.Add(1);
            data.Add(2);
            
            data.Add(3);

            Assert.AreEqual(1, data[0]);
            Assert.AreEqual(3, data.getLength());
        }

        [TestMethod]
        public void givenAFullArrayIfAnElementIsAddedItShouldDoubleCapacity()
        {
            MyArray<int> data = new MyArray<int>();
            data.Add(1);
            data.Add(2);

            data.Add(3);

            Assert.AreEqual(4, data.getCapacity());
        }

        [TestMethod]
       public void givenAnArrayIfAnElementIsRemovedItShouldReduceTheCount()
        {
            MyArray<int> data = new MyArray<int>();
            data.Add(1);
            data.Add(2);
            data.Add(1);

            data.Remove(1);

            Assert.AreEqual(1, data.getLength());
            Assert.AreEqual(2, data[0]);
        }
    }
}
