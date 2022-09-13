using NUnit.Framework;
using WbsAlgorithms.Arithmetic;

namespace WbsAlgorithmsTest.Arithmetic
{
    [TestFixture]
    public class BinaryRepresentationTest
    {
        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(2, "10")]
        [TestCase(3, "11")]
        [TestCase(4, "100")]
        [TestCase(5, "101")]
        [TestCase(50, "110010")]
        [TestCase(255, "11111111")]
        [TestCase(256, "100000000")]
        public void GetBinaryUsingStackTest(int inputNumber, string expectedBinary)
        {
            var actualBinary1 = BinaryRepresentation.GetBinary(inputNumber);
            var actualBinary2 = BinaryRepresentation.GetBinaryUsingStack(inputNumber);

            Assert.AreEqual(expectedBinary, actualBinary1);
            Assert.AreEqual(expectedBinary, actualBinary2);
        }
    }
}
