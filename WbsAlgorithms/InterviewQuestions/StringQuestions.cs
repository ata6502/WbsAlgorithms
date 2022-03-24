using System;
using System.Diagnostics;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to reverse a string? [ReverseString]
    public class StringQuestions
    {
        public static string ReverseString(string s)
        {
            Debug.Assert(s != null);

            var a = s.ToCharArray();
            Array.Reverse(a);
            var reversed = string.Join("", a); // or new string(.)
            return reversed;
        }

        public static string ReverseStringWithoutUsingArrayReverse(string s)
        {
            Debug.Assert(s != null);

            var a = s.ToCharArray();

            for (int i = 0, j = a.Length - 1; i < j; ++i, --j)
            {
                var tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
            }

            return new string(a);
        }

        public static string ReverseStringUsingXorSwapping(string s)
        {
            Debug.Assert(s != null);

            var a = s.ToCharArray();

            for (int i = 0, j = a.Length - 1; i < j; ++i, --j)
            {
                a[i] ^= a[j];
                a[j] ^= a[i];
                a[i] ^= a[j];
            }

            return new string(a);
        }
    }
}
