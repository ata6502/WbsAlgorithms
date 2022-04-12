using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to reverse a string? [ReverseString]
    // 2. How to determine if a string is a palindrome? [IsPalindrome]
    // 3. How to find duplicated characters in a string? [FindDuplicatedCharacters]
    // 4. How to calculate the number of vowels and consonants in a string? [CountVowelsAndConsonants]
    // 5. How to count the number of occurrences of a given character in a string? [CountCharacter]
    // 6. How to remove all non-alphanumeric characters from a string? [RemoveNonAlphanumericCharacters]
    // 7. How to obtain all permutations of letters in a given string? [GetPermutations]
    // 8. How to determine if a string has all unique characters? [IsUnique] [CodingInterview] 1.1 p.90
    // 9. How to check if a string is a permutation of another one? [CheckPermution] [CodingInterview] 1.2 p.90
    public class StringQuestions
    {
        public static bool CheckPermutationUsingSorting(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            var a1 = s.ToCharArray();
            Array.Sort(a1);
            var a2 = t.ToCharArray();
            Array.Sort(a2);

            for(var i = 0; i < s.Length; ++i)
            {
                if (a1[i] != a2[i])
                    return false;
            }

            return true;
        }

        public static bool CheckPermutationUsingSortingAndLinq(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            return Sort(s).SequenceEqual(Sort(t));

            char[] Sort(string s)
            {
                var a = s.ToCharArray();
                Array.Sort(a);
                return a;
            }
        }

        // Constraint: ASCII codes of the characters in the input strings are in the range 0-128.
        public static bool CheckPermutationConstrained(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            var h = new int[128];

            foreach(var c in s)
            {
                var code = (byte)c;
                ++h[code];
            }

            foreach(var c in t)
            {
                var code = (byte)c;
                --h[code];
                if (h[code] < 0)
                    return false;
            }

            // The histogram h has no negative values. It means it also does not have any positive values.

            return true;
        }

        public static bool CheckPermutation(string s, string t)
        {
            if (s.Length != t.Length)
                return false;

            var hs = ComputeHistogram(s);
            var ht = ComputeHistogram(t);

            // Compare histograms for both strings.
            foreach(var x in hs)
            {
                if (!ht.TryGetValue(x.Key, out var y))
                    return false;
                if (x.Value != y)
                    return false;
            }

            return true;

            Dictionary<char, int> ComputeHistogram(string s)
            {
                var a = s.ToCharArray();
                var d = new Dictionary<char, int>(s.Length);

                foreach(var c in a)
                {
                    if (d.TryGetValue(c, out _))
                        ++d[c];
                    else
                        d[c] = 1;
                }

                return d;
            }
        }

        // Constraint: ASCII codes of the characters in the string s are in the range 0-128.
        public static bool IsUniqueConstrained(string s)
        {
            if (s.Length > 128)
                return false;

            var a = s.ToCharArray();
            var b = new bool[128];

            foreach(var c in a)
            {
                var code = (byte)c;
                if (b[code])
                    return false;
                b[code] = true;
            }

            return true;
        }

        // Constraint: ASCII codes of the characters in the string s are in the range 0-128.
        public static bool IsUniqueConstrainedUsingBitArray(string s)
        {
            if (s.Length > 128)
                return false;

            var a = s.ToCharArray();
            var b = new BitArray(128);

            foreach (var c in a)
            {
                var code = (byte)c;
                if (b[code])
                    return false;
                b[code] = true;
            }

            return true;
        }

        public static bool IsUniqueBruteForce(string s)
        {
            var a = s.ToCharArray();

            for (var i = 0; i < a.Length; ++i)
                for (var j = i + 1; j < a.Length; ++j)
                    if (a[i] == a[j])
                        return false;

            return true;
        }

        public static bool IsUniqueUsingHashSet(string s)
        {
            Debug.Assert(s != null);

            var t = new HashSet<char>();
            var a = s.ToCharArray();

            foreach(var c in a)
            {
                if (t.TryGetValue(c, out _))
                    return false;
                t.Add(c);
            }

            return true;
        }

        public static string[] GetPermutations(string s)
        {
            var permutations = new List<string>();
            GetPermuationsRecursive(s, "");
            return permutations.ToArray();

            void GetPermuationsRecursive(string s, string prefix)
            {
                if (s.Length == 0)
                {
                    permutations.Add(prefix);
                }
                else
                {
                    for (var i = 0; i < s.Length; ++i)
                    {
                        var rem = s.Substring(0, i) + s.Substring(i + 1);
                        GetPermuationsRecursive(rem, prefix + s[i]);
                    }
                }
            }
        }

        // Removes all non-alphanumeric characters from a string and leaves only letters and numbers.
        public static string RemoveNonAlphanumericCharacters(string s)
        {
            return Regex.Replace(s, "[^a-zA-Z0-9]", "");
        }

        public static int CountCharacter(string s, char ch)
        {
            Debug.Assert(s != null);

            var counter = 0;
            foreach (var c in s)
                if (c == ch)
                    ++counter;
            return counter;
        }

        public static int CountCharacterUsingRegEx(string s, char ch)
        {
            Debug.Assert(s != null);

            return Regex.Matches(s, ch.ToString()).Count;
        }

        // Counts the number of vowels and consonants in a string containing only letters.
        public static (int VowelCount, int ConsonantCount) CountVowelsAndConsonants(string s)
        {
            Debug.Assert(s != null);

            var lowerCaseStr = Regex.Replace(s.ToLower(), "[^a-z]", "");

            if (lowerCaseStr.Length != s.Length)
                throw new ArgumentException($"The input string {s} should contain only letters.");

            // Count vowels.
            var counter = 0;
            foreach (var c in lowerCaseStr)
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                    ++counter;

            // Returns the number of vowels and consonants.
            return (counter, lowerCaseStr.Length - counter);
        }

        // Returns a string formed from characters that are duplicated in the input 
        // string at least twice.
        public static char[] FindDuplicatedCharactersUsingDictionary(string s)
        {
            Debug.Assert(s != null);

            var d = new Dictionary<char, int>();

            foreach (var c in s)
            {
                if (d.TryGetValue(c, out _))
                    ++d[c];
                else
                    d[c] = 1;
            }

            var dublicates = new List<char>();
            foreach (var kvp in d)
                if (kvp.Value > 1)
                    dublicates.Add(kvp.Key);

            return dublicates.ToArray();
        }

        public static char[] FindDuplicatedCharactersUsingLinq(string s)
        {
            Debug.Assert(s != null);

            var groups = from c in s
                         group c by c into g
                         where g.Count() > 1
                         select g.Key;

            return groups.ToArray();
        }

        // Determines if the input string is a palindrome. A palindrome is
        // a string that reads the same backward as forward.
        public static bool IsPalindrome(string s)
        {
            Debug.Assert(s != null);

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
