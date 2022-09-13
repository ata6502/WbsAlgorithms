using NUnit.Framework;
using WbsAlgorithms.Arithmetic;

namespace WbsAlgorithmsTest.Arithmetic
{
    [TestFixture]
    public class IntegerMultiplicationTest
    {
        [TestCase("100056", "7001", "700492056")]
        [TestCase("13262316301400003014000013262316", "13262316301400003014000013262316", "175889033678380255587203016617268446432164745241023889025683856")]
        [TestCase("134", "46", "6164")]
        [TestCase("16", "98", "1568")]
        [TestCase("30140000", "13262316", "399726204240000")]
        [TestCase("30145673", "13263451", "399835656697523")]
        [TestCase("3141592653589169399375105820974944592", "2718281828459045234324352553455360287471352662497757247093699959574966967627", "8539734222671871291753529514643246274797725721593773924247998764362368043642794208545797374895871952806582723184")]
        [TestCase("3141592653589793238462643383279502884197169399375105820974944592", "2718281828459045235360287471352662497757247093699959574966967627", "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
        [TestCase("3223", "776567", "2502875441")]
        [TestCase("4", "8", "32")]
        [TestCase("5678", "1234", "7006652")]
        [TestCase("7900", "8945", "70665500")]
        [TestCase("8", "999999", "7999992")]
        [TestCase("98975", "231", "22863225")]
        public void MultiplyTest(string x, string y, string result)
        {
            var resultSimple = IntegerMultiplication.SimpleMultiply(x, y);
            var resultRecursive = IntegerMultiplication.RecursiveMultiply(x, y);
            var resultKaratsuba = IntegerMultiplication.KaratsubaMultiply(x, y);

            Assert.AreEqual(result, resultSimple, "Simple algorithm");
            Assert.AreEqual(result, resultRecursive, "Recursive algorithm");
            Assert.AreEqual(result, resultKaratsuba, "Karatsuba algorithm");
        }
    }
}
