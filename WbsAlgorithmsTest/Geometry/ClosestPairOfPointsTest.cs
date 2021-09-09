using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WbsAlgorithms.Geometry;

namespace WbsAlgorithmsTest.Geometry
{
    [TestFixture]
    public class ClosestPairOfPointsTest
    {
        [TestCaseSource(nameof(ComputeClosestPairOfPointsTestCases))]
        public void ComputeClosestPairOfPointsTest(Point[] points, Point[] expectedClosestPair, double expectedDistance)
        {
            const double Tolerance = 0.0000000001;

            var result = ClosestPairOfPoints.ComputeBruteForce(points);

            CollectionAssert.AreEqual(expectedClosestPair, result.ClosestPair);
            Assert.AreEqual(expectedDistance, result.Distance, Tolerance);
        }

        private static IEnumerable<TestCaseData> ComputeClosestPairOfPointsTestCases()
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

        #region Private helpers
        private static Point[] CreatePoints(string filename)
        {
            var str = File.ReadAllText(filename).Split(',');
            var coordinates = new double[str.Length];
            var i = 0;

            foreach(var s in str)
            {
                if (double.TryParse(s, out var n))
                    coordinates[i] = n;
                else
                    throw new FormatException($"The string '{s}' in the position {i+1} is not correctly formatted number.");
                ++i;
            }

            return CreatePoints(coordinates);
        }

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
