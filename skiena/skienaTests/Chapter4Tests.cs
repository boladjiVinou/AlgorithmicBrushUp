using skiena.Chapter4;
using System;
using System.Collections.Frozen;
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
            List<int> data = new List<int> { 5, 3, 1, 9 };

            var result = Chapter4.partitionList(data);

            Assert.AreEqual(1, result[0].Item1);
            Assert.AreEqual(9, result[0].Item2);
            Assert.AreEqual(3, result[1].Item1);
            Assert.AreEqual(5, result[1].Item2);
        }

        [TestMethod]
        public void whenSortedByColorElementSortedByNumber_TheNumberOrderShouldBeKeptForSameColor()
        {
            List<Tuple<int, Color>> data = new List<Tuple<int, Color>>();
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                Color color = Color.Blue;
                switch (random.Next(3))
                {
                    case 0: color = Color.Red; break;
                    case 1: color = Color.Blue; break;
                    case 2: color = Color.Yellow; break;
                }

                data.Add(new Tuple<int, Color>(random.Next(100), color));
            }

            data.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            Chapter4.sortByColorAndNumber(data);

            for (int i = 0; i < data.Count; i++) 
            {
                for (int j = i + 1; j < data.Count; j++) 
                {
                    if (data[i].Item2.Equals(data[j].Item2))
                    {
                        Assert.IsTrue(data[i].Item1 <= data[j].Item1);
                    }
                    else 
                    {
                        ColorComparer comparer = new ColorComparer();
                        Assert.IsTrue(comparer.Compare(data[i].Item2, data[j].Item2) < 0);
                    }
                }
            }
        }

        [TestMethod]
        public void whenSearchingForTheModeWeShouldReturnTheRightValue()
        {
            var random = new Random();
            List<int> data = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                data.Add(random.Next(10));
            }

            var mode = data.GroupBy(x => x)
                .Select(x => new { key = x.Key, count = x.Count() })
                .OrderByDescending(x => x.count)
                .First()
                .key;

            Assert.AreEqual(mode, Chapter4.findTheMode(data));
        }

        [TestMethod]
        public void whenThereIsATargetSumPair_ThenItShouldBeFound() 
        {
            var random = new Random();
            List<int> data1 = new List<int>();
            List<int> data2 = new List<int>();
            for (int i = 0; i < 20; i++) 
            {
                data1.Add(random.Next(1000));
                data2.Add(random.Next(1000));
            }

            Assert.IsTrue(Chapter4.findPairEqualTargetInNSquare(data1, data2, data1[random.Next(data1.Count)] + data2[random.Next(data2.Count)]));
            Assert.IsTrue(Chapter4.findPairEqualTargetInNLogN(data1, data2, data1[random.Next(data1.Count)] + data2[random.Next(data2.Count)]));
            Assert.IsTrue(Chapter4.findPairEqualTargetInN(data1, data2, data1[random.Next(data1.Count)] + data2[random.Next(data2.Count)]));
        }

        [TestMethod]
        public void whenThereIsAnUnpaidBillWeShouldFindIt() 
        {
            var random = new Random();
            List<PhoneData> phoneDatas = new List<PhoneData>();
            List<PhoneCheck> bills = new List<PhoneCheck>();
            for (int i = 0; i < 20; i++) 
            {
                phoneDatas.Add(new PhoneData());
                var phone = phoneDatas.Last();
                phone.id = i;
                phone.price = random.Next(1000);
                bills.Add(new PhoneCheck());
                var bill = bills.Last();
                bill.phoneId = phone.id;
                bill.amount = phone.price;
            }
            List<PhoneData> unpaidExpected = new List<PhoneData>();
            for (int i = 0; i < 10; i++) 
            {
                unpaidExpected.Add(new PhoneData());
                var phone = unpaidExpected.Last();
                phone.id = 50 + i;
                phone.price = random.Next(1000);
                phoneDatas.Add(phone);
            }

            var unpaidBills = Chapter4.findWhoDidntPaidBill(phoneDatas, bills);

            Assert.AreEqual(unpaidExpected.Count, unpaidBills.Count);
            Assert.IsTrue(unpaidExpected.All(x => unpaidBills.Any(y => y.id == x.id && y.price == x.price)));
        }

        [TestMethod]
        public void whenSearchingForTheNumberOfBookPerCompanyTheRightAmountShouldBeFound() 
        {
            List<BookMetadata> bookMetadata = new List<BookMetadata>();
            List<string> publishers = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 20; i++) 
            {
                bookMetadata.Add(new BookMetadata());
                var book = bookMetadata.Last();
                book.callNumber = random.Next().ToString();
                book.author = "author" + i;
                book.publisher = "publisher" + random.Next(9);
            }
            for (int i = 0; i < 9; i++) 
            {
                publishers.Add("publisher"+i);
            }


           var nbBookPerPublisher = Chapter4.findNumberOfBookPerCompany(bookMetadata,publishers);
           var expectedResult = bookMetadata.ToLookup(x => x.publisher);

            Assert.IsTrue(nbBookPerPublisher.Keys.All(x => expectedResult[x].Count() == nbBookPerPublisher[x]));
        }

        [TestMethod]
        public void whenThereIsATargetSumInUnsortedFloatSetWeShouldFindIt()
        {
            List<double> data = new List<double>();
            Random random = new Random();
            for (int i = 0; i < 20; i++) 
            {
                data.Add(i+100*random.NextDouble());
            }

            Assert.IsTrue(Chapter4.findTwoElementsEqualSumInUnsortedSet(data.ToFrozenSet(), data[random.Next(10)] + data[10 + random.Next(10)]));
        }

        [TestMethod]
        public void whenThereIsATargetSumInSortedFloatSetWeShouldFindIt()
        {
            List<double> data = new List<double>();
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                data.Add(i);
            }

            Assert.IsTrue(Chapter4.findTwoElementsEqualSumInSortedSet(data, data[random.Next(10)] + data[10 + random.Next(10)]));
        }

        [TestMethod]
        public void whenSearchingForIntersectionOfTwoUnsortedSetWeShouldFindIt() 
        {

        }

    }
}
