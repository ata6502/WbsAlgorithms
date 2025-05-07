using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Strings
{
    public class LongestSubstringWithNoRepetitions
    {
        /*
            Longest Substring Without Repeating Characters

            [LeetCode]: https://leetcode.com/problems/longest-substring-without-repeating-characters/

            Given a string, find the length of the longest substring without repeating characters.

            Example 1: Input: "abcabcbb", Output: 3 
            Example 2: Input: "bbbbb", Output: 1
            Example 3: Input: "pwwkew", Output: 3
        */

        /*
            In the brute-force solution we repeatedly check a substring to see if it has duplicated characters. 
            But if a substring s[i..j) is already checked to have no duplicate characters, we don't need to check it again.

            We can use a HashSet to store characters in the current sliding window [i, j). The sliding window represents a substring 
            with no repeated characters. Note that the HashSet keeps characters unordered. It's fine, as we don't need to keep 
            the exact substring, just the unique characters in it. A lookup using HashSet takes O(1).

            Initially j = i. Then, we slide the index j to the right. If s[j] is not in the HashSet, we slide j further until 
            we find s[j] that is already in the HashSet. Is such a case, we shrink the window on the left up to the duplicated 
            character.

            A sliding window concept:
                A window is a range of elements in the array/string which is usually defined by the start and end indices, 
                i.e. [i..j) (left-closed, right-open). A sliding window "slides" its two boundaries in the certain direction. 
                For example, if we slide [i, j) to the right by 1 element, then it becomes [i+1, j+1). If we leave the right 
                index j untact and increase the left index i, we effectively shrink the window e.g., [i+1, j)

            Complexity Analysis
                Time complexity : O(2n) = O(n). In the worst case scenario each character is visited twice by i and j.
                Space complexity : O(min(m, n)). We need O(k) space for the sliding window, where k is the size of the Set. 
                The size of the Set is upper bounded by the size of the string n and the size of the charset/alphabet m.
        */
        public static int GetSubstringLengthUsingSlidingWindow(string s)
        {
            var n = s.Length;

            // The HashSet keeps track of the encountered characters. The order of the characters
            // in the HashSet is not important.
            var chars = new HashSet<char>();

            // The output - the length of the longest substring.
            var result = 0;

            // The left and right indices of the sliding window.
            int i = 0, j = 0;

            while (i < n && j < n)
            {
                // Try to extend the window [i, j)

                // Take the character on the right boundary of the window and check if we already encountered it.
                // HashSet.Add returns true if the element is added to the HashSet or false if the element is already present.
                if (chars.Add(s[j]))
                {
                    // Expand the window to the right. After this increment the window is [i, j)
                    ++j;

                    // Calculate the new width of the window. Note that we can simply calculate j-i
                    // rather than j-i+1 because we have just incremented j.
                    result = Math.Max(result, j - i);
                }
                else
                {
                    // If yes, it means the character s[j] is already in the window. We need to "wind up" 
                    // the window from the left up to the duplicated character.

                    // Remove the left-most character from the encountered character set.
                    chars.Remove(s[i]);

                    // Shrink the window's size.
                    ++i;
                }
            }

            return result;
        }

        /*
            GetSubstringLengthUsingSlidingWindow requires at most 2n steps. We could optimize it to require only n steps. 
            Instead of using a HashSet to tell if a character has been encountered or not, we could define a mapping of the characters 
            to their indices. Then, we can skip an entire substring when we find a repeated character.

            The reason is that if s[j] is duplicated in the range [i, j) with the index j', we don't need to increase i little by little. 
            We can skip all the characters in the range [i, j'] and let i to be j' + 1 directly.

            Time complexity: O(n)
            Space complexity: O(min(m,n))
        */
        public static int GetSubstringLengthUsingSlidingWindowOptimized(string s)
        {
            var n = s.Length;

            // The Dictionary keeps track of the encountered characters as well as their indices in s.
            var chars = new Dictionary<char, int>();

            // The output - the length of the longest substring.
            var result = 0;

            for (int j = 0, i = 0; j < n; ++j)
            {
                var c = s[j];

                // Check if we already encountered the character s[j].
                if (chars.TryGetValue(c, out var index))
                {
                    // If so, skip the entire substring up to the duplicated character.
                    // We don't need to increase i by 1 because we keep the increased
                    // index in the dictionary (because of the open right-range).
                    // Also, Math.Max(index, i) prevents the window from sliding back to the left.
                    i = Math.Max(index, i);

                    // Increase the window's right index to keep the open range.
                    chars[c] = j + 1;
                }
                else
                {
                    // Increase the window's right index to keep the open range.
                    chars.Add(c, j + 1);
                }

                // j-i+1 is the length of the current substring without duplicates (also the window's width).
                result = Math.Max(result, j - i + 1);
            }

            return result;
        }

        /*
            In GetSubstringLengthUsingBruteForce we utilize a helper function AreCharsInSubstringUnique
            which returns true if the characters in the substring are unique, otherwise false. 
            We iterate through all the possible substrings of the given string s and call the function AreCharsInSubstringUnique.
            If it turns out to be true, then we update our answer.

            Complexity Analysis:

            Time complexity: O(n^3)

                To verify if characters within the index range [i, j) are unique, we need to scan all of them. 
                It costs O(j - i) time. For a given i: Sum[j = i+1..n] of O(j-i) - the inner loop in GetSubstringLengthUsingBruteForce.

                Thus, the sum of all time consumption is (n == s.Length):
                O( Sum[i = 0..n-1] of Sum[j = i+1..n] of (j-i) ) = O( Sum[i = 0..n-1] of (1+n-i)(n-i)/2 ) = O(n^3)
        
            Space complexity: O(min(n, m))

                We need O(k) space for checking if a substring has no duplicate characters, 
                where k is the size of the Set. The size of the Set is upper bounded by the size of the string n and the size 
                of the charset/alphabet m.
        */
        public static int GetSubstringLengthUsingBruteForce(string s)
        {
            var max = 0;

            // Enumerate through the entire string.
            for (var i = 0; i < s.Length; ++i)
            {
                // Form a substring s[i..j)
                for (var j = i + 1; j <= s.Length; ++j)
                {
                    // Check if the substring has unique characters.
                    if (!AreCharsInSubstringUnique(s, i, j))
                        break;

                    // If so, update our answer. The j-i value is the length
                    // of the substring. Because j is exclusive, we don't need to
                    // add one i.e., j-i+1.
                    max = Math.Max(max, j - i);
                }
            }

            return max;
        }

        // start - the beginning of the substring (inclusive)
        // stop - the end of the substring (exclusive)
        private static bool AreCharsInSubstringUnique(string s, int start, int stop)
        {
            // Use a set collection to keep characters we already encountered.
            var chars = new HashSet<char>();

            for (int i = start; i < stop; ++i)
            {
                // Get a character from the string.
                char c = s[i];

                // Check if the set already contains the character.
                if (chars.TryGetValue(c, out var _))
                    return false;

                // Keep the character in the set.
                chars.Add(c);
            }

            // We iterated over the entire string and we did not find any duplicates.
            return true;
        }
    }
}
