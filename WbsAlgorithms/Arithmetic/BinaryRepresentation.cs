using System.Text;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithms.Arithmetic
{
    /// <summary>
    /// [Sedgewick] 1.1.9 p.55 - Convert a decimal number to its binary representation. Provide a concise implementation.
    /// [Sedgewick] 1.3.5 p.161 - Convert a decimal number to its binary representation using a stack.
    /// 
    /// Example: GetBinary(50) returns 110010
    /// </summary>
    public class BinaryRepresentation
    {
        /// <summary>
        /// Converts a decimal number into its binary representation.
        /// </summary>
        /// <param name="n">The input number</param>
        /// <returns>Binary represenation of the input number as a string of 0s and 1s</returns>
        public static string GetBinary(int n)
        {
            var sb = new StringBuilder();
            for (var i = n; i > 0; i /= 2)
                sb.Insert(0, i % 2);
            return sb.Length == 0 ? "0" : sb.ToString();
        }

        /// <summary>
        /// Converts a decimal number into its binary representation using a stack.
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
