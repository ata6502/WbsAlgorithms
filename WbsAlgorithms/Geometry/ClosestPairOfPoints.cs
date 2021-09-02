using System;
using System.Diagnostics;

namespace WbsAlgorithms.Geometry
{
    /// <summary>
    /// [Sedgewick] 1.2.1 p.114 - Compute the distance separating the closest pair of points.
    /// </summary>
    public class ClosestPairOfPoints
    {
        /// <summary>
        /// Computes the distance separating the closest pair of points by iterating through 
        /// all the pairs. Time complexity: O(n^2)
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The distance separating the closest pair of points and the pair of points itself</returns>
        public static (double Distance, Point[] ClosestPair) ComputeBruteForce(Point[] points)
        {
            Debug.Assert(points.Length >= 2);

            var min = double.MaxValue;
            var closestPair = new Point[] { points[0], points[1] };

            for (int i = 0; i < points.Length; ++i)
            {
                for (var j = i + 1; j < points.Length; ++j)
                {
                    var d = GetSquaredDistance(points[i], points[j]);

                    if (d < min)
                    {
                        min = d;
                        closestPair[0] = points[i];
                        closestPair[1] = points[j];
                    }
                }
            }

            // Calculate the actual distance as min contains the square of the minimum distance.
            return (Math.Sqrt(min), closestPair);
        }

        /// <summary>
        /// Calculates the squared Euclidean distance between two points. We just need it
        /// for comparison so it does not matter if it is the exact distance or squared.
        /// </summary>
        /// <param name="p1">The first point</param>
        /// <param name="p2">The second point</param>
        /// <returns>The square of the distance between the two input points</returns>
        private static double GetSquaredDistance(Point p1, Point p2) =>
            Math.Pow(p1.X - p2.X, 2.0) + Math.Pow(p1.Y - p2.Y, 2.0);
    }
}
