using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairMinMax
{
    /// <summary>
    /// [Sedgewick] 1.2.1 p.114 - Compute the distance separating the closest pair of points.
    /// </summary>
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
        /// [AlgoIlluminated-1] p.77- 3.4 An O(n log(n))-Time Algorithm for the Closest Pair of Points.
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The pair of points with the smallest Euclidean distance and the pair of points itself</returns>
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
        /// TODO: Add comments.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        private static (double Distance, Point[] ClosestPair) FindClosestPair(Point[] Px, Point[] Py)
        {
            Debug.Assert(Px.Length == Py.Length);
            var size = Px.Length;

            // Base case: sort up to three points using brute-force.
            if (size <= 3)
                return ComputeBruteForce(Px);

            var halfSize = size / 2;

            var Lx = new Point[halfSize];
            Array.Copy(Px, Lx, Lx.Length);

            var Rx = new Point[size - halfSize];
            Array.Copy(Px, halfSize, Rx, 0, Rx.Length);

            var Ly = new Point[halfSize];
            var Ry = new Point[size - halfSize];
            var medianX = Lx[halfSize - 1].X;

            int i = 0, l = 0, r = 0;

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

            var leftPair = FindClosestPair(Lx, Ly);
            var rightPair = FindClosestPair(Rx, Ry);

            var minDistance = Math.Min(leftPair.Distance, rightPair.Distance);
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

        private static (double Distance, Point[] ClosestPair) FindClosestSplitPair(Point[] Px, Point[] Py, double minDistance)
        {
            Debug.Assert(Px.Length == Py.Length);
            var size = Px.Length;

            var halfSize = size / 2;
            var medianX = Px[halfSize - 1].X;

            var Sy = new List<Point>(6); // at most 6-points
            foreach(var p in Py)
            {
                if (p.X > medianX - minDistance && p.X < medianX + minDistance)
                    Sy.Add(p);
            }

            var pair = ComputeBruteForce(Sy.ToArray());

            if (pair.Distance < minDistance)
                return pair;
            else
                return (double.MaxValue, null);
        }

        /// <summary>
        /// Computes a distance separating the closest pair of points by iterating through all the pairs. 
        /// Time complexity: O(n^2)
        /// </summary>
        /// <param name="points">A set of 2D points</param>
        /// <returns>The pair of points with the smallest Euclidean distance and the pair of points itself</returns>
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
