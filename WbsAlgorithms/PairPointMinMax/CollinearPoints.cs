using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairPointMinMax
{
    /// [Sedgewick] 1.4.26 p.211 (3-collinearity)
    /// Suppose that you have an algorithm that takes as input N distinct points in the plane and 
    /// can return the number of triples that fall on the same line. Show that you can use this 
    /// algorithm to solve the 3-sum problem. 
    /// Hint: Use algebra to show that (a, a^3), (b, b^3), and(c, c^3) are collinear if and only if a + b + c = 0.
    /// 
    /// Three points A,B,C are collinear if the lines AB and BC have the same slopes.
    /// Having A=(x1,y1), B=(x2,y2), C=(x3,y3) we need to check if 
    /// (y2-y1) / (x2-x1) = (y3-y2) / (x3-x2)
    ///
    /// References: 
    /// https://stackoverflow.com/questions/2734301/given-a-set-of-points-find-if-any-of-the-three-points-are-collinear
    /// https://stackoverflow.com/questions/4179581/what-is-the-most-efficient-algorithm-to-find-a-straight-line-that-goes-through-m
    public class CollinearPoints
    {
        /// <summary>
        /// Determines the number of triples that fall on the same line given a set of points in the plane.
        /// </summary>
        /// <param name="points">A set of points in the plane</param>
        /// <returns>The number of triples that fall on the same line</returns>
        public static int GetNumberOfTriplesBruteForce(Point[] points)
        {
            var cnt = 0;
            var len = points.Length;

            for (var i = 0; i < len; ++i)
            {
                for (var j = i + 1; j < len; ++j)
                {
                    var A = points[i];
                    var B = points[j];
                    var m1 = (B.Y - A.Y) / (B.X - A.X);

                    for (var k = j + 1; k < len; ++k)
                    {
                        var C = points[k];
                        var m2 = (C.Y - B.Y) / (C.X - B.X);

                        if (Math.Abs(m2 - m1) < double.Epsilon)
                            ++cnt;
                    }
                }
            }

            return cnt;
        }
    }
}
