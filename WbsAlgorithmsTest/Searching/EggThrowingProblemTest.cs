using NUnit.Framework;
using System.Collections;
using WbsAlgorithms.Searching;

namespace WbsAlgorithmsTest.Searching
{
    [TestFixture]
    public class EggThrowingProblemTest
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
        [TestCase("xxxx", -1, TestName = "4Floors_None")]
        [TestCase(".xxx", 1, TestName = "4Floors_1")]
        [TestCase("..xx", 2, TestName = "4Floors_2")]
        [TestCase("...x", 3, TestName = "4Floors_3")]
        [TestCase("....", 4, TestName = "4Floors_4")]
        [TestCase("xxxxx", -1, TestName = "5Floors_None")]
        [TestCase(".xxxx", 1, TestName = "5Floors_1")]
        [TestCase("..xxx", 2, TestName = "5Floors_2")]
        [TestCase("...xx", 3, TestName = "5Floors_3")]
        [TestCase("....x", 4, TestName = "5Floors_4")]
        [TestCase(".....", 5, TestName = "5Floors_5")]
        [TestCase("......xxxxx", 6, TestName = "11Floors_6")]
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
        [TestCase("..................xxxxx", 18, TestName = "23Floors_18")]
        [TestCase("....xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", 4, TestName = "37Floors_4")]
        [TestCase(".......xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", 7, TestName = "37Floors_7")]
        [TestCase("............xxxxxxxxxxxxxxxxxxxxxxxxx", 12, TestName = "37Floors_12")]
        [TestCase(".................xxxxxxxxxxxxxxxxxxxx", 17, TestName = "37Floors_17")]
        [TestCase("........................xxxxxxxxxxxxx", 24, TestName = "37Floors_24")]
        [TestCase("...............................xxxxxx", 31, TestName = "37Floors_31")]
        [TestCase("....................................x", 36, TestName = "37Floors_36")]
        [TestCase(".....................................", 37, TestName = "37Floors_37")]
        public void GetHighestFloorTest(string floors, int expectedFloor)
        {
            var actualFloor = EggThrowingProblem.GetHighestFloor(ConvertToBitArray(floors));
            Assert.That(actualFloor, Is.EqualTo(expectedFloor));

            var actualFloorFaster = EggThrowingProblem.GetHighestFloorFaster(ConvertToBitArray(floors));
            Assert.That(actualFloorFaster, Is.EqualTo(expectedFloor));
        }

        private BitArray ConvertToBitArray(string s)
        {
            var a = new BitArray(s.Length);
            for (var i = 0; i < s.Length; ++i)
                a[i] = s[i] == '.';
            return a;
        }
    }
}
