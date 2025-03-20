using System;

namespace WbsAlgorithms.Strings
{
    // A palindrome is a string that reads the same backward as forward.
    public class PalindromeAlgorithms
    {
        // Determines if a string is a palindrome using a for loop with one variable. 
        public static bool IsPalindrome1(string s)
        {
            for (var i = 0; i < s.Length / 2; ++i)
            {
                if (s[i] != s[s.Length - 1 - i])
                    return false;
            }

            return true;
        }

        // Determines if a string is a palindrome using a for loop with two variables. 
        public static bool IsPalindrome2(string s)
        {
            for (int i = 0, j = s.Length - 1; i < j; ++i, --j)
            {
                if (s[i] != s[j])
                    return false;
            }

            return true;
        }

        // Determines if a string is a palindrome using a while loop. Expands search
        // from the palindrome's center.
        public static bool IsPalindrome3(string s)
        {
            var n = s.Length;

            // Indices of the expanding palindrome substring [i,j)
            var i = n / 2;
            var j = n / 2;

            if (n % 2 == 0)
                --i;

            while (i >= 0 && j < n)
            {
                if (s[i] != s[j])
                    return false;

                --i;
                ++j;
            }

            return true;
        }

        // Checks if the a string is a palindrome permutation.
        // Assumption: The input string is made of characters that have ASCII codes in the range 0-128.
        // [CodingInterview] 1.4 p.91 (simplification: case-sensitive; includes special characters)
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

        /*
            Find the longest palindrome substring in a given string.
            [Leetcode]: https://leetcode.com/problems/longest-palindromic-substring/solution/

            A palindrome can be expanded from its center as it mirrors its characters. There are 2n-1 such centers.
            The number 2n-1 comes from the fact that the center of a palindrome can be between two letters. Such 
            palindromes have even number of letters e.g., "abba"'s center is between the two b's.

            Time complexity: O(n^2). Since expanding a palindrome around its center could take O(n) time, the overall complexity is O(n^2)
            Space complexity: O(1)
        */
        public static string GetLongestPalindrome(string s)
        {
            if (s.Length <= 1)
                return s;

            int index = 0, length = 0;

            for (var i = 0; i < s.Length; ++i)
            {
                var len = Math.Max(
                    GetPalindromeLength(i, i),      // odd number of chars
                    GetPalindromeLength(i, i + 1)); // even number of chars

                // Do we have a new palindrome?
                if (len > length)
                {
                    // If so, keep its starting index and the length.
                    index = i - (len - 1) / 2; // half-window
                    length = len;
                }
            }

            return s.Substring(index, length);

            // Returns at least 1 for a single character (which is a trivial palindrome).
            int GetPalindromeLength(int left, int right)
            {
                while (left >= 0 && right < s.Length && s[left] == s[right])
                {
                    --left;
                    ++right;
                }

                return right - left - 1;
            }
        }

        // The brute force solution picks all possible starting and ending positions for a substring and verifies if it is a palindrome.
        // Time complexity: O(n^3)
        // Space complexity: O(1)
        public static string GetLongestPalindromeBruteForce(string s)
        {
            if (s.Length == 1)
                return s;

            var maxPalindrome = "";

            for (var i = 0; i < s.Length; ++i)
            {
                // Set j=i to cover all cases.
                for (var j = i; j < s.Length; ++j)
                {
                    var sub = s.Substring(i, j - i + 1);
                    if (IsPalindrome1(sub) && sub.Length > maxPalindrome.Length)
                        maxPalindrome = sub;
                }
            }

            return maxPalindrome;
        }
    }
}
