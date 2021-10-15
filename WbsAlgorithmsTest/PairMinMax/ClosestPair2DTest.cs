using NUnit.Framework;
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

            var result = ClosestPair2D.ComputeBruteForce(points);

            CollectionAssert.AreEqual(expectedClosestPair, result.ClosestPair);
            Assert.AreEqual(expectedDistance, result.Distance, Tolerance);
        }

        private static IEnumerable<TestCaseData> ComputeClosestPair2DTestCases()
        {
            yield return new TestCaseData(CreatePoints(1, 1, 4, 2), CreatePoints(1, 1, 4, 2), 3.1622776602).SetName("TwoPoints");
            yield return new TestCaseData(CreatePoints(3, 5, 1, 4, -1, 2), CreatePoints(3, 5, 1, 4), 2.2360679775).SetName("ThreePoints");
            yield return new TestCaseData(CreatePoints(-3, 3, 1, 1, 1.5, 5, 2, -3, 3, 2, 7, 1.5), CreatePoints(1, 1, 3, 2), 2.2360679775).SetName("SixPoints");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2, 3.5, 2.5, 7, 2, 2, 3, 3, 4, 6, 7, 5, 6, 3, 9, 5, 7, 7, 8, 2), CreatePoints(2, 3.5, 3, 3), 1.1180339887).SetName("LeftHalfPair");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2.5, 7, 2, 2, 3, 3, 4, 6, 7, 5, 6, 3, 9, 5, 7, 7, 8, 2, 7, 1.5), CreatePoints(8, 2, 7, 1.5), 1.1180339887).SetName("RightHalfPair");
            yield return new TestCaseData(CreatePoints(1, 5, 4, 4, 2.5, 7, 2, 2, 3, 3, 4.5, 6, 7, 5, 6, 3, 9, 5, 7, 7, 8, 2, 5.5, 5.5), CreatePoints(4.5, 6, 5.5, 5.5), 1.1180339887).SetName("SplitPair");
            yield return new TestCaseData(CreatePoints(@"Data\Points50.txt"), CreatePoints(3.0, 29.3, 3.1, 30.7), 1.4035668848).SetName("Points50");
            yield return new TestCaseData(CreatePoints(@"Data\Points100.txt"), CreatePoints(73.8, 93.0, 73.2, 92.9), 0.6082762530).SetName("Points100");
            yield return new TestCaseData(CreatePoints(@"Data\Points200.txt"), CreatePoints(11.9, 83.1, 11.4, 83.1), 0.5).SetName("Points200");
            yield return new TestCaseData(CreatePoints(@"Data\Points500.txt"), CreatePoints(28.0, 96.9, 27.9, 96.9), 0.1).SetName("Points500");
        }
    }
}
