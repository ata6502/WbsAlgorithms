using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Geometry;

namespace WbsAlgorithmsTest.Geometry
{
    [TestFixture]
    public class ClosestPointsTest
    {
        [TestCaseSource(nameof(GetShortestDistanceTestCases))]
        public void GetShortestDistanceTest(Point[] points, double expectedDistance)
        {
            const double Tolerance = 0.0000000001;

            var actualDistance = ClosestPoints.GetShortestDistanceBruteForce(points);

            Assert.AreEqual(expectedDistance, actualDistance, Tolerance);
        }

        private static IEnumerable<TestCaseData> GetShortestDistanceTestCases()
        {
            yield return new TestCaseData(CreatePoints(1, 1, 4, 2), 3.1622776602).SetName("TwoPoints");
            yield return new TestCaseData(CreatePoints(3, 5, 1, 4, -1, 2), 2.2360679775).SetName("ThreePoints");
            yield return new TestCaseData(CreatePoints(-3, 3, 1, 1, 1.5, 5, 2, -3, 3, 2, 7, 1.5), 2.2360679775).SetName("SixPoints");
        }

        #region Private helpers
        private static Point[] CreatePoints(params double[] coordinates)
        {
            Debug.Assert(coordinates.Length % 2 == 0);

            var pointCount = coordinates.Length / 2;
            var points = new Point[pointCount];

            for(var i = 0; i < pointCount; ++i)
            {
                var p = new Point(coordinates[2 * i], coordinates[2 * i + 1]);
                points[i] = p;
            }

            return points;
        }
        #endregion
    }
}
