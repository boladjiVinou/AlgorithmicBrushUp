using skiena;
using skiena.datastructures;
namespace skienaTests
{
   
    [TestClass]
    public sealed class Chapter3Tests
    {
        [TestMethod]
        public void givenABalancedInputTheExercice1ShoulDetectValidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector("((())())()");
            Assert.IsTrue(result.Item1);
            Assert.IsTrue(result.Item2 < 0);
        }
        [TestMethod]
        public void givenAnUnbalancedInputAtStartTheExercice1ShoulDetectInvalidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector(")()(");
            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == 0);
        }
        [TestMethod]
        public void givenAnUnbalancedInputAtEndTheExercice1ShoulDetectInvalidity()
        {
            Tuple<bool, int> result = Chapter3.parenthesisDetector("())");
            Assert.IsFalse(result.Item1);
            Assert.IsTrue(result.Item2 == 2);
        }
    }
}
