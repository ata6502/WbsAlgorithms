using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Common;
using WbsAlgorithms.PairPointMinMax;
using static WbsAlgorithmsTest.Utilities.DataReader;

namespace WbsAlgorithmsTest.PairPointMinMax
{
    [TestFixture]
    public class CollinearPointsTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void CountTripletsBruteForceTest(Point[] points, int expectedTripletCount, bool useForCubicYTest)
        {
            var actualTripletCountBruteForce = CollinearPoints.CountTriplesBruteForce(points);
            Assert.AreEqual(expectedTripletCount, actualTripletCountBruteForce);

            var actualTripletCountUsingSlopes = CollinearPoints.CountTriplesUsingSlopes(points);
            Assert.AreEqual(expectedTripletCount, actualTripletCountUsingSlopes);

            if (useForCubicYTest)
            {
                var actualTripletCountWithCubicY = CollinearPoints.CountTriplesWithCubicY(points);
                Assert.AreEqual(expectedTripletCount, actualTripletCountWithCubicY);
            }
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(CreatePoints(2, 8, 3, 5, 7, 2, 5, 9, 1, 8), 0, true).SetName("NoTriplets");
            yield return new TestCaseData(CreatePoints(2, 3, 3, 5, 7, 2, 5, 9, 1, 8), 1, false).SetName("1Triplet_1");
            yield return new TestCaseData(CreatePoints(-3, 4, 3, 2, 6, 1), 1, false).SetName("1Triplet_2");
            yield return new TestCaseData(CreatePoints(1, 1, 2, 8, -3, -27), 1, true).SetName("1Triplet_3");
            yield return new TestCaseData(CreatePoints(-5, -125, -2, -8, 7, 343, 3, 27, -1, -1), 2, true).SetName("2Triplets_2");
            yield return new TestCaseData(CreatePoints(-3, 4, 3, 2, 6, 1, 9, 0), 4, false).SetName("4Triplets_1");
            yield return new TestCaseData(CreatePoints(0, 1.5, 1, 2.5, 2, 3.5, 3, 4.5, 4, 5.5, 5, 6.5), 20, false).SetName("20Triplets");
        }
    }
}
