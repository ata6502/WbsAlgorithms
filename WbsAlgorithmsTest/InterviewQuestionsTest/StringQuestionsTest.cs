using NUnit.Framework;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class StringQuestionsTest
    {
        [TestCase("a", "a")]
        [TestCase("abbcb", "abbcb")]
        [TestCase("abbb", "abbb")]
        [TestCase("abbbb", "a1b4")]
        [TestCase("aabbbcc", "a2b3c2")]
        [TestCase("aaaaa", "a5")]
        [TestCase("aaaaab", "a5b1")]
        [TestCase("aabcccccaaa", "a2b1c5a3")]
        public void CompressStringTest(string inputString, string expectedCompressedString)
        {
            Assert.That(StringQuestions.CompressString(inputString), Is.EqualTo(expectedCompressedString));
            Assert.That(StringQuestions.CompressStringWithCheckingNext(inputString), Is.EqualTo(expectedCompressedString));
        }

        [TestCase("pale", "ple", true)]
        [TestCase("ple", "pale", true)]
        [TestCase("pale", "pales", true)]
        [TestCase("pale", "bale", true)]
        [TestCase("abc", "aabc", true)]
        [TestCase("abc", "abbc", true)]
        [TestCase("abc", "abcc", true)]
        [TestCase("abc", "abbcc", false)]
        [TestCase("pale", "bake", false)]
        [TestCase("pale", "pke", false)]
        public void AreOneEditAwayTest(string firstString, string secondString, bool areOneEditAway)
        {
            Assert.That(StringQuestions.AreOneEditAway(firstString, secondString), Is.EqualTo(areOneEditAway));
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
            Assert.That(StringQuestions.IsPalindromePermutation(inputString), Is.EqualTo(isPalindromePermutation));
        }

        [TestCase("a   ", 2, "a%20")]
        [TestCase(" a  ", 2, "%20a")]
        [TestCase("a a  ", 3, "a%20a")]
        [TestCase("abc def  ghi   jkl            ", 18, "abc%20def%20%20ghi%20%20%20jkl")]
        [TestCase("a abc d       ", 8, "a%20abc%20d%20")]
        [TestCase("a     a          ", 7, "a%20%20%20%20%20a")]
        public void URLifyTest(string inputString, int length, string expectedURLifiedString)
        {
            Assert.That(StringQuestions.URLify(inputString, length), Is.EqualTo(expectedURLifiedString));
        }

        [TestCase("a", "a", true)]
        [TestCase("ab", "ab", true)]
        [TestCase("ab", "ba", true)]
        [TestCase("abc", "abc", true)]
        [TestCase("abc", "acb", true)]
        [TestCase("abc", "bac", true)]
        [TestCase("abc", "bca", true)]
        [TestCase("abc", "cab", true)]
        [TestCase("abc", "cba", true)]
        [TestCase("a", "b", false)]
        [TestCase("ab", "bc", false)]
        [TestCase("abc", "bcd", false)]
        [TestCase("aba", "baa", true)]
        [TestCase("aba", "aab", true)]
        [TestCase("ab", "abb", false)]
        public void CheckPermutationTest(string inputString1, string inputString2, bool isPermutation)
        {
            Assert.That(StringQuestions.CheckPermutation(inputString1, inputString2), Is.EqualTo(isPermutation));
            Assert.That(StringQuestions.CheckPermutationConstrained(inputString1, inputString2), Is.EqualTo(isPermutation));
            Assert.That(StringQuestions.CheckPermutationUsingSorting(inputString1, inputString2), Is.EqualTo(isPermutation));
            Assert.That(StringQuestions.CheckPermutationUsingSortingAndLinq(inputString1, inputString2), Is.EqualTo(isPermutation));
        }

        [TestCase("", true)]
        [TestCase("a", true)]
        [TestCase("ab", true)]
        [TestCase("abc", true)]
        [TestCase("aa", false)]
        [TestCase("aba", false)]
        [TestCase("abb", false)]
        [TestCase("bba", false)]
        [TestCase("bbb", false)]
        [TestCase("abcd", true)]
        [TestCase("abcb", false)]
        public void IsUniqueTest(string inputString, bool isUnique)
        {
            Assert.That(StringQuestions.IsUniqueUsingHashSet(inputString), Is.EqualTo(isUnique));
            Assert.That(StringQuestions.IsUniqueBruteForce(inputString), Is.EqualTo(isUnique));
            Assert.That(StringQuestions.IsUniqueConstrained(inputString), Is.EqualTo(isUnique));
            Assert.That(StringQuestions.IsUniqueConstrainedUsingBitArray(inputString), Is.EqualTo(isUnique));
        }

        [TestCase("a", "a", TestName = "Permutations of A")]
        [TestCase("ab", "ab,ba", TestName = "Permutations of AB")]
        [TestCase("abc", "abc,acb,bac,bca,cab,cba", TestName = "Permutations of ABC")]
        [TestCase("abcd", "abcd,abdc,acbd,acdb,adbc,adcb,bacd,badc,bcad,bcda,bdac,bdca,cabd,cadb,cbad,cbda,cdab,cdba,dabc,dacb,dbac,dbca,dcab,dcba", TestName = "Permutations of ABCD")]
        public void GetPermutationsTest(string inputString, string expectedPermutations)
        {
            var permutations = StringQuestions.GetPermutations(inputString);
            var actualPermutations = string.Join(',', permutations);
            Assert.That(actualPermutations, Is.EqualTo(expectedPermutations));
        }

        [TestCase("", "")]
        [TestCase("@", "")]
        [TestCase("#$%^&*", "")]
        [TestCase("a", "a")]
        [TestCase("#A", "A")]
        [TestCase("#A#", "A")]
        [TestCase("a #1", "a1")]
        [TestCase("   !a1", "a1")]
        [TestCase("wup*iOp|", "wupiOp")]
        [TestCase("A aQ,...1?", "AaQ1")]
        [TestCase("34$%", "34")]
        [TestCase("3gy7^yy    -", "3gy7yy")]
        public void RemoveNonAlphanumericCharactersTest(string inputString, string expectedString)
        {
            Assert.That(StringQuestions.RemoveNonAlphanumericCharacters(inputString), Is.EqualTo(expectedString));
        }

        [TestCase("a", 'a', 1)]
        [TestCase("ab", 'a', 1)]
        [TestCase("ab", 'b', 1)]
        [TestCase("aba", 'a', 2)]
        [TestCase("aba", 'b', 1)]
        [TestCase("bab", 'a', 1)]
        [TestCase("bab", 'b', 2)]
        [TestCase("ccccc", 'c', 5)]
        [TestCase("cccccddc", 'd', 2)]
        [TestCase("abracadabra", 'a', 5)]
        [TestCase("abracadabra", 'b', 2)]
        [TestCase("abracadabra", 'r', 2)]
        public void CountCharacterTest(string inputString, char inputCharacter, int expectedCount)
        {
            Assert.That(StringQuestions.CountCharacter(inputString, inputCharacter), Is.EqualTo(expectedCount));
            Assert.That(StringQuestions.CountCharacterUsingRegEx(inputString, inputCharacter), Is.EqualTo(expectedCount));
        }

        [TestCase("a", 1, 0)]
        [TestCase("b", 0, 1)]
        [TestCase("ab", 1, 1)]
        [TestCase("ba", 1, 1)]
        [TestCase("bic", 1, 2)]
        [TestCase("ooooo", 5, 0)]
        [TestCase("Abracadabra", 5, 6)]
        [TestCase("alphaNUMERIC", 5, 7)]
        [TestCase("whitespace", 4, 6)]
        [TestCase("pattern", 2, 5)]
        [TestCase("echidna", 3, 4)]
        public void CountVowelsAndConsonantsTest(string inputString, int expectedVowelCount, int expectedConsonantCount)
        {
            var (actualVowelCount, actualConsonantCount) = StringQuestions.CountVowelsAndConsonants(inputString);
            Assert.That(actualVowelCount, Is.EqualTo(expectedVowelCount));
            Assert.That(actualConsonantCount, Is.EqualTo(expectedConsonantCount));
        }

        [TestCase("a", new char[] { })]
        [TestCase("abb", new char[] { 'b' })]
        [TestCase("abc", new char[] { })]
        [TestCase("aacbb", new char[] { 'a', 'b' })]
        [TestCase("abbcfccdeafb", new char[] { 'a', 'b', 'c', 'f' })]
        [TestCase("abcdefghte", new char[] { 'e' })]
        public void FindDuplicatedCharactersTest(string inputString, char[] expectedDuplicates)
        {
            Assert.That(StringQuestions.FindDuplicatedCharactersUsingDictionary(inputString), Is.EqualTo(expectedDuplicates).AsCollection);
            Assert.That(StringQuestions.FindDuplicatedCharactersUsingLinq(inputString), Is.EqualTo(expectedDuplicates).AsCollection);
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
            Assert.That(StringQuestions.IsPalindrome(inputString), Is.EqualTo(isPalindrome));
        }

        [TestCase("a", "a")]
        [TestCase("ab", "ba")]
        [TestCase("abc", "cba")]
        [TestCase("abcd", "dcba")]
        [TestCase("abcde", "edcba")]
        public void ReverseStringTest(string inputString, string expectedResult)
        {
            Assert.That(StringQuestions.ReverseString(inputString), Is.EqualTo(expectedResult));
            Assert.That(StringQuestions.ReverseStringWithoutUsingArrayReverse(inputString), Is.EqualTo(expectedResult));
            Assert.That(StringQuestions.ReverseStringUsingXorSwapping(inputString), Is.EqualTo(expectedResult));
        }
    }
}
