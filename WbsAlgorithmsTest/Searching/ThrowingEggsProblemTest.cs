using NUnit.Framework;
using WbsAlgorithms.Searching;

namespace WbsAlgorithmsTest.Searching
{
    [TestFixture]
    public class ThrowingEggsProblemTest
    {
        [TestCase("x", -1, TestName = "1Floor_None")]
        [TestCase(".", 1, TestName = "1Floor")]
        [TestCase("xx", -1, TestName = "2Floors_None")]
        [TestCase(".x", 1, TestName = "2Floors_1")]
        [TestCase("..", 2, TestName = "2Floors_2")]
        [TestCase("xxx", -1, TestName = "3Floors_None")]
        [TestCase(".xx", 1, TestName = "3Floors_1")]
        [TestCase("..x", 2, TestName = "3Floors_2")]
        [TestCase("...", 3, TestName = "3Floors_3")]
        [TestCase("xxxxxxxxxxxx", -1, TestName = "12Floors_None")]
        [TestCase(".xxxxxxxxxxx", 1, TestName = "12Floors_1")]
        [TestCase("..xxxxxxxxxx", 2, TestName = "12Floors_2")]
        [TestCase("...xxxxxxxxx", 3, TestName = "12Floors_3")]
        [TestCase("....xxxxxxxx", 4, TestName = "12Floors_4")]
        [TestCase(".....xxxxxxx", 5, TestName = "12Floors_5")]
        [TestCase("......xxxxxx", 6, TestName = "12Floors_6")]
        [TestCase(".......xxxxx", 7, TestName = "12Floors_7")]
        [TestCase("........xxxx", 8, TestName = "12Floors_8")]
        [TestCase(".........xxx", 9, TestName = "12Floors_9")]
        [TestCase("..........xx", 10, TestName = "12Floors_10")]
        [TestCase("...........x", 11, TestName = "12Floors_11")]
        [TestCase("............", 12, TestName = "12Floors_12")]
        public void GetHighestFloorTest(string floors, int expectedFloor)
        {
            var actualFloor = ThrowingEggsProblem.GetHighestFloor(ConvertToBoolArray(floors));
            Assert.AreEqual(expectedFloor, actualFloor);
        }

        private bool[] ConvertToBoolArray(string s)
        {
            var a = new bool[s.Length];
            for (var i = 0; i < s.Length; ++i)
                a[i] = s[i] == '.';
            return a;
        }
    }
}
