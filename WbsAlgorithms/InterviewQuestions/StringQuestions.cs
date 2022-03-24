using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to reverse a string? [ReverseString]
    // 2. How to determine if a string is a palindrome? [IsPalindrome]
    // 3. How to find duplicated characters in a string? [FindDuplicatedCharacters]
    public class StringQuestions
    {
        // Returns a string formed from characters that are duplicated in the input 
        // string at least twice.
        public static string FindDuplicatedCharactersUsingLinq(string s)
        {
            var groups = from c in s
                         group c by c into g
                         where g.Count() > 1
                         select g.Key;

            return new string(groups.ToArray());
        }

        public static string FindDuplicatedCharactersUsingDictionary(string s)
        {
            var d = new Dictionary<char, int>();

            foreach (var c in s)
            {
                if (d.TryGetValue(c, out _))
                    ++d[c];
                else
                    d[c] = 1;
            }

            var sb = new StringBuilder();
            foreach (var kvp in d)
                if (kvp.Value > 1)
                    sb.Append(kvp.Key);

            return sb.ToString();
        }

        // Determines if the input string is a palindrome. A palindrome is
        // a string that reads the same backward as forward.
        public static bool IsPalindrome(string s)
        {
            for (int i = 0, j = s.Length - 1; i < j; ++i, --j)
            {
                if (s[i] != s[j])
                    return false;
            }

            return true;
        }

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
