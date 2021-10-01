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
    }
}
