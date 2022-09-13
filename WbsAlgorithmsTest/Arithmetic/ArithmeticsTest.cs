using NUnit.Framework;
using WbsAlgorithms.Arithmetic;

namespace WbsAlgorithmsTest.Arithmetic
{
    [TestFixture]
    public class ArithmeticTest
    {
        // The arithmetic expressions have to be fully parenthesized with numbers and
        // characters separated by whitespace.
        [TestCase("( ( 1 + sqrt ( 5.0 ) ) / 2.0 )", 1.6180339887498949)]
        [TestCase("( 1.5 + ( ( 2 + 3 ) * ( 4 * 5 ) ) )", 101.5)]
        public void DijkstraTwoStackTest(string expression, double expectedResult)
        {
            var eval = new DijkstraTwoStack();
            var actualResult = eval.EvaluateExpression(expression);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("[()]{}{[()()]()}", true)]
        [TestCase("[(])", false)]
        [TestCase("[](", false)]
        [TestCase("[()]{({}[{}])}{()[()[({})]]({[]}()())}()", true)]
        [TestCase("[()]{({}[{}])}{()[()[({})]{]({[]}()())}()", false)]
        public void AreParenthesesBalancedTest(string parentheses, bool expectedValue)
        {
            var actualValue = BalancedParentheses.AreParenthesesBalanced(parentheses);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("1 + 2 ) * 3 - 4 ) * 5 - 6 ) ) )", "( ( 1 + 2 ) * ( ( 3 - 4 ) * ( 5 - 6 ) ) )")]
        [TestCase("1 + 2 * 1 + 3 ) ) )", "( 1 + ( 2 * ( 1 + 3 ) ) )")]
        [TestCase("1 + sqrt 5.0 ) ) / 2.0 )", "( ( 1 + sqrt ( 5.0 ) ) / 2.0 )")]
        public void InsertParenthesesTest(string expression, string expectedExpression)
        {
            var actualExpression = BalancedParentheses.InsertParentheses(expression);
            Assert.AreEqual(expectedExpression, actualExpression);
        }

        [TestCase("( x + y )", "( x y + )")]
        [TestCase("( ( A * B ) + ( C / D ) )", "( ( A B * ) ( C D / ) + )")]
        [TestCase("( ( 1 + 2 ) * ( 4 / 2 ) )", "( ( 1 2 + ) ( 4 2 / ) * )")]
        [TestCase("( ( 1 + 2 ) * ( ( 3 - 4 ) * ( 5 - 6 ) ) )", "( ( 1 2 + ) ( ( 3 4 - ) ( 5 6 - ) * ) * )")]
        [TestCase("( ( A * ( B + C ) ) / D )", "( ( A ( B C + ) * ) D / )")]
        [TestCase("( A * ( B + ( C / D ) ) )", "( A ( B ( C D / ) + ) * )")]
        public void ConvertToPostfixTest(string expression, string expectedExpression)
        {
            var actualExpression = InfixPostfix.ConvertToPostfix(expression);
            Assert.AreEqual(expectedExpression, actualExpression);
        }

        [TestCase("2 5 +", 7)]
        [TestCase("4 2 + 1 3 - *", -12)]
        [TestCase("1 2 * 3 4 / +", 2.75)]
        [TestCase("1 2 3 + * 4 /", 1.25)] // fully parenthesized: ( ( 1 ( 2 3 + ) * ) 4 / )
        [TestCase("2 2 3 4 / + *", 5.5)] // fully parenthesized: ( 2 ( 2 ( 3 4 / ) + ) * )
        [TestCase("( ( 1 2 + ) ( ( 3 4 - ) ( 5 6 - ) * ) * )", 3)] // fully parenthesized
        [TestCase("1 2 + 3 4 - 5 6 - * *", 3)] // the same as above but with no parentheses
        public void EvaluatePostfixTest(string expression, double expectedValue)
        {
            var actualValue = InfixPostfix.EvaluatePostfix(expression);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
