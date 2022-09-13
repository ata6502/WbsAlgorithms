using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WbsAlgorithms.Arithmetic;

namespace WbsAlgorithmsTest.Arithmetic
{
    [TestFixture]
    public class StringArithmeticHelperTest
    {
        [TestCase("0", "0", "0", "0")]
        [TestCase("0", "1", "0", "1")]
        [TestCase("1", "0", "1", "0")]
        [TestCase("0", "10", "00", "10")]
        [TestCase("1", "10", "01", "10")]
        [TestCase("10", "1", "10", "01")]
        [TestCase("123", "1", "0123", "0001")]
        [TestCase("123", "321", "0123", "0321")]
        [TestCase("12345", "22", "00012345", "00000022")]
        public void MakeLengthEqualAndPowerOfTwoTest(string x, string y, string paddedX, string paddedY)
        {
            var result = StringArithmeticHelper.PadLeftZeros(x, y);

            Assert.AreEqual(paddedX, result.x);
            Assert.AreEqual(paddedY, result.y);
        }

        [TestCase("1", "2", "3")]
        [TestCase("8", "3", "11")]
        [TestCase("2", "12", "14")]
        [TestCase("13", "5", "18")]
        [TestCase("90", "12", "102")]
        [TestCase("99", "2", "101")]
        [TestCase("100", "450", "550")]
        [TestCase("998", "5", "1003")]
        public void AddNumbersTest(string x, string y, string expectedResult)
        {
            var result = StringArithmeticHelper.AddNumbers(x, y);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("2", "1", "1")]
        [TestCase("8", "3", "5")]
        [TestCase("12", "2", "10")]
        [TestCase("13", "5", "8")]
        [TestCase("90", "12", "78")]
        [TestCase("99", "2", "97")]
        [TestCase("450", "100", "350")]
        [TestCase("998", "5", "993")]
        public void SubtractNumbersTest(string x, string y, string expectedResult)
        {
            var result = StringArithmeticHelper.SubtractNumbers(x, y);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
