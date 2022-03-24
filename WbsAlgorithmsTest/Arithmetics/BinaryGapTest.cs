using NUnit.Framework;
using WbsAlgorithms.Arithmetics;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class BinaryGapTest
    {
        [TestCase(1, 0)]
        [TestCase(5, 1)]
        [TestCase(6, 0)]
        [TestCase(9, 2)]
        [TestCase(11, 1)]
        [TestCase(15, 0)]
        [TestCase(16, 0)]
        [TestCase(20, 1)]
        [TestCase(32, 0)]
        [TestCase(328, 2)]
        [TestCase(529, 4)]
        [TestCase(1024, 0)]
        [TestCase(1041, 5)]
        [TestCase(805306373, 25)]
        [TestCase(1376796946, 5)]
        [TestCase(2147483647, 0)]
        public void GetMaxBinaryGapTest(int inputValue, int expectedMaxBinaryGap)
        {
            var actualMaxBinaryGap = BinaryGap.GetMaxBinaryGap(inputValue);

            Assert.AreEqual(expectedMaxBinaryGap, actualMaxBinaryGap);
        }
    }
}
