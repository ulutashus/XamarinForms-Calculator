using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public static class Evaluator
    {
        // A list of operators.
        private static String OPERATORS = "+-*/";
        // The operand stack.
        private static Stack<double> operandStack;

        public static double Evaluate(Expression expression)
        {
            Expression postfix = InfixToPostfix.Convert(expression);
            ResultStatus status;
            return EvalPostfix(postfix, out status);
        }

        /** Evaluates a postfix expression.
            @param expression The expression to be evaluated
            @return The value of the expression
            @throws SyntaxErrorException if a syntax error is detected
         */
        private static double EvalPostfix(Expression expression, out ResultStatus resultStatus)
        {
            double result = 0;
            // Create an empty stack.
            operandStack = new Stack<double>();

            // Process each token.
            try
            {
                foreach (string nextToken in expression.Tokens)
                {
                    // Does it start with a digit?
                    if (Char.IsDigit(nextToken[0]))
                    {
                        // Get the integer value.
                        double value = Double.Parse(nextToken);
                        // Push value onto operand stack.
                        operandStack.Push(value);
                    } // Is it an operator?
                    else if (IsOperator(nextToken[0]))
                    {
                        // Evaluate the operator.
                        result = operandStack.Peek();
                        double res = EvalOp(nextToken[0]);
                        // Push result onto the operand stack.
                        operandStack.Push(res);
                    }
                    else
                    {
                        // Invalid character.
                        resultStatus = ResultStatus.InvalidChar;
                        return operandStack.Pop();
                    }
                } // End while.

                // No more tokens - pop result from operand stack.
                result = operandStack.Pop();
                // Operand stack should be empty.
                if (operandStack.Count == 0)
                {
                    resultStatus = ResultStatus.Success;
                }
                else
                {
                    // Indicate syntax error.
                    resultStatus = ResultStatus.Slack;
                }
            }
            catch (InvalidOperationException)
            {
                // Pop was attempted on an empty stack.
                resultStatus = ResultStatus.Lack;
            }
            return result;
        }

        /** Evaluates the current operation.
            This function pops the two operands off the operand
            stack and applies the operator.
            @param op A character representing the operator
            @return The result of applying the operator
            @throws EmptyStackException if pop is attempted on
                    an empty stack
         */
        private static double EvalOp(char op)
        {
            // Pop the two operands off the stack.
            double rhs = operandStack.Pop();
            double lhs = operandStack.Pop();
            double result = 0;
            // Evaluate the operator.
            switch (op)
            {
                case '+':
                    result = lhs + rhs;
                    break;
                case '-':
                    result = lhs - rhs;
                    break;
                case '/':
                    result = lhs / rhs;
                    break;
                case '*':
                    result = lhs * rhs;
                    break;

            }
            return result;
        }

        /** Determines whether a character is an operator.
            @param op The character to be tested
            @return true if the character is an operator
         */
        private static bool IsOperator(char ch)
        {
            return OPERATORS.Contains(ch.ToString());
        }

        private enum ResultStatus
        {
            Success, InvalidChar, Incomplete, Lack, Slack
        }
    }
}
