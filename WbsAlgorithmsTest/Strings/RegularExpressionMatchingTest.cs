using NUnit.Framework;
using WbsAlgorithms.Strings;

namespace WbsAlgorithmsTest.Strings
{
    [TestFixture]
    public class RegularExpressionMatchingTest
    {
        [TestCase("aa", "a", false)]
        [TestCase("aa", "a*", true)]
        [TestCase("ab", ".*", true)]
        [TestCase("abc", ".*c", true)]
        [TestCase("ab", ".*c", false)]
        [TestCase("aab", "c*a*b", true)]
        [TestCase("aaa", "a*a", true)]
        [TestCase("mississippi", "mis*is*p*.", false)]
        public void IsMatchTest(string inputString, string inputPattern, bool expectedResult)
        {
            var resultRecursive = RegularExpressionMatching.IsMatchRecursive(inputString, inputPattern);
            var resultDynamicBottomUp = RegularExpressionMatching.IsMatchDynamicBottomUp(inputString, inputPattern);
            var resultDynamicTopDown = RegularExpressionMatching.IsMatchDynamicTopDown(inputString, inputPattern);

            Assert.That(resultRecursive, Is.EqualTo(expectedResult));
            Assert.That(resultDynamicBottomUp, Is.EqualTo(expectedResult));
            Assert.That(resultDynamicTopDown, Is.EqualTo(expectedResult));
        }
    }
}
