using NUnit.Framework;
using WbsAlgorithms.Arithmetics;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class ZeroSumTest
    {
        [TestCase(new[] { 1 }, 0)]
        [TestCase(new[] { 1, 1 }, 0)]
        [TestCase(new[] { 1, 1, 1 }, 0)]
        [TestCase(new[] { -1 }, 0)]
        [TestCase(new[] { -1, -1 }, 0)]
        [TestCase(new[] { -1, -1, -1 }, 0)]
        [TestCase(new[] { -1, -1, -1, -1 }, 0)]
        [TestCase(new[] { -1, 1 }, 1)]
        [TestCase(new[] { -1, -1, -1, 1, 1 }, 6)]
        [TestCase(new[] { -2, 1, 2 }, 1)]
        [TestCase(new[] { -2, 1, 2, 2 }, 2)]
        [TestCase(new[] { -2, -2, 1, 2 }, 2)]
        [TestCase(new[] { -2, -2, 0, 0, 2 }, 3)]
        [TestCase(new[] { -3, -2, -2, -2, -1, 0, 1, 1, 1, 2, 4 }, 6)]
        [TestCase(new[] { -8, -8, -5, 0, 5, 8, 8, 9, 10 }, 5)]
        [TestCase(new[] { -3, -1, 1, 3, 5, 77 }, 2)]
        [TestCase(new[] { -4, -2, 0, 0, 0, 0, 0, 0, 2 }, 16)]
        [TestCase(new[] { -9, -9, -6, -3, 1, 1, 1, 3, 4, 7 }, 1)]
        [TestCase(new[] { 0 }, 0, TestName = "CountPairs_1Zero")]
        [TestCase(new[] { -7, -6, 0, 0 }, 1, TestName = "CountPairs_2Zeros1")]
        [TestCase(new[] { 0, 0, 2, 3 }, 1, TestName = "CountPairs_2Zeros2")]
        [TestCase(new[] { -7, -6, 0, 0, 2, 3 }, 1, TestName = "CountPairs_2Zeros3")]
        [TestCase(new[] { 0, 0, 0, 1, 5, 7 }, 3, TestName = "CountPairs_3Zeros")]
        [TestCase(new[] { -5, -1, 0, 0, 0, 0 }, 6, TestName = "CountPairs_4Zeros")]
        [TestCase(new[] { 0, 0, 0, 0, 0 }, 10, TestName = "CountPairs_5Zeros")]
        [TestCase(new[] { -4, -2, 0, 0, 0, 0, 0, 0, 3 }, 15, TestName = "CountPairs_6Zeros")]
        public void CountPairsTest(int[] inputArray, int expectedCount)
        {
            var actualCount1 = ZeroSum.CountPairsQuadratic(inputArray);
            Assert.AreEqual(expectedCount, actualCount1);

            var actualCount2 = ZeroSum.CountPairsLinear(inputArray); // the inputArray has to be sorted
            Assert.AreEqual(expectedCount, actualCount2);
        }

        [TestCase(new[] { 1 }, 0)]
        [TestCase(new[] { 1, 1 }, 0)]
        [TestCase(new[] { 1, 1, 1 }, 0)]
        [TestCase(new[] { -1, -1, -1, 1, 1 }, 0)]
        [TestCase(new[] { -1 }, 0)]
        [TestCase(new[] { -1, -1 }, 0)]
        [TestCase(new[] { -1, -1, -1 }, 0)]
        [TestCase(new[] { -1, -1, 0, 0, 0, 1, 2, 2 }, 9)]
        [TestCase(new[] { -1, -1, 1, 1, 1, 1 }, 0)]
        [TestCase(new[] { -2, -1, -1, 0, 0, 1, 1, 2, 2, 3 }, 17)]
        [TestCase(new[] { -3, -1, -1, 1, 1, 1, 1, 2, 3, 3 }, 5)]
        [TestCase(new[] { -4, -2, -2, 0, 0, 0, 1, 1, 1, 1, 2, 2 }, 26)]
        [TestCase(new[] { -10, -10, -5, 0, 5, 10, 10, 15, 20 }, 8)]
        [TestCase(new[] { -3, -2, 2, 3, 5, 88 }, 1)]
        [TestCase(new[] { -10, -10, -10, 10 }, 0)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0 }, 35)]
        [TestCase(new[] { -2, -1, 0, 0, 0, 0, 0, 0, 3 }, 21)]
        [TestCase(new[] { -5, -5, -3, -2, -1, -1, -1, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 4, 5, 7 }, 100)]
        [TestCase(new[] { -3, -2, -2, 0, 0, 0, 0, 0, 0, 0 }, 35)]
        [TestCase(new[] { -14, -10, 5, 5, 7, 7 }, 2)]
        [TestCase(new[] { -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 3, 3, 3 }, 160)]
        [TestCase(new[] { -21, -20, -4, -2, -2, -2, 0, 1, 3, 5, 5, 6, 6, 6, 7, 7, 8, 8, 9, 9, 10, 12, 15, 19 }, 20)]
        public void CountTripletsTest(int[] inputArray, int expectedCount)
        {
            var actualCount1 = ZeroSum.CountTripletsCubic(inputArray);
            Assert.AreEqual(expectedCount, actualCount1);

            var actualCount2 = ZeroSum.CountTripletsQuadratic(inputArray); // the inputArray has to be sorted
            Assert.AreEqual(expectedCount, actualCount2);
        }
    }
}
