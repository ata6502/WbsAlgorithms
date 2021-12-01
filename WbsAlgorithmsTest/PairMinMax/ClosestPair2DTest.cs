using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.Common;
using WbsAlgorithms.PairMinMax;
using static WbsAlgorithmsTest.Utilities.DataReader;

namespace WbsAlgorithmsTest.PairMinMax
{
    [TestFixture]
    public class ClosestPair2DTest
    {
        [TestCaseSource(nameof(ComputeClosestPair2DTestCases))]
        public void ComputeClosestPair2DTest(Point[] points, Point[] expectedClosestPair, double expectedDistance)
        {
            const double Tolerance = 0.0000000001;

            var resultBruteForce = ClosestPair2D.ComputeBruteForce(points);
            AssertPair(expectedClosestPair, resultBruteForce.ClosestPair);
            Assert.AreEqual(expectedDistance, resultBruteForce.Distance, Tolerance);

            var resultRecursive = ClosestPair2D.ComputeRecursive(points);
            AssertPair(expectedClosestPair, resultRecursive.ClosestPair);
            Assert.AreEqual(expectedDistance, resultRecursive.Distance, Tolerance);
        }

        private void AssertPair(Point[] expectedPair, Point[] actualPair)
        {
            Assert.AreEqual(2, actualPair.Length);
            Assert.IsTrue(
                (expectedPair[0] == actualPair[0] && expectedPair[1] == actualPair[1]) ||
                (expectedPair[1] == actualPair[0] && expectedPair[1] == actualPair[0]));
        }

        private static IEnumerable<TestCaseData> ComputeClosestPair2DTestCases()
        {
            yield return new TestCaseData(CreatePoints(1, 1, 4, 2), CreatePoints(1, 1, 4, 2), 3.1622776602).SetName("TwoPoints");
            yield return new TestCaseData(CreatePoints(3, 5, 1, 4, -1, 2), CreatePoints(3, 5, 1, 4), 2.2360679775).SetName("ThreePoints");
            yield return new TestCaseData(CreatePoints(-3, 3, 1, 1, 1.5, 5, 2, -3, 3.5, 2, 7, 1.5), CreatePoints(1, 1, 3.5, 2), 2.6925824036).SetName("SixPoints");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2, 3.5, 2.5, 7, 1.9, 2, 3, 3, 4.1, 6, 7, 5, 6, 2.9, 9, 5.1, 7.1, 7.1, 8, 1.8), CreatePoints(2, 3.5, 3, 3), 1.1180339887).SetName("LeftHalfPair");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2.5, 6.9, 2, 1.9, 3, 3, 4.1, 6, 7, 5.1, 6, 3.1, 9, 5.2, 7.1, 7.1, 8, 2, 7, 1.5), CreatePoints(8, 2, 7, 1.5), 1.1180339887).SetName("RightHalfPair");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2.5, 7, 2, 2, 3, 3, 4.5, 6, 7.1, 4.9, 6, 3.1, 9, 5.1, 7, 7.1, 8, 2.1, 5.5, 5.5), CreatePoints(4.5, 6, 5.5, 5.5), 1.1180339887).SetName("SplitPair");
            yield return new TestCaseData(CreatePoints(@"Data\Points50.txt"), CreatePoints(6.3, 0.6, 5.9, 0.1), 0.6403124237).SetName("Points50");
            yield return new TestCaseData(CreatePoints(@"Data\Points100.txt"), CreatePoints(18.7, 23.0, 17.5, 20.9), 2.4186773245).SetName("Points100");
            yield return new TestCaseData(CreatePoints(@"Data\Points200.txt"), CreatePoints(28.3, 16.0, 29.2, 15.3), 1.1401754251).SetName("Points200");
            yield return new TestCaseData(CreatePoints(@"Data\Points500.txt"), CreatePoints(-29.8, 12.5, -29.5, 13.0), 0.5830951895).SetName("Points500");
        }
    }
}
