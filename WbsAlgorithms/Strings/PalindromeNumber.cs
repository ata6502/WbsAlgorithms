namespace WbsAlgorithms.Strings
{
    /*
        Palindrome Number

        [leetcode]: https://leetcode.com/problems/palindrome-number/

        Determine whether an integer is a palindrome. An integer is a palindrome when it reads the same backward as forward.

        Example 1: 121 is a palindrome.
        Example 2: -121 is not a palindrome
        Example 3: 10 is not a palindrome

        Coud you solve it without converting the integer to a string?
    */

    // Time complexity: O(log10(n)). We divide the input by 10 for every iteration.
    // Space complexity: O(1)
    public class PalindromeNumber
    {
        // Solution from the website: https://leetcode.com/problems/palindrome-number/solution/
        public static bool IsPalindrome1(int n)
        {
            // Special cases.
            // - When n < 0, n is not a palindrome.
            // - If the last digit of the number is 0, in order to be a palindrome, the first 
            //   digit of the number also needs to be 0. Only 0 satisfy this property.
            if (n < 0 || (n != 0 && n % 10 == 0))
                return false;

            var reverted = 0;
            while (n > reverted)
            {
                // Revert the number.
                reverted = reverted * 10 + n % 10;

                // Decrease the order of the input number.
                n /= 10;
            }

            // When the length is an odd number, we can get rid of the middle digit by reverted/10
            // For example when the input is 12321, at the end of the while loop we get n = 12, reverted = 123,
            // since the middle digit doesn't matter in palidrome (it will always equal to itself), we can simply get rid of it.
            return (n == reverted) || (n == reverted / 10);
        }

        // My solution.
        public static bool IsPalindrome2(int n)
        {
            // Special case.
            if (n != 0 && n % 10 == 0)
                return false;

            var r = 0;
            while (r < n)
            {
                r += (n % 10);

                if (r != n)
                    n /= 10;
                if (r < n)
                    r *= 10;
            }

            return (r == n);
        }

        public static bool IsPalindromeUsingString(int n)
        {
            var s = n.ToString();

            for (var i = 0; i < s.Length / 2; ++i)
            {
                if (s[i] != s[s.Length - 1 - i])
                    return false;
            }

            return true;
        }
    }
}
