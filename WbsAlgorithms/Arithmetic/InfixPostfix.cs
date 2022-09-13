using System;
using System.Diagnostics;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithms.Arithmetic
{
    public class InfixPostfix
    {
        /// <summary>
        /// In postfix notation, the parentheses are optional and could be omitted. The ConvertToPostfix
        /// method returns a fully parenthesised postfix expression for clarity.
        /// 
        /// Examples:
        /// x + y --> x y +
        /// ( ( 1 + 2 ) * ( ( 3 - 4 ) * ( 5 - 6 ) ) )  -->  ( ( 1 2 + ) ( ( 3 4 - ) ( 5 6 - ) * ) * ) --> the same as: 1 2 + 3 4 - 5 6 - * *
        /// 
        /// [Sedgewick] 1.3.10 p.162 - Convert an arithmetic expression from infix to postfix.
        /// Reference: http://www.cs.man.ac.uk/~pjj/cs212/fix.html
        /// </summary>
        /// <param name="expression">A fully parenthesised infix arithmetic expression</param>
        /// <returns>A fully parenthesised postfix arithmetic expression</returns>
        public static string ConvertToPostfix(string expression)
        {
            var operators = new StackLinkedList<string>();
            var operands = new StackLinkedList<string>();

            var tokens = expression.Split(' ');

            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "(":
                        // Ignore left parentheses.
                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        operators.Push(token);
                        break;
                    case ")":
                        {
                            // Pop an operator.
                            var op = operators.Pop();

                            // Pop two operands (values). They are stored on the stack in opposite order.
                            var v2 = operands.Pop();
                            var v1 = operands.Pop();

                            // Do not evaluate the expression. Just concatenate
                            // operands and the operator and push back on the stack.
                            operands.Push($"( {v1} {v2} {op} )");
                        }
                        break;
                    default:
                        operands.Push(token);
                        break;
                }
            }

            Debug.Assert(operators.IsEmpty);
            Debug.Assert(operands.Size == 1);

            return operands.Pop();
        }

        /// <summary>
        /// [Sedgewick] 1.3.11 p.162 - Evaluate a postfix arithmetic expression.
        /// 
        /// Example:
        /// 4 2 + 1 3 - * --> -12
        /// </summary>
        /// <param name="expression">A postfix arithmetic expression (parenthesized or not)</param>
        /// <returns>Evaluated value of the input expression</returns>
        public static double EvaluatePostfix(string expression)
        {
            var operands = new StackLinkedList<double>();

            var tokens = expression.Split(' ');

            foreach (string token in tokens)
            {
                switch (token)
                {
                    case "(":
                    case ")":
                        // Ignore parentheses.
                        break;
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        {
                            // Pop two operands (values). They are stored on the stack in opposite order.
                            var v2 = operands.Pop();
                            var v1 = operands.Pop();

                            switch(token)
                            {
                                case "+":
                                    operands.Push(v1 + v2);
                                    break;
                                case "-":
                                    operands.Push(v1 - v2);
                                    break;
                                case "*":
                                    operands.Push(v1 * v2);
                                    break;
                                case "/":
                                    operands.Push(v1 / v2);
                                    break;
                            }
                        }
                        break;
                    default:
                        // The token is a numerical value.
                        if (double.TryParse(token, out var num))
                            operands.Push(num);
                        else
                            throw new ArithmeticException($"Expected a number. The value '{token}' is not a number.");
                        break;
                }
            }

            // At the end, there should be only one value on the stack - the value of the expression.
            Debug.Assert(operands.Size == 1);
            return operands.Pop();
        }
    }
}
