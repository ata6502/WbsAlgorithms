using NUnit.Framework;
using WbsAlgorithms.Strings;

namespace WbsAlgorithmsTest.Strings
{
    [TestFixture]
    public class PalindromeAlgorithmsTest
    {
        [TestCase("a", true)]
        [TestCase("ab", false)]
        [TestCase("aa", true)]
        [TestCase("abb", false)]
        [TestCase("aba", true)]
        [TestCase("aaa", true)]
        [TestCase("abca", false)]
        [TestCase("abba", true)]
        [TestCase("aaaa", true)]
        [TestCase("abcad", false)]
        [TestCase("bbcba", false)]
        [TestCase("abbba", true)]
        [TestCase("abcba", true)]
        [TestCase("aaaaa", true)]
        [TestCase("abcbaa", false)]
        [TestCase("abccba", true)]
        [TestCase("aaaaaa", true)]
        public void IsPalindromeTest(string inputString, bool isPalindrome)
        {
            Assert.That(PalindromeAlgorithms.IsPalindrome1(inputString), Is.EqualTo(isPalindrome));
            Assert.That(PalindromeAlgorithms.IsPalindrome2(inputString), Is.EqualTo(isPalindrome));
            Assert.That(PalindromeAlgorithms.IsPalindrome3(inputString), Is.EqualTo(isPalindrome));
        }

        [TestCase("a", true)]
        [TestCase("ab", false)]
        [TestCase("aba", true)]
        [TestCase("aab", true)]
        [TestCase("baa", true)]
        [TestCase("abc", false)]
        [TestCase("abcd", false)]
        [TestCase("abccab", true)]
        [TestCase("cacabb", true)]
        [TestCase("cacabbd", true)]
        [TestCase("cdadcabbd", true)] // abcdddcba
        [TestCase("cdadcabbda", false)] // abcdddcbaa
        public void IsPalindromePermutationTest(string inputString, bool isPalindromePermutation)
        {
            Assert.That(PalindromeAlgorithms.IsPalindromePermutation(inputString), Is.EqualTo(isPalindromePermutation));
        }

        [TestCase("a", "a")]
        [TestCase("bb", "bb")]
        [TestCase("abba", "abba")]
        [TestCase("cbbd", "bb")]
        [TestCase("babad", "bab")] // "aba" is also a valid answer
        [TestCase("caba", "aba")]
        [TestCase("bacab", "bacab")]
        [TestCase("bacccab", "bacccab")]
        [TestCase("baaaabad", "baaaab")]
        [TestCase("aabbbcccccdd", "ccccc")]
        [TestCase("bafabbaffab", "fabbaf")]
        [TestCase("abacdfgdcaba", "aba")]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeee", "eeeeeeeeeeeeeeeeeeeeeeeeeeee")]
        [TestCase("wqpapqrtbniiookooiivcbdnmmmkkmmml", "iiookooii")]
        [TestCase("geeksskeeg", "geeksskeeg")]
        [TestCase("geeks", "ee")]
        [TestCase("abababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababa", "abababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababa")]
        public void GetLongestPalindromeTest(string s, string expectedPalindrome)
        {
            var palindromeBruteForce = PalindromeAlgorithms.GetLongestPalindromeBruteForce(s);
            var palindrome = PalindromeAlgorithms.GetLongestPalindrome(s);

            Assert.That(palindromeBruteForce, Is.EqualTo(expectedPalindrome));
            Assert.That(palindrome, Is.EqualTo(expectedPalindrome));
        }
    }
}
