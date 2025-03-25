using System;
using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairPoint
{
    /// [Sedgewick] 1.4.26 p.211 (3-collinearity)
    /// Suppose that you have an algorithm that takes as input N distinct points in the plane and 
    /// can return the number of triples that fall on the same line. Show that you can use this 
    /// algorithm to solve the 3-sum problem. 
    /// Hint: Use algebra to show that (a, a^3), (b, b^3), and(c, c^3) are collinear if and only if a + b + c = 0.
    /// 
    /// Three points A,B,C are collinear if the lines AB and BC have the same slopes.
    /// Having A=(x1,y1), B=(x2,y2), C=(x3,y3) we need to check if 
    /// (y2-y1) / (x2-x1) = (y3-y2) / (x3-x2)  -or-  (y2-y1) / (x2-x1) = (y3-y1) / (x3-x1)
    /// We have a special case when both slopes are zero.
    ///
    /// References: 
    /// https://stackoverflow.com/questions/2734301/given-a-set-of-points-find-if-any-of-the-three-points-are-collinear
    /// https://stackoverflow.com/questions/3813681/checking-to-see-if-3-points-are-on-the-same-line
    /// https://stackoverflow.com/questions/4179581/what-is-the-most-efficient-algorithm-to-find-a-straight-line-that-goes-through-m
    public class CollinearPoints
    {
        /// <summary>
        /// Determines the number of triples that fall on the same line given a set of points in the plane.
        /// </summary>
        /// <param name="points">A set of points in the plane</param>
        /// <returns>The number of triples that fall on the same line</returns>
        public static int CountTriplesBruteForce(Point[] points)
        {
            var tripletCount = 0;
            var len = points.Length;

            for (var i = 0; i < len; ++i)
            {
                for (var j = i + 1; j < len; ++j)
                {
                    var A = points[i];
                    var B = points[j];
                    var denominator1 = B.X - A.X;
                    var m1 = Math.Abs(denominator1) <= double.Epsilon ? double.PositiveInfinity : (B.Y - A.Y) / denominator1;

                    for (var k = j + 1; k < len; ++k)
                    {
                        var C = points[k];
                        var denominator2 = C.X - A.X;
                        var m2 = Math.Abs(denominator2) <= double.Epsilon ? double.PositiveInfinity : (C.Y - A.Y) / denominator2;

                        var bothSlopesInfinity = m1 == double.PositiveInfinity && m2 == double.PositiveInfinity;
                        var sameXs = A.X == B.X && B.X == C.X;

                        if ((bothSlopesInfinity && sameXs) || (!bothSlopesInfinity && Math.Abs(m2 - m1) < double.Epsilon))
                            ++tripletCount;
                    }
                }
            }

            return tripletCount;
        }

        /// <summary>
        /// Determines the number of triples that fall on the same line by calculating and storing slopes 
        /// between all pairs of points first. Then, iterating over those slopes and checking if the points 
        /// with the same slope are collinear.
        /// </summary>
        /// <param name="points">A set of points in the plane</param>
        /// <returns>The number of triples that fall on the same line</returns>
        public static int CountTriplesUsingSlopes(Point[] points)
        {
            // The points are collinear if (y2-y1) / (x2-x1) = (y3-y1) / (x3-x1)

            var len = points.Length;

            // Group pairs of points with the same slope.
            var slopes = new Dictionary<double, List<int>>();
            for (var i = 0; i < len; ++i)
            {
                for (var j = i + 1; j < len; ++j)
                {
                    var A = points[i];
                    var B = points[j];
                    var denominator = B.X - A.X;
                    var slope = Math.Abs(denominator) <= double.Epsilon ? double.PositiveInfinity : (B.Y - A.Y) / denominator;

                    if (slopes.TryGetValue(slope, out var indices))
                    {
                        if (!indices.Contains(i))
                            indices.Add(i);

                        if (!indices.Contains(j))
                            indices.Add(j);
                    }
                    else
                    {
                        slopes[slope] = new List<int> { i, j };
                    }
                }
            }

            // Iterate over slopes and check if the points with the same slope are collinear.
            var tripletCount = 0;
            foreach(var slopeIndices in slopes)
            {
                var indices = slopeIndices.Value;
                var slope = slopeIndices.Key;

                // Initialize the starting point A. The pointCount n will contain the number
                // of points that are collinear with the point A. We need to initialize n to 1
                // to include the point A.
                var A = points[indices[0]];
                var n = 1;

                for (var i = 1; i < indices.Count; ++i)
                {
                    var B = points[indices[i]];
                    var denominator = B.X - A.X;
                    var nextSlope = Math.Abs(denominator) <= double.Epsilon ? double.PositiveInfinity : (B.Y - A.Y) / denominator;

                    var bothSlopesInfinity = slope == double.PositiveInfinity && nextSlope == double.PositiveInfinity;
                    var sameXs = A.X == B.X;

                    if ((bothSlopesInfinity && sameXs) || (!bothSlopesInfinity && Math.Abs(nextSlope - slope) < double.Epsilon))
                        ++n;
                    else
                    {
                        // Calculate the number of triplets using combinatorics: the number of ways to choose a k-elements
                        // from an n-elements is given by the formula for binomial coefficients: C(n, k) = n! / ((n - k)! k!)
                        // For k = 3 we have:
                        tripletCount += (n - 2) * (n - 1) * n / 6;

                        // We have another group of points with the same slope but not collinear with the previous points.
                        // Initialize a new starting point and pointCount.
                        A = points[indices[i]];
                        n = 1;
                    }
                }

                // Calculate the number of triplets for the last group of points.
                tripletCount += (n - 2) * (n - 1) * n / 6;
            }

            return tripletCount;
        }

        /// <summary>
        /// Determines the number of triples that fall on the same line.
        /// If x + y + z = 0 then:
        /// - the slope of the line from x to y is (y^3 - x^3) / (y - x) = y^2 + yx + x^2
        /// - the slope of the line from x to z is (z^3 - x^3) / (z - x) = z^2 + zx + x^2
        /// 
        /// Therefore, if the slope from x to y equals the slope from x to z then 
        /// y^2 + yx + x^2 = z^2 + zx + x^2
        /// 
        /// This implies that (y - z) (x + y + z) = 0
        /// i.e., either y = z or z = -x - y.
        /// </summary>
        /// <param name="points">A set of points in the plane</param>
        /// <returns>The number of triples that fall on the same line</returns>
        public static int CountTriplesWithCubicY(Point[] points)
        {
            var len = points.Length;

            // Check if there are any duplicates in pointsX.
            var pointsX = new Dictionary<double, List<int>>();
            for (var i = 0; i < len; ++i)
            {
                if (pointsX.TryGetValue(points[i].X, out var indices))
                    indices.Add(i);
                else
                    pointsX[points[i].X] = new List<int> { i };
            }

            var tripletCount = 0;
            for (var i = 0; i < len; ++i)
            {
                for (var j = i + 1; j < len; ++j)
                {
                    var complementPoint = -1.0 * (points[i].X + points[j].X);

                    if (pointsX.TryGetValue(complementPoint, out var indices))
                    {
                        foreach (var index in indices)
                        {
                            if (index > i && index > j)
                                ++tripletCount;
                        }
                    }
                }
            }

            return tripletCount;
        }
    }
}
