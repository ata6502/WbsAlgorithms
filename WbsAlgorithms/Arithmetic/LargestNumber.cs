using System;
using System.Linq;

namespace WbsAlgorithms.Arithmetic
{
    /// <summary>
    /// Given a list of non-negative integers, arrange them such that they form the largest number.
    /// Example: Given [3, 30, 34, 5, 9], the largest formed number is 9534330.
    ///
    /// [InterviewBit] (Amazon) https://www.interviewbit.com/problems/largest-number/
    /// </summary>
    public class LargestNumber
    {
        /// <summary>
        /// Arrange numbers from the input array to form the largest number.
        /// This method coverts input numbers to strings at the beginning of the method.
        /// Then, it uses a custom comparer to sort the strings. The custom comparer checks
        /// all pairs of numbers. For example, for numbers 3,30,34,5,9, the custom comparer 
        /// checks the following pairs:
        /// - pair (3,30)  compares "330" and "303"   --> "330"  is greater
        /// - pair (3,34)  compares "334" and "343"   --> "343"  is greater
        /// - pair (3,5)   compares "35" and "53"     --> "53 "  is greater
        /// - pair (3,9)   compares "39" and "93"     --> "93"   is greater
        /// - pair (30,34) compares "3034" and "3430" --> "3430" is greater
        /// - pair (30,5)  compares "305" and "530"   --> "530"  is greater
        /// - pair (30,9)  compares "309" and "930"   --> "930"  is greater
        /// - pair (34,5)  compares "345" and "534"   --> "534"  is greater
        /// - pair (34,9)  compares "349" and "934"   --> "934"  is greater
        /// - pair (5,9)   compares "59" and "95"     --> "95"   is greater
        /// </summary>
        /// <param name="a">A list of non-negative integers</param>
        /// <returns>The largest number as a string</returns>
        public static string GetLargestNumber1(int[] a)
        {
            // Convert input numbers to strings.
            var s = a.Select(x => x.ToString()).ToArray();

            // Sort the strings using a custom comparer. 
            Array.Sort(s, (x, y) => (y + x).CompareTo(x + y));

            // Join the sorted numbers to form the largest possible number.
            var numStr = string.Join("", s);

            // Check a corner case when there are only zeroes.
            if (numStr.TrimStart('0').Length == 0)
                numStr = "0";

            return numStr;
        }

        /// <summary>
        /// Arrange numbers from the input array to form the largest number.
        /// This method coverts input numbers to strings during comparison in a custom comparer.
        /// </summary>
        /// <param name="a">A list of non-negative integers</param>
        /// <returns>The largest number as a string</returns>
        public static string GetLargestNumber2(int[] a)
        {
            // Sort the numbers using a custom comparer.
            Array.Sort(a, Compare);

            // Join the sorted numbers to form the largest possible number.
            var numStr = string.Join("", a);

            // Check a corner case when there are only zeroes.
            if (numStr.TrimStart('0').Length == 0)
                numStr = "0";

            return numStr;

            int Compare(int x, int y)
            {
                var xs = x.ToString();
                var ys = y.ToString();

                return (ys + xs).CompareTo(xs + ys);
            }
        }
    }
}
