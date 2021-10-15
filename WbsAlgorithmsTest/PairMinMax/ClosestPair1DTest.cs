using NUnit.Framework;
using WbsAlgorithms.Common;
using WbsAlgorithms.PairMinMax;

namespace WbsAlgorithmsTest.PairMinMax
{
    [TestFixture]
    public class ClosestPair1DTest
    {
        [TestCase(new double[] { 1, 1 }, 1, 1)]
        [TestCase(new double[] { 1, 5, 7 }, 5, 7)]
        [TestCase(new double[] { -5.2, 9.4, 20, -10, 21.1, 40, 50, -20 }, 20, 21.1)]
        [TestCase(new double[] { -4, -3, 0, 10, 20 }, -4, -3)]
        [TestCase(new double[] { -10, -3, 0, 2, 4, 20 }, 0, 2)]
        public void FindPairTest(double[] inputArray, double expectedX, double expectedY)
        {
            var pairExpected = new Point(expectedX, expectedY);

            var pairBruteForce = ClosestPair1D.FindPairBruteForce(inputArray);
            Assert.AreEqual(pairExpected, pairBruteForce);

            var pairLinearithmic = ClosestPair1D.FindPairLinearithmic(inputArray);
            Assert.AreEqual(pairExpected, pairLinearithmic);
        }
    }
}
