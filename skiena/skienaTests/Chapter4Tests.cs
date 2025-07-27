using skiena.Chapter4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skienaTests
{
    [TestClass]
    public sealed class Chapter4Tests
    {
        [TestMethod]
        public void whenTheGrinchIsAskedToCreateTeamItShouldCreateImbalancedTeam()
        {
            List<int> players = [];
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                players.Add(random.Next(100));
            }

            Chapter4.exercice_4_1(players);
            List<int> teamA = players.Take(10).ToList();
            List<int> teamB = players.Skip(10).ToList();

            Assert.IsTrue(teamA.All(x => teamB.All(y => y > x)));
        }
        [TestMethod]
        public void whenSearchingPairMaximizingDifferenceInUnsortedList_WeShouldFindTheRightOne() 
        {
            List<int> data = [];
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                data.Add(random.Next(100));
            }

            var result = Chapter4.findMaxPairFromUnsortedArray(data);

            Assert.AreEqual(data.Min(), Math.Min(result.Item1, result.Item2));
            Assert.AreEqual(data.Max(), Math.Max(result.Item1, result.Item2));
        }

        [TestMethod]
        public void whenSearchingPairMaximizingDifferenceInSortedList_WeShouldFindTheRightOne()
        {
            List<int> data = [];
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                data.Add(random.Next(100));
            }
            data.Sort();

            var result = Chapter4.findMaxPairFromSortedArray(data);

            Assert.AreEqual(data.First(), Math.Min(result.Item1, result.Item2));
            Assert.AreEqual(data.Last(), Math.Max(result.Item1, result.Item2));
        }

        [TestMethod]
        public void whenSearchingPairMinimizingDifferenceInSortedList_WeShouldFindTheRightOne()
        {
            List<int> data = new List<int>{ 1, 9,12,15,50,99,100 };

            var result = Chapter4.findMinPairFromSortedArray(data);

            Assert.AreEqual(99, Math.Min(result.Item1, result.Item2));
            Assert.AreEqual(100, Math.Max(result.Item1, result.Item2));
        }

        [TestMethod]
        public void whenSearchingForPairMinimizingTheMaximumSum_WeShouldFindIt() 
        {
            List<int> data = new List<int> { 1, 3, 5, 9 };

            var result = Chapter4.partitionList(data);

            Assert.AreEqual(1, result[0].Item1);
            Assert.AreEqual(9, result[0].Item2);
            Assert.AreEqual(3, result[1].Item1);
            Assert.AreEqual(5, result[1].Item2);
        }

    }
}
