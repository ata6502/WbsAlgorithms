using System;

namespace WbsAlgorithms.Geometry
{
    /// <summary>
    /// [Sedgewick] 1.2.1 p.114 - Compute the distance separating the closest pair of points.
    /// </summary>
    public class ClosestPoints
    {
        /// <summary>
        /// Computes the distance separating the closest pair of points by iterating through 
        /// all the pairs. Time complexity: O(n^2)
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The distance separating the closest pair of points</returns>
        public static double GetShortestDistanceBruteForce(Point[] points)
        {
            var min = double.MaxValue;

            for (int i = 0; i < points.Length; ++i)
            {
                for (var j = i + 1; j < points.Length; ++j)
                {
                    var d = GetSquaredDistance(points[i], points[j]);
                    min = Math.Min(d, min);
                }
            }

            // Caclulate the actual distance as min contains the square of the minimum distance.
            return Math.Sqrt(min);
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
