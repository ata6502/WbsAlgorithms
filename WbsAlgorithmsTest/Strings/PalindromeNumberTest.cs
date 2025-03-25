using NUnit.Framework;
using WbsAlgorithms.Strings;

namespace WbsAlgorithmsTest.Strings
{
    public class PalindromeNumberTest
    {
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        [TestCase(10, false)]
        [TestCase(11, true)]
        [TestCase(12, false)]
        [TestCase(21, false)]
        [TestCase(100, false)]
        [TestCase(101, true)]
        [TestCase(121, true)]
        [TestCase(-121, false)]
        [TestCase(1221, true)]
        [TestCase(1001, true)]
        [TestCase(10001, true)]
        [TestCase(12031, false)]
        [TestCase(21012, true)]
        [TestCase(210012, true)]
        [TestCase(12321, true)]
        public void IsPalindromeTest(int number, bool expectedValue)
        {
            var isPalindrome1 = PalindromeNumber.IsPalindrome1(number);
            var isPalindrome2 = PalindromeNumber.IsPalindrome2(number);
            var isPalindromeUsingString = PalindromeNumber.IsPalindromeUsingString(number);

            Assert.That(isPalindrome1, Is.EqualTo(expectedValue));
            Assert.That(isPalindrome2, Is.EqualTo(expectedValue));
            Assert.That(isPalindromeUsingString, Is.EqualTo(expectedValue));
        }
    }
}
