using System;
using System.Text;

namespace WbsAlgorithms.Arithmetics
{
    /// <summary>
    /// The StringArithmetic class provides static methods that perform arithmetic
    /// operations on integers represented as strings. It allows us to do calculations 
    /// on numbers greater than C#'s maximum and minimum integers.
    /// </summary>
    public class StringArithmeticHelper
    {
        /// <summary>
        /// Makes the length of the input integers to be a power of 2. Also, the resulting
        /// lengths of both numbers are equal. The method achieves that by prepending leading 0s 
        /// to the beginning of the numbers. This operation at most doubles the lengths of 
        /// the numbers.
        /// </summary>
        /// <param name="x">The first integer in the string representation</param>
        /// <param name="y">The second integer in the string representation</param>
        /// <returns>The same integers as input but with leading 0s if needed</returns>
        public static (string x, string y) PadLeftZeros(string x, string y)
        {
            // Find the closest power of two for the number with the greater number of digits.
            int pow = (int)Math.Ceiling(Math.Log(Math.Max(x.Length, y.Length), 2.0));

            // Calculate the required number of digits.
            int digitCount = (2 << (pow - 1));

            // Pad the numbers with 0s and return.
            return (
                x.PadLeft(digitCount, '0'),
                y.PadLeft(digitCount, '0'));
        }

        /// <summary>
        /// Adds two integers represented by strings.
        /// </summary>
        /// <param name="x">The first integer in the string representation</param>
        /// <param name="y">The second integer in the string representation</param>
        /// <returns>The sum of the input integers as a string.</returns>
        public static string AddNumbers(string x, string y)
        {
            var sb = new StringBuilder();
            var carry = 0;

            for (int i = x.Length - 1, j = y.Length - 1; i >= 0 || j >= 0; --i, --j)
            {
                // Check if the i and j indices are less than 0 because the numbers
                // may have different lengths.

                // Convert the first number to a numerical value.
                var xnum = 0;
                if (i >= 0)
                    xnum = int.Parse(x[i].ToString());

                // Convert the second number to a numerical value.
                var ynum = 0;
                if (j >= 0)
                    ynum = int.Parse(y[j].ToString());

                // Add both numbers taking into account the value of carry.
                var n = xnum + ynum + carry;

                // Determine the carry value.
                carry = n / 10;

                // Keep only the remainder value as long as there are still more digits to process.
                // Otherwise, if both i=0 and j=0, we have achieved the leading digit. We just need
                // to keep this digit.
                if (i > 0 || j > 0)
                    n = n % 10;

                sb.Insert(0, n.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Subtracts two numbers represented as strings. The method has a constraint that
        /// the first number has to be greater or equal to the second number, i.e., x >= y
        /// </summary>
        /// <param name="x">The first integer in the string representation. It has to be greater then the second integer.</param>
        /// <param name="y">The second integer in the string representation</param>
        /// <returns>The result of the subtraction x - y. Because x >= y, the result is always greater than 0.</returns>
        public static string SubtractNumbers(string x, string y)
        {
            var sb = new StringBuilder();
            var borrow = 0;

            for (int i = x.Length - 1, j = y.Length - 1; i >= 0 || j >= 0; --i, --j)
            {
                var xnum = 0;
                if (i >= 0)
                    xnum = int.Parse(x[i].ToString());

                var ynum = 0;
                if (j >= 0)
                    ynum = int.Parse(y[j].ToString());

                if (xnum - borrow < ynum)
                {
                    if (xnum == 0)
                        xnum = 10 - borrow;
                    else
                        xnum = xnum - borrow + 10;

                    borrow = 1;
                }
                else
                {
                    xnum = xnum - borrow;
                    borrow = 0;
                }

                var n = xnum - ynum;

                sb.Insert(0, n.ToString());
            }

            // Remove prefix 0s if any.
            var result = sb.ToString().TrimStart('0');
            if (result.Length == 0)
                result = "0";

            return result;
        }
    }
}
