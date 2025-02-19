using NUnit.Framework;
using WbsAlgorithms.Arithmetic;

namespace WbsAlgorithmsTest.Arithmetic
{
    public class IntegerReversalTest
    {
        [TestCase(1, 1)]
        [TestCase(-1, -1)]
        [TestCase(12, 21)]
        [TestCase(123, 321)]
        [TestCase(-123, -321)]
        [TestCase(120, 21)]
        [TestCase(1534236469, 0)] // overflow
        [TestCase(int.MinValue, 0)] // -2147483648; -(-2147483648) is still -2147483648 because of overflow
        [TestCase(-2147483647, 0)]
        [TestCase(int.MaxValue, 0)] // 2147483647; overflow
        [TestCase(2147483641, 1463847412)]
        public void ReverseTest(int inputInteger, int expectedReversedInteger)
        {
            var actualReversedInteger = IntegerReversal.Reverse(inputInteger);
            Assert.That(actualReversedInteger, Is.EqualTo(expectedReversedInteger));

            var actualReversedIntegerWithoutChecked = IntegerReversal.ReverseWithoutChecked(inputInteger);
            Assert.That(actualReversedIntegerWithoutChecked, Is.EqualTo(expectedReversedInteger));

            var actualReversedIntegerUsingString = IntegerReversal.ReverseUsingString(inputInteger);
            Assert.That(actualReversedIntegerUsingString, Is.EqualTo(expectedReversedInteger));
        }
    }
}
