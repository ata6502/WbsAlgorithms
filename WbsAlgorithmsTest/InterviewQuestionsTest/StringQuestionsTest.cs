using NUnit.Framework;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class StringQuestionsTest
    {
        [TestCase("a", "")]
        [TestCase("abb", "b")]
        [TestCase("abc", "")]
        [TestCase("aacbb", "ab")]
        [TestCase("abbcfccdeafb", "abcf")]
        [TestCase("abcdefghte", "e")]
        public void FindDuplicatedCharactersTest(string inputString, string expectedDuplicates)
        {
            Assert.AreEqual(expectedDuplicates, StringQuestions.FindDuplicatedCharactersUsingLinq(inputString));
            Assert.AreEqual(expectedDuplicates, StringQuestions.FindDuplicatedCharactersUsingDictionary(inputString));
        }

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
            Assert.AreEqual(isPalindrome, StringQuestions.IsPalindrome(inputString));
        }

        [TestCase("a", "a")]
        [TestCase("ab", "ba")]
        [TestCase("abc", "cba")]
        [TestCase("abcd", "dcba")]
        [TestCase("abcde", "edcba")]
        public void ReverseStringTest(string inputString, string expectedResult)
        {
            Assert.AreEqual(expectedResult, StringQuestions.ReverseString(inputString));
            Assert.AreEqual(expectedResult, StringQuestions.ReverseStringWithoutUsingArrayReverse(inputString));
            Assert.AreEqual(expectedResult, StringQuestions.ReverseStringUsingXorSwapping(inputString));
        }
    }
}
