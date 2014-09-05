using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Evaluator
    {
        // The operand stack.
        private Stack<double> operandStack;
        private ResultStatus status;
        private double result;

        public double Evaluate(Expression expression)
        {
            Expression postfix = InfixToPostfix.Convert(expression);
            result = EvalPostfix(postfix, out status);
            return result;
        }

        private double EvalPostfix(Expression expression, out ResultStatus resultStatus)
        {
            double result = 0;
            // Create an empty stack.
            operandStack = new Stack<double>();

            // Process each token.
            try
            {
                foreach (var token in expression.Tokens)
                {
                    // Does it start with a digit?
                    if (token.Value is String)
                    {
                        // Get the integer value.
                        double value = Double.Parse(token.Value as string);
                        // Push value onto operand stack.
                        operandStack.Push(value);
                    } // Is it an operator?
                    else if (token.Value is Operator)
                    {
                        // Evaluate the operator.
                        double res = EvalOp(token.Value as Operator);
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

        private double EvalOp(Operator op)
        {
            // Pop the two operands off the stack.
            double rhs = op.Type != TermType.None ?
                operandStack.Pop() : 0;
            double lhs = op.Type == TermType.Biominal ?
                operandStack.Pop() : 0;
            // Evaluate the operator.
            return op.Operation != null ? op.Operation(lhs, rhs) : 0;
        }

        #region Properties
        public double Result
        {
            get { return result; }
        }

        public ResultStatus Status
        {
            get { return status; }
        }
        #endregion
    }

    public enum ResultStatus
    {
        Success, InvalidChar, Incomplete, Lack, Slack
    }
}
