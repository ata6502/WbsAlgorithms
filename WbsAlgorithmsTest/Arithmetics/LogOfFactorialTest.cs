using NUnit.Framework;
using System;
using WbsAlgorithms.Arithmetics;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class LogOfFactorialTest
    {
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(2, 0.69314718055995)]
        [TestCase(3, 1.79175946922806)]
        [TestCase(4, 3.17805383034795)]
        [TestCase(5, 4.78749174278205)]
        [TestCase(6, 6.57925121201010)]
        [TestCase(7, 8.52516136106541)]
        [TestCase(8, 10.60460290274525)]
        [TestCase(9, 12.80182748008147)]
        [TestCase(10, 15.10441257307552)]
        public void ComputeLogOfFactorialTest(int inputNumber, double expectedValue)
        {
            var actualValue = LogOfFactorial.Compute(inputNumber);

            Assert.AreEqual(expectedValue, actualValue, 0.00000000000001);
        }

        [Test]
        public void InvalidArgumentTest()
        {
            Assert.Throws<ArgumentException>(() => LogOfFactorial.Compute(-1));
        }
    }
}
