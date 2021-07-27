using System;
using System.Diagnostics;
using WbsAlgorithms.Collections;

namespace WbsAlgorithms.Arithmetics
{
    /// <summary>
    /// The EvaluateExpression method evaluates an arithmetic expression provided as a string and returns a numerical 
    /// result. The input expression has to be fully parenthesized. The method supports the *, +, -, and / operators 
    /// as well as a square root operator sqrt.
    /// 
    /// Definition of an arithmetic expression [Sedgewick]: an arithmetic expression is either a number, or a left
    /// parenthesis followed by an arithmetic expression followed by an operator followed by another arithmetic 
    /// expression followed by a right parenthesis. This definition is for fully parenthesized arithmetic expressions.
    /// 
    /// The evaluation of the arithmetic expressions is performed by the Dijkstra's two-stack algorithm. The algorithm 
    /// uses two stacks: one is used for operands and another one for operators. Proceeding from left to right, the algorithm
    /// takes a parenthesis, an operator, or an operand (a number) one at a time and manipulate the stacks according to 
    /// four possible cases:
    /// - Push an operand onto the operand stack.
    /// - Push an operato onto the operator stack.
    /// - Ignore left parenthesis.
    /// - On ecountering a right parenthesis, pop an operator, pop the requisite number of operands, and push onto the operand
    ///   stack the result of applying that operator to those operands.
    /// After the final right parenthesis is processed, there is one value on the stack - the value of the expression.
    /// 
    /// This program is an example of an interpreter: a program that interprets a string and performs computations to 
    /// return a result.
    /// 
    /// Example: EvaluateExpression("( ( 1 + sqrt ( 5.0 ) ) / 2.0 )")
    /// ( ( 1 + sqrt ( 5.0 ) ) / 2.0 )
    /// ( ( 1 + 2.23606 ) / 2.0 )
    /// ( 3.23606 / 2.0 )
    /// 1.61803
    /// 
    /// [Sedgewick] p.128-131 - Arithmetic expression evaluation 
    /// </summary>
    public class DijkstraTwoStack
    {
        private StackLinkedList<string> operators = new StackLinkedList<string>();
        private StackLinkedList<double> values = new StackLinkedList<double>(); // operands

        public double EvaluateExpression(string expression)
        {
            string[] tokens = expression.Split(' ');

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
                    case "sqrt":
                        operators.Push(token);
                        break;
                    case ")":
                        {
                            // Pop, evaluate, and push result if the token is ")".

                            // Pop an operator.
                            string op = operators.Pop();

                            // Pop the first operand.
                            double val = values.Pop();

                            switch (op)
                            {
                                // Pop the second operand and evaluate.
                                case "+":
                                    val = values.Pop() + val;
                                    break;
                                case "-":
                                    val = values.Pop() - val;
                                    break;
                                case "*":
                                    val = values.Pop() * val;
                                    break;
                                case "/":
                                    val = values.Pop() / val;
                                    break;
                                case "sqrt":
                                    val = Math.Sqrt(val);
                                    break;
                            }

                            // Push onto the operand stack the result.
                            values.Push(val);
                        }
                        break;
                    default:
                        // The token is a numerical value.
                        if (double.TryParse(token, out var num))
                            values.Push(num);
                        else
                            throw new ArithmeticException($"Expected a number. The value '{token}' is not a number.");
                        break;
                }
            }

            // At the end, there should be only one value on the stack - the value of the expression.
            Debug.Assert(values.Size == 1);
            return values.Pop();
        }
    }
}
