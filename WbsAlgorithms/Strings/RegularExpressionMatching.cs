using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WbsAlgorithms.Strings
{
    /*
        Regular Expression Matching (hard)

        [leetcode]: https://leetcode.com/problems/regular-expression-matching/

        Given an input string s and a pattern p, implement regular expression matching with support for '.' and '*'.

        '.' matches any single character.
        '*' matches zero or more of the preceding element.

        The matching should cover the entire input string (not partial).

        Input parameters:
        s - an input string
        p - a regular expression pattern

        Note:
        s could be empty or contain lowercase letters a-z
        p could be empty or contain lowercase letters a-z, and characters . or *

        Example 1: 
        Input: s = "aa", p = "a"
        Output: false
        Explanation: "a" does not match the entire string "aa".

        Example 2:
        Input: s = "aa", p = "a*"
        Output: true
        Explanation: '*' means zero or more of the preceding element, 'a'. Therefore, by repeating 'a' once, it becomes "aa".

        Example 3:
        Input: s = "ab", p = ".*"
        Output: true
        Explanation: ".*" means "zero or more * of any character ."

        Example 4:
        Input: s = "aab", p = "c*a*b"
        Output: true
        Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore, it matches "aab".

        Example 5:
        Input: s = "mississippi", p = "mis*is*p*."
        Output: false
    */
    public class RegularExpressionMatching
    {
        /*
            Recursive Approach
            ------------------

            If there was no Kleene stars (the * wildcard character for regular expressions), the problem would be easier 
            - we would simply check from left to right if each character of the text matches the pattern.

            When a star is present, we may need to check many different suffixes of the text and see if they match the rest 
            of the pattern. A recursive solution is a straightforward way to represent this relationship. 

            If a star is present, it will be in the second position p[1]. Then, we may ignore this part of the pattern, 
            or delete a matching character in the text. If we have a match on the remaining strings after any of these operations, 
            then the initial input matched.

            Time Complexity: O((T+P)2^(T+p/2)) where T = s.Length, P = p.Length
            Space Complexity: If memory is not freed, the space complexity is also O((T+P)2^(T+p/2))
        */
        public static bool IsMatchRecursive(string s, string p)
        {
            if (string.IsNullOrEmpty(p))
                return string.IsNullOrEmpty(s);

            bool first_match = (!string.IsNullOrEmpty(s) &&
                               (p[0] == s[0] || p[0] == '.'));

            if (p.Length >= 2 && p[1] == '*')
            {
                return (IsMatchRecursive(s, p.Substring(2)) ||
                       (first_match && IsMatchRecursive(s.Substring(1), p)));
            }
            else
            {
                return first_match && IsMatchRecursive(s.Substring(1), p.Substring(1));
            }
        }

        /*
            Dynamic Programming
            -------------------

            As the problem has an *optimal substructure*, it is natural to cache intermediate results. We ask the question dp(i,j):
            does s[i:] and p[j:] match? We can describe our answer in terms of answers to questions involving smaller strings.

            We proceed with the same recursion as above except, because calls will only ever be made to match(text[i:], pattern[j:]), 
            we use dp(i,j) to handle those calls instead, saving us expensive string-building operations and allowing us to cache 
            the intermediate results.
        */
        public static bool IsMatchDynamicBottomUp(string s, string p)
        {
            bool[,] dp = new bool[s.Length + 1, p.Length + 1];
            dp[s.Length, p.Length] = true;

            for (int i = s.Length; i >= 0; i--)
            {
                for (int j = p.Length - 1; j >= 0; j--)
                {
                    bool first_match = (i < s.Length &&
                                       (p[j] == s[i] || p[j] == '.'));
                    if (j + 1 < p.Length && p[j + 1] == '*')
                    {
                        dp[i, j] = dp[i, j + 2] || first_match && dp[i + 1, j];
                    }
                    else
                    {
                        dp[i, j] = first_match && dp[i + 1, j + 1];
                    }
                }
            }
            return dp[0, 0];
        }

        private static bool?[,] _memo;

        public static bool IsMatchDynamicTopDown(string s, string p)
        {
            _memo = new bool?[s.Length + 1, p.Length + 1];
            return dp(0, 0, s, p);
        }

        private static bool dp(int i, int j, string text, string pattern)
        {
            if (_memo[i, j] != null)
                return _memo[i, j] == true;

            bool ans;
            if (j == pattern.Length)
            {
                ans = (i == text.Length);
            }
            else
            {
                bool first_match = (i < text.Length &&
                                   (pattern[j] == text[i] ||
                                    pattern[j] == '.'));

                if (j + 1 < pattern.Length && pattern[j + 1] == '*')
                {
                    ans = (dp(i, j + 2, text, pattern) ||
                           first_match && dp(i + 1, j, text, pattern));
                }
                else
                {
                    ans = first_match && dp(i + 1, j + 1, text, pattern);
                }
            }

            _memo[i, j] = ans;
            return ans;
        }
    }
}
