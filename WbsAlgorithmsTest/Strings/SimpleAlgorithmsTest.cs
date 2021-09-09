using NUnit.Framework;
using WbsAlgorithms.Strings;

namespace WbsAlgorithmsTest.Strings
{
    [TestFixture]
    public class SimpleAlgorithmsTest
    {
        [TestCase("a", "a", true, TestName = "OneChar")]
        [TestCase("ab", "ab", true, TestName = "TwoChars1")]
        [TestCase("ab", "ba", true, TestName = "TwoChars2")]
        [TestCase("ab", "aa", false, TestName = "TwoChars3")]
        [TestCase("abc", "abc", true, TestName = "ThreeChars1")]
        [TestCase("abc", "bca", true, TestName = "ThreeChars2")]
        [TestCase("abc", "cab", true, TestName = "ThreeChars3")]
        [TestCase("abc", "cac", false, TestName = "ThreeChars4")]
        [TestCase("aabbcc", "abbcca", true, TestName = "SixChars1")]
        [TestCase("abbcca", "bbccaa", true, TestName = "SixChars2")]
        [TestCase("bbccaa", "ccaabb", true, TestName = "SixChars3")]
        [TestCase("ACTGACG", "TGACGAC", true, TestName = "GenomicSequence1")]
        [TestCase("GACCTGACTCC", "ACTCCGACCTG", true, TestName = "GenomicSequence2")]
        [TestCase("GTCCGGTACT", "CTGTCCGGTA", true, TestName = "GenomicSequence3")]
        [TestCase("CTTAGCTTA", "AGCTTACTT", true, TestName = "GenomicSequence4")]
        [TestCase("TAGACTA", "ACTATAG", true, TestName = "GenomicSequence5")]
        public void AreStringsCircularRotationsTest(string s1, string s2, bool expectedResult)
        {
            var actualResult = SimpleAlgorithms.AreStringsCircularRotations(s1, s2);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
