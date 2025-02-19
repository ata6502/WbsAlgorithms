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
            Assert.That(actualTripletCountBruteForce, Is.EqualTo(expectedTripletCount));

            var actualTripletCountUsingSlopes = CollinearPoints.CountTriplesUsingSlopes(points);
            Assert.That(actualTripletCountUsingSlopes, Is.EqualTo(expectedTripletCount));

            if (useForCubicYTest)
            {
                var actualTripletCountWithCubicY = CollinearPoints.CountTriplesWithCubicY(points);
                Assert.That(actualTripletCountWithCubicY, Is.EqualTo(expectedTripletCount));
            }
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(CreatePoints(2, 8, 3, 5, 7, 2, 5, 9, 1, 8), 0, true).SetName("NoTriplets");
            yield return new TestCaseData(CreatePoints(2, 3, 3, 5, 7, 2, 5, 9, 1, 8), 1, false).SetName("1Triplet_1");
            yield return new TestCaseData(CreatePoints(-3, 4, 3, 2, 6, 1), 1, false).SetName("1Triplet_2");
            yield return new TestCaseData(CreatePoints(1, 1, 2, 8, -3, -27), 1, true).SetName("1Triplet_3");
            yield return new TestCaseData(CreatePoints(2, 2, 2, 5, 2, 7, 4, 2, 4, 6, 6, 2), 2, false).SetName("2Triplets_1");
            yield return new TestCaseData(CreatePoints(-5, -125, -2, -8, 7, 343, 3, 27, -1, -1), 2, true).SetName("2Triplets_2");
            yield return new TestCaseData(CreatePoints(1, 1, 2, 2, 3, 3, 4, 2, 5, 3, 6, 4), 2, false).SetName("2Triplets_3");
            yield return new TestCaseData(CreatePoints(1, 2, 6, 2, 3, 4, 7, 4, 5, 6, 8, 6), 2, false).SetName("2Triplets_4");
            yield return new TestCaseData(CreatePoints(-3, 4, 3, 2, 6, 1, 9, 0), 4, false).SetName("4Triplets_1");
            yield return new TestCaseData(CreatePoints(1, 5, 3, 3, 5, 3, 7, 1, 3, 7, 5, 5, 6, 4, 4, 9, 2, 11, 7, 7, 8, 6, 10, 4, 12, 2), 8, false).SetName("8Triplets");
            yield return new TestCaseData(CreatePoints(0, 1.5, 1, 2.5, 2, 3.5, 3, 4.5, 4, 5.5, 5, 6.5), 20, false).SetName("20Triplets");
            yield return new TestCaseData(CreatePoints(3, 3, 2, 1, 4, 2, 2, 3, 3, 4, 1, 3, 4, 4, 3, 2, 1, 2, 4, 1, 2, 4, 4, 3, 2, 2, 3, 1, 1, 1, 1, 4), 44, false).SetName("UnsortedLattice1"); // 4x4 square lattice
            yield return new TestCaseData(CreatePoints(2, 1, 5, 3, 4, 4, 3, 2, 7, 4, 4, 1, 2, 2, 4, 3, 5, 2, 3, 1, 1, 1, 5, 4, 6, 3, 4, 2, 3, 3, 6, 4), 44, false).SetName("UnsortedLattice2"); // 4x4 skewed lattice
            yield return new TestCaseData(CreatePoints(2, 2, 3, 3, 1, 3, 3, 1, 1, 1), 2, false).SetName("Cross1"); // X-cross
            yield return new TestCaseData(CreatePoints(2, 1, 1, 2, 2, 3, 3, 2, 2, 2), 2, false).SetName("Cross2"); // +-cross
            yield return new TestCaseData(CreatePoints(@"Data\Points50.txt"), 0, false).SetName("Points50");
            yield return new TestCaseData(CreatePoints(@"Data\Points500.txt"), 8, false).SetName("Points500");
        }
    }
}
