using System;

namespace WbsAlgorithms.Arithmetics
{
    /// <summary>
    /// Given a 32-bit signed integer, reverse digits of the integer.
    /// 
    /// Example 1: Input: 123, Output: 321
    /// Example 2: Input: -123, Output: -321
    /// Example 3: Input: 120, Output: 21
    /// Example 4: Input: 1534236469, Output: 0 (overflow)
    /// Example 5: Input: Int32.MinValue, Output: 0 (overflow)
    /// 
    /// [Leetcode] https://leetcode.com/problems/reverse-integer/submissions/
    /// </summary>
    public class IntegerReversal
    {
        /// <summary>
        /// Reverses the input integer. Relies on the checked() statement to detect 
        /// overflow and catch the OverflowException.
        /// </summary>
        /// <param name="n">The input 32-bit integer</param>
        /// <returns>The input integer with reversed digits; 0 when the reversed integer overflows</returns>
        public static int Reverse(int n)
        {
            if (n == int.MinValue)
                return 0;

            var r = 0;
            var sign = n < 0 ? -1 : 1;

            n = Math.Abs(n);

            while (n > 0)
            {
                try
                {
                    r = checked(10 * r + (n % 10)); // may overflow
                }
                catch (OverflowException)
                {
                    return 0;
                }

                n = n / 10;
            }

            return sign * r;
        }

        /// <summary>
        /// Reverses the input integer without relying on the checked() statement and 
        /// on catching the OverflowException.
        /// 
        /// We check beforehand if there is overflow using the following logic (assume r > 0):
        /// If r = r * 10 + pop causes overflow, then it must be that r >= IntMax/10:
        ///     If r > IntMax/10, then r = r * 10 + pop is guaranteed to overflow.
        ///     If r == IntMax/10, then r = r * 10 + pop will overflow if and only if pop > 7 i.e., the last digit is greater than 7 (Int32.MaxValue is 2147483647)
        /// Similar logic can be applied when r < 0
        /// </summary>
        /// <param name="n">The input 32-bit integer</param>
        /// <returns>The input integer with reversed digits; 0 when the reversed integer overflows</returns>
        public static int ReverseWithoutChecked(int n)
        {
            int r = 0;

            while (n != 0)
            {
                // Repeatedly "pop" the last digit of n and "push" it to the back of r.
                // In the end, r will be the reverse of the n.

                // pop
                int pop = n % 10;
                n /= 10;

                // Check if the statement r = r * 10 + pop will cause an overflow.
                if (r > int.MaxValue / 10 || (r == int.MaxValue / 10 && pop > 7)) // int.MaxValue == 2147483647
                    return 0;
                if (r < int.MinValue / 10 || (r == int.MinValue / 10 && pop < -8)) // int.MinValue == -2147483648
                    return 0;

                // push
                r = r * 10 + pop;
            }

            return r;
        }

        /// <summary>
        /// Reverses the input integer by first converting it to a string. Then, reversing the string
        /// and converting it back to an integer.
        /// </summary>
        /// <param name="n">The input 32-bit integer</param>
        /// <returns>The input integer with reversed digits; 0 when the reversed integer overflows</returns>
        public static int ReverseUsingString(int n)
        {
            if (n == int.MinValue)
                return 0;

            var s = (n < 0 ? "-" : "") + ReverseString(Math.Abs(n).ToString());

            if (int.TryParse(s, out var v))
                return v;
            else
                return 0;

            string ReverseString(string s)
            {
                var a = s.ToCharArray();
                for (var i = 0; i < a.Length / 2; ++i)
                {
                    var c = a[i];
                    a[i] = a[a.Length - i - 1];
                    a[a.Length - i - 1] = c;
                }
                return new string(a);
            }
        }
    }
}
