using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairPointMinMax
{
    public class ClosestPair2D
    {
        /// <summary>
        /// Computes a distance separating the closest pair of points using a divide-and-conquer algorithm.
        /// Time complexity: O(n log(n))
        /// Assumption: No two points have the same x-coordinate and y-coordinate.
        /// 
        /// References:
        /// https://en.wikipedia.org/wiki/Closest_pair_of_points_problem
        /// https://www.geeksforgeeks.org/closest-pair-of-points-onlogn-implementation/
        /// https://www.cs.cmu.edu/~ckingsf/bioinfo-lectures/closepoints.pdf
        /// http://www.youtube.com/watch?v=vS4Zn1a9KUc 
        /// https://www.cs.mcgill.ca/~cs251/ClosestPair/ClosestPairDQ.html
        /// 
        /// [AlgoIlluminated-1] p.77-88 3.4 An O(n log(n))-Time Algorithm for the Closest Pair of Points.
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The pair of points with the smallest Euclidean distance</returns>
        public static (double Distance, Point[] ClosestPair) ComputeRecursive(Point[] points)
        {
            Debug.Assert(points.Length >= 2);

            // Sort the input points by their x-coordinate.
            var Px = from p in points
                     orderby p.X ascending, p.Y ascending
                     select p;

            // Sort the input points by their y-coordinate.
            var Py = from p in points
                     orderby p.Y ascending, p.X ascending
                     select p;

            return FindClosestPair(Px.ToArray(), Py.ToArray());
        }

        /// <summary>
        /// Computes a distance separating the closest pair of points recursively.
        /// </summary>
        /// <param name="Px">Points sorted by their x-coordinate</param>
        /// <param name="Py">Points sorted by their y-coordinate</param>
        /// <returns>The pair of points with the smallest Euclidean distance</returns>
        private static (double Distance, Point[] ClosestPair) FindClosestPair(Point[] Px, Point[] Py)
        {
            Debug.Assert(Px.Length >= 2);
            Debug.Assert(Py.Length >= 2);
            Debug.Assert(Px.Length == Py.Length);

            var size = Px.Length;

            // Base case: find the closest pair among two or three points using brute-force.
            // Because there are only two or three points, the time to find the closest
            // pair is constant O(1).
            if (size <= 3)
                return FindClosestPairBruteForce(Px);

            var halfSize = size / 2;

            // The first half of Px, sorted by x-coordinate.
            var Lx = new Point[halfSize];
            Array.Copy(Px, Lx, Lx.Length); // copy the first half of Px to Lx

            // The second half of Px, sorted by x-coordinate.
            var Rx = new Point[size - halfSize];
            Array.Copy(Px, halfSize, Rx, 0, Rx.Length); // copy the second half of Px to Rx

            // The first half of Px, sorted by y-coordinate.
            var Ly = new Point[halfSize];

            // The second half of Px, sorted by y-coordinate.
            var Ry = new Point[size - halfSize];

            var medianX = Lx[halfSize - 1].X;
            int i = 0, l = 0, r = 0;

            // Perform a linear scan over Py and populate Ly and Ry.
            // Note that Py contains the same points as Px just sorted by 
            // y-coordinate rather than by x-coordinate.
            // Because this is a linear scan, its running time is O(n).
            while(i < Py.Length)
            {
                if (Py[i].X <= medianX)
                {
                    Ly[l] = Py[i];
                    ++l;
                }
                else
                {
                    Ry[r] = Py[i];
                    ++r;
                }

                ++i;
            }

            // A pair of points is called
            // - leftPair if both points belong to the left half of the input point set
            // - rightPair if both points belong to the right half of the input point set
            // - splitPair if the points belong to different halves of the input point set

            // Find the best leftPair and the best rightPair recursively.
            var leftPair = FindClosestPair(Lx, Ly);
            var rightPair = FindClosestPair(Rx, Ry);

            var minDistance = Math.Min(leftPair.Distance, rightPair.Distance);

            // Find the best splitPair.
            var splitPair = FindClosestSplitPair(Px, Py, minDistance);

            // Return a pair with the shortest distance among leftPair, rightPair, and splitPair.
            if (leftPair.Distance < rightPair.Distance && leftPair.Distance < splitPair.Distance)
                return leftPair;
            else if (rightPair.Distance < leftPair.Distance && rightPair.Distance < splitPair.Distance)
                return rightPair;
            else if (splitPair.Distance < leftPair.Distance && splitPair.Distance < rightPair.Distance)
                return splitPair;
            else
                throw new NotImplementedException("Ties are not implemented.");
        }

        /// <summary>
        /// Finds a split pair whose distance is less than the input minDistance. 
        /// Time complexity: O(n)
        /// </summary>
        /// <param name="Px">Points sorted by their x-coordinate</param>
        /// <param name="Py">Points sorted by their y-coordinate</param>
        /// <param name="minDistance">A minimum distance found so far from the leftPair or rightPair</param>
        /// <returns>A split pair whose distance is less than the minDistance. If there is no such a split pair, returns null.</returns>
        private static (double Distance, Point[] ClosestPair) FindClosestSplitPair(Point[] Px, Point[] Py, double minDistance)
        {
            Debug.Assert(Px.Length == Py.Length);
            var size = Px.Length;

            var halfSize = size / 2;

            // medianX is the largest x-coordinate in the left half. A pair of points is a split pair
            // if one point has x <= mediaX and the other point x > medianX.
            var medianX = Px[halfSize - 1].X;

            // Sy is a set of points with x-coordinate between [mx-d, mx+d] sorted by y-coordinate
            // where mx is medianX, d is minDistance. We scan through Py and remove any points with
            // x-coordinate outside of the range [mx-d, mx+d].
            // Refer to [AlgoIlluminated-1] p.85 3.4.7 for the proof of correctness.
            var Sy = new List<Point>();
            foreach(var p in Py)
            {
                if (p.X > medianX - minDistance && p.X < medianX + minDistance)
                    Sy.Add(p);
            }

            // Use brute-force to find the closest split pair.
            if (Sy.Count >= 2)
            {
                var distance = minDistance;
                Point[] bestPair = null;

                for(var i = 0; i <= Sy.Count - 2; ++i)
                {
                    // Note that we iterate over 6 points at most.
                    for(var j = 1; j <= Math.Min(6, Sy.Count - i - 1); ++j)
                    {
                        var d = Math.Sqrt(GetSquaredDistance(Sy[i], Sy[i + j]));
                        if (d < distance)
                        {
                            distance = d;
                            bestPair = new Point[] { Sy[i], Sy[i + j] };
                        }
                    }
                }

                if (bestPair != null)
                    return (distance, bestPair);
            }

            return (double.MaxValue, null);
        }

        /// <summary>
        /// Finds a closest pair between two or three points.
        /// </summary>
        /// <param name="points">An array containing 2 or 3 points</param>
        /// <returns>The pair of points with the smallest Euclidean distance</returns>
        private static (double Distance, Point[] ClosestPair) FindClosestPairBruteForce(Point[] points)
        {
            Debug.Assert(points.Length == 2 || points.Length == 3);

            var d1 = Math.Sqrt(GetSquaredDistance(points[0], points[1]));

            if (points.Length == 2)
                return (d1, points);

            var d2 = Math.Sqrt(GetSquaredDistance(points[0], points[2]));
            var d3 = Math.Sqrt(GetSquaredDistance(points[1], points[2]));

            if (d1 < d2 && d1 < d3)
                return (d1, new Point[] { points[0], points[1] });
            else if (d2 < d1 && d2 < d3)
                return (d2, new Point[] { points[0], points[2] });
            else
                return (d3, new Point[] { points[1], points[2] });
        }

        /// <summary>
        /// Computes a distance separating the closest pair of points by iterating through all the pairs. 
        /// Time complexity: O(n^2)
        /// 
        /// [Sedgewick] 1.2.1 p.114 - Compute the distance separating the closest pair of points.
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The pair of points with the smallest Euclidean distance</returns>
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
