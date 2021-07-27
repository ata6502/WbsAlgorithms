using WbsAlgorithms.Collections;

namespace WbsAlgorithms.Arithmetics
{
    /// <summary>
    /// [Sedgewick] 1.3.5 p.161 - Convert decimal numbers to their binary representation.
    /// 
    /// Example: GetBinary(50) returns 110010
    /// </summary>
    public class BinaryRepresentation
    {
        /// <summary>
        /// Convert the input decimal number into its binary representation.
        /// </summary>
        /// <param name="n">The input number</param>
        /// <returns>Binary represenation of the input number as a string of 0s and 1s</returns>
        public static string GetBinaryUsingStack(int n)
        {
            const int Zero = (int)'0';

            if (n == 0)
                return "0";

            var stack = new StackArray<int>(8);

            while(n > 0)
            {
                stack.Push(n % 2);
                n /= 2;
            }

            var binary = new char[stack.Size];

            var i = 0;
            while(!stack.IsEmpty)
            {
                binary[i++] = (char)(stack.Pop() + Zero);
            }

            return new string(binary);
        }
    }
}
