using NUnit.Framework;
using WbsAlgorithms.Arithmetics;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class FibonacciSequenceTest
    {
        [TestCase(0, "0")]
        [TestCase(1, "0 1")]
        [TestCase(2, "0 1 1")]
        [TestCase(3, "0 1 1 2")]
        [TestCase(4, "0 1 1 2 3")]
        [TestCase(5, "0 1 1 2 3 5")]
        [TestCase(6, "0 1 1 2 3 5 8")]
        [TestCase(7, "0 1 1 2 3 5 8 13")]
        [TestCase(8, "0 1 1 2 3 5 8 13 21")]
        [TestCase(9, "0 1 1 2 3 5 8 13 21 34")]
        [TestCase(10, "0 1 1 2 3 5 8 13 21 34 55")]
        [TestCase(11, "0 1 1 2 3 5 8 13 21 34 55 89")]
        [TestCase(12, "0 1 1 2 3 5 8 13 21 34 55 89 144")]
        [TestCase(13, "0 1 1 2 3 5 8 13 21 34 55 89 144 233")]
        [TestCase(14, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377")]
        [TestCase(15, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610")]
        [TestCase(16, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987")]
        [TestCase(17, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597")]
        [TestCase(18, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584")]
        [TestCase(19, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584 4181")]
        [TestCase(20, "0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584 4181 6765")]
        public void ComputeFibonacciNumberTest(int inputNumber, string expectedSequence)
        {
            var actualSequence = string.Join(' ', FibonacciSequence.ComputeSequenceUsingArray(inputNumber));

            Assert.AreEqual(expectedSequence, actualSequence);
        }
    }
}
