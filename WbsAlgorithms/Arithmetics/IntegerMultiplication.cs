using System.Text;

namespace WbsAlgorithms.Arithmetics
{
    public class IntegerMultiplication
    {
        /// <summary>
        /// Multiplies two integers using the Karatsuba multiplication algorithm. The input integers
        /// are passed to the method as strings.
        /// 
        /// Karatsuba multiplication is a recursive algorithm for integer multiplication. It uses 
        /// the Gauss's trick to save one recursive call comparing to a more straightforward recursive algorithm.
        /// 
        /// [AlgoIlluminated-1] p.6-11 1.3 Karatsuba Multiplication
        /// </summary>
        /// <param name="in_x">n-digit positive integer</param>
        /// <param name="in_y">n-digit positive integer</param>
        /// <returns>The product x*y</returns>
        public static string KaratsubaMultiply(string in_x, string in_y)
        {
            if (in_x.Length == 1 && in_y.Length == 1)
            {
                // Base case.
                int n = int.Parse(in_x) * int.Parse(in_y);
                return n.ToString();
            }
            else
            {
                // Make the length of both numbers the same and the power of 2. 
                var paddedNumbers = StringArithmeticHelper.PadLeftZeros(in_x, in_y);
                var x = paddedNumbers.x;
                var y = paddedNumbers.y;

                // Recursive case.

                // Split numbers in half.
                string a = x.Substring(0, x.Length / 2);
                string b = x.Substring(x.Length / 2);
                string c = y.Substring(0, y.Length / 2);
                string d = y.Substring(y.Length / 2);

                // Calculate ac and bd just like in the Recursive Algorithm.
                string ac = KaratsubaMultiply(a, c);
                string bd = KaratsubaMultiply(b, d);

                // Calculate the sums before the 3rd recursive call: a+b and c+d
                string p = StringArithmeticHelper.AddNumbers(a, b);
                string q = StringArithmeticHelper.AddNumbers(c, d);

                // The 3rd recursive call to calculate p*q = (a+b)*(c+d)
                string pq = KaratsubaMultiply(p, q);

                // Add the results of the 1st and 2nd recursive step.
                string acbd = StringArithmeticHelper.AddNumbers(ac, bd);

                // Apply the Gauss trick: p*q - acbd = (a+b)*(c+d) - (ac+bd) = ad + bc
                string adbc = StringArithmeticHelper.SubtractNumbers(pq, acbd);

                int n = x.Length;

                // Compute: 10^n * ac + 10^(n/2) * adbc + bd
                string r1 = ac.PadRight(ac.Length + n, '0');
                string r2 = adbc.PadRight(adbc.Length + n / 2, '0');
                string r3 = StringArithmeticHelper.AddNumbers(r1, r2);
                string result = StringArithmeticHelper.AddNumbers(r3, bd).TrimStart('0');

                if (result.Length == 0)
                    result = "0";

                return result;
            }
        }

        /// <summary>
        /// Multiplies two integers using the recursive multiplication algorithm. The input integers
        /// are passed to the method as strings. The algorithm is based on the fact that an integer
        /// x with an even number of n digits can be expressed in terms of two n/2-digit numbers. 
        /// The two n/2-digit numbers a and b can be combined arithmetically to obtain x = 10^(n/2)*a + b
        /// Example: 5678 * 1234 --> ab* cd, a = 56 b=78 c=12 d=34
        /// 
        /// [AlgoIlluminated-1] p.8-10 1.3.2 The Recursive Multiplication Algorithm
        /// </summary>
        /// <param name="in_x">n-digit positive integer</param>
        /// <param name="in_y">n-digit positive integer</param>
        /// <returns>The product x*y</returns>
        public static string RecursiveMultiply(string in_x, string in_y)
        {
            // Base case: if x and y are 1-digit numbers, multiply them 
            // in one primitive operation and return the result.
            if (in_x.Length == 1 && in_y.Length == 1)
            {
                int n = int.Parse(in_x) * int.Parse(in_y);
                return n.ToString();
            }
            else
            {
                // Make the length of both numbers the same and the power of 2. 
                var paddedNumbers = StringArithmeticHelper.PadLeftZeros(in_x, in_y);
                var x = paddedNumbers.x;
                var y = paddedNumbers.y;

                // Recursive case.

                // Split numbers in half. The lengths of numbers are the powers of 2
                // hence thay can't be odd.
                string a = x.Substring(0, x.Length / 2);
                string b = x.Substring(x.Length / 2);
                string c = y.Substring(0, y.Length / 2);
                string d = y.Substring(y.Length / 2);

                string ac = RecursiveMultiply(a, c); // ac is the returned result
                string ad = RecursiveMultiply(a, d);
                string bc = RecursiveMultiply(b, c);
                string bd = RecursiveMultiply(b, d);

                // Compute 10^n * ac + 10^(n/2) * (ad + bc) + bd, where n = x.Length = y.Length (the same because they are padded)
                var n = x.Length;
                string p1 = ac.PadRight(ac.Length + n, '0');                // p1 = 10^n * ac
                string adbc = StringArithmeticHelper.AddNumbers(ad, bc);    // adbc = ab + bc
                string p2 = adbc.PadRight(adbc.Length + n / 2, '0');        // p2 = 10^(n/2) * adbc
                string p = StringArithmeticHelper.AddNumbers(p1, p2);       // p = 10^n * ac + 10^(n/2) * (ad + bc)
                string result = StringArithmeticHelper.AddNumbers(p, bd).TrimStart('0'); // result = p + bd

                if (result.Length == 0)
                    result = "0";

                return result;
            }
        }

        /// <summary>
        /// Multiplies two integers using the simple grade-school Algorithm algorithm. 
        /// The input integers are passed to the method as strings.
        /// 
        /// The algorithm uses the following primitive operations:
        /// - Adding two single-digit numbers.
        /// - Multiplying two single-digit numbers.
        /// - Adding a zero to the beginning or end of a number.
        /// 
        /// Asymptotic complexity: O(n^2), where n is the number of digits of the input numbers
        /// 
        /// Sample input:
        /// x = 5678  i = 0..3  n = x.Length
        /// y = 1234  j = 0..3  m = y.Length
        /// 
        /// Calculations:
        ///      5678
        ///      1234
        ///      ----
        ///     22712 row0
        ///    17034_ row1
        ///   11356__ row2
        ///   5678___ row3
        ///   -------
        ///   7006652
        ///   
        /// [AlgoIlluminated-1] p.4-6 1.2.3 The Grade-School Multiplication Algorithm
        /// </summary>
        /// <param name="x">n-digit nonnegative integer</param>
        /// <param name="y">n-digit nonnegative integer</param>
        /// <returns>The product x* y</returns>
        public static string SimpleMultiply(string x, string y)
        {
            var result = "0";
            var temp = new StringBuilder();
            var row = 0;

            // The second number.
            for (var j = y.Length - 1; j >= 0; --j)
            {
                var a = int.Parse(y[j].ToString());
                var carry = 0;

                // temp keeps the product from the current row augmented at the end with zeroes
                // for easy addition later.
                temp.Clear();
                temp.Append('0', row);

                // Compute a partial product using the current digit from the first number
                // and all the digits from the first number.
                for (var i = x.Length - 1; i >= 0; --i)
                {
                    var b = int.Parse(x[i].ToString());
                    var n = a * b + carry;
                    temp.Insert(0, n % 10);
                    carry = n / 10;
                }

                // Insert the lst leading digit.
                if (carry > 0)
                    temp.Insert(0, carry);

                // Move on to the next row.
                ++row;

                // Additions add additional primitive operations.
                result = StringArithmeticHelper.AddNumbers(result, temp.ToString());
            }

            return result.ToString();
        }
    }
}
