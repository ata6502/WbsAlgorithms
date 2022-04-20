using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    // 10. How to replace spaces with %20? [URLify] [CodingInterview] 1.3 p.90
    // 11. How to check if a given permutation is a palindrome? [IsPalindromePermutation] [CodingInterview] 1.4 p.91 (simplification: case-sensitive; includes special characters)
    // 12. How to check if two strings are one edit (or zero edits) away? [AreOneEditAway] [CodingInterview] 1.5 p.91
    // 13. How to compress a string e.g., aabcccccaaa --> a2b1c5a3 ? [CompressString] [CodingInterview] 1.6 p.91
    // 14. How to check if a string is a rotation of another string? [IsRotation] [CodingInterview] 1.9 p.91 --> SimpleAlgorithms.AreStringsCircularRotations
    public class StringQuestions
    {
        // Assumption: the input string contains only letters 'a'-'z' and 'A'-'Z'.
        public static string CompressString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            var prev = s[0];
            var cnt = 1;
            var sb = new StringBuilder();

            for(var i = 1; i < s.Length; ++i)
            {
                var c = s[i];

                if (c != prev)
                {
                    sb.Append($"{prev}{cnt}");
                    cnt = 0;
                }

                prev = c;
                ++cnt;
            }

            sb.Append($"{prev}{cnt}");

            var compressed = sb.ToString();

            // Return the original string if the compressed string is not smaller.
            return compressed.Length < s.Length ? compressed : s;
        }

        // This method checks the next character rather than keeping track of the previous one.
        public static string CompressStringWithCheckingNext(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            var cnt = 0;
            var sb = new StringBuilder();

            for (var i = 0; i < s.Length; ++i)
            {
                ++cnt;

                // The i == s.Length - 1 condition detects if the loop reached
                // the last character (i.e., the last index) in the string.
                // If so, it short circuits the other condition that would 
                // throw IndexOutOfRange exception.
                if (i == s.Length - 1 || s[i] != s[i + 1])
                {
                    sb.Append($"{s[i]}{cnt}");
                    cnt = 0;
                }
            }

            var compressed = sb.ToString();

            // Return the original string if the compressed string is not smaller.
            return compressed.Length < s.Length ? compressed : s;
        }

        public static bool AreOneEditAway(string s, string t)
        {
            // If both strings have the same length it means that they may differ
            // only by replacing any character(s).
            if (s.Length == t.Length)
                return CheckReplace();
            // If a string has one more character than the other string we can treat
            // it as an insertion of the character. Alternatively, we can view it as
            // deletion of the same character from the shorter string.
            else if (Math.Abs(s.Length - t.Length) == 1)
                return CheckInsertOrDelete();
            return false;

            bool CheckReplace()
            {
                var foundReplacedChar = false;
                for(var i = 0; i < s.Length; ++i)
                {
                    if (s[i] != t[i])
                    {
                        if (foundReplacedChar)
                            return false;
                        foundReplacedChar = true;
                    }
                }
                return true;
            }

            // Inserting a char in one string is an equivalent to deleting the same char
            // from the other string.
            bool CheckInsertOrDelete()
            {
                var isCharSkipped = false;

                var i = 0;
                var j = 0;

                while(i < s.Length && j < t.Length)
                {
                    if (s[i] != t[j])
                    {
                        if (isCharSkipped)
                            return false;

                        isCharSkipped = true;

                        // Skip the missing char in the longer string. We are allowed to 
                        // skip a char only once.
                        if (s.Length > t.Length)
                            ++i;
                        else
                            ++j;
                    }
                    else
                    {
                        ++i;
                        ++j;
                    }
                }

                return true;
            }
        }

        // Check if the input string s is a palindrome permutation.
        // Assumption: The input string is made of characters that have ASCII codes in the range 0-128.
        public static bool IsPalindromePermutation(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            var frequency = new int[128];

            // Count frequencies of each letter in the input string.
            // For even-length strings, all frequencies should be even.
            // For odd-length string, all frequencies should be even except one that should be odd.
            foreach (var ch in s)
                ++frequency[(byte)ch];

            // Keeps a track if we've already encountered an odd frequency of a character.
            var hasOneOddCharacter = false;

            // Test if the frequencies have no more than one odd character.
            foreach (var f in frequency)
            {
                if (f % 2 == 1)
                {
                    if (hasOneOddCharacter)
                        return false;
                    hasOneOddCharacter = true;
                }
            }

            return true;
        }

        // The input string s has exact space at the end to hold the additional characters i.e., the length
        // of the string s is the length after the space-to-%20 replacement.
        // The len parameter is the true length of the string (without the trailing spaces).
        // For this excercise, you need to convert the string to an array to perform the replace
        // operation in-place.
        public static string URLify(string s, int len)
        {
            var a = s.ToCharArray();

            var j = a.Length - 1; // new index
            for(var i = len - 1; i >= 0; --i) // traverse the original string
            {
                if (a[i] == ' ')
                {
                    a[j] = '0';
                    a[j - 1] = '2';
                    a[j - 2] = '%';

                    j -= 3;
                }
                else
                {
                    a[j] = a[i];
                    j--;
                }
            }

            return new string(a);
        }

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
