using NUnit.Framework;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class StringQuestionsTest
    {
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
