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
        public void CountTripletsBruteForceTest(Point[] points, int expectedTripletCount)
        {
            var actualTripletCount = CollinearPoints.CountTriplesBruteForce(points);
            Assert.AreEqual(expectedTripletCount, actualTripletCount);
        }

        [TestCaseSource(nameof(TestCases))]
        public void CountTriplesUsingSlopesTest(Point[] points, int expectedTripletCount)
        {
            var actualTripletCount = CollinearPoints.CountTriplesUsingSlopes(points);
            Assert.AreEqual(expectedTripletCount, actualTripletCount);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(CreatePoints(2, 8, 3, 5, 7, 2, 5, 9, 1, 8), 0).SetName("NoTriples");
            yield return new TestCaseData(CreatePoints(2, 3, 3, 5, 7, 2, 5, 9, 1, 8), 1).SetName("1Triple");
        }
    }
}
