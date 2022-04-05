using System.Diagnostics;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithms.Arithmetics
{
    public class BalancedParentheses
    {
        /// <summary>
        /// Verifies if a provided string contains properly balanced parentheses. 
        /// The program uses a stack to analyze the input string.
        /// 
        /// [Sedgewick] 1.3.4 p.161 - Determine whether the input parentheses are balanced.
        /// 
        /// Examples:
        /// [()]{}{[()()]()} - true (balanced)
        /// [(]) - false (not balanced)
        /// </summary>
        /// <param name="parentheses"></param>
        /// <returns>True if balanced; otherwise, false</returns>
        public static bool AreParenthesesBalanced(string parentheses)
        {
            var s = new StackLinkedList<char>();

            foreach (char ch in parentheses)
            {
                if (ch == '{' || ch == '[' || ch == '(')
                    s.Push(ch);
                else
                {
                    char r = s.Pop();
                    if ((ch == ')' && r != '(') ||
                        (ch == ']' && r != '[') ||
                        (ch == '}' && r != '{'))
                    {
                        return false;
                    }
                }
            }

            return s.IsEmpty;
        }

        /// <summary>
        /// [Sedgewick] 1.3.9 p.162 - Insert missing parentheses.
        /// 
        /// The InsertParentheses method takes an expression without left parentheses and 
        /// returns the equivalent infix expression with parentheses inserted.
        /// The solution is similar to Dijkstra's Two Stack algorithm except
        /// the operands are not converted to numbers and arithmentic expressions 
        /// are not evalueated. Instead, they are stored on the operands stack
        /// as strings. At the end, there should be only one expression on the operands
        /// stack - the fully parenthesised expression with left parentheses inserted.
        /// 
        /// Example:
        /// Input: 1 + 2 ) * 3 - 4 ) * 5 - 6 ) ) )
        /// Output: ( ( 1 + 2 ) * ( ( 3 - 4 ) * ( 5 - 6 ) ) )
        /// </summary>
        /// <param name="expression">A fully parenthesised expression but without left parentheses</param>
        /// <returns>A fully parenthesised expression</returns>
        public static string InsertParentheses(string expression)
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
                    case "sqrt":
                        operators.Push(token);
                        break;
                    case ")":
                        {
                            // Pop an operator.
                            var op = operators.Pop();

                            if (op == "sqrt")
                            {
                                // Pop one operand.
                                var v = operands.Pop();
                                operands.Push($"{op} ( {v} )");
                            }
                            else
                            {
                                // Pop two operands (values). They are stored on the stack in opposite order.
                                var v2 = operands.Pop();
                                var v1 = operands.Pop();

                                // Do not evaluate the expression. Just concatenate
                                // operands and the operator and push back on the stack.
                                operands.Push($"( {v1} {op} {v2} )");
                            }
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
    }
}
