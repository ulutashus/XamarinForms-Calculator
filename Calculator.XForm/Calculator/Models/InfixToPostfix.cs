using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public static class InfixToPostfix
    {
        // The operator stack
        private static Stack<Operator> operatorStack;
        // The precedence of the operators, matches order of OPERATORS.
        // The postfix string
        private static Expression postfix;

        public static Expression Convert(Expression infix)
        {
            operatorStack = new Stack<Operator>();
            postfix = new Expression();
            try
            {
                // Process each token in the infix string.
                foreach (var token in infix.Tokens)
                {
                    // Is it an operand?
                    if (token.Value is String)
                    {
                        postfix.AddToken(token.Value);
                    } // Is it an operator?
                    else if (token.Value is Operator)
                    {
                        ProcessOperator(token.Value as Operator);
                    }
                    else
                    {
                        throw new Exception("Unexpected Character Encountered: "
                             + token.Value);
                    }
                } 
                // Pop any remaining operators
                // and append them to postfix.
                while (operatorStack.Count != 0)
                {
                    Operator op = operatorStack.Pop();
                    // Any '(' on the stack is not matched.
                    //if (op.Value == Functions.BracketOne)
                    //    throw new Exception("Unmatched opening parenthesis");
                    postfix.AddToken(op);
                }
                // assert: Stack is empty, return result.
                return postfix;
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Syntax Error: The stack is empty");
            }
        }

        private static void ProcessOperator(Operator op)
        {
            if (operatorStack.Count == 0 || op.Display == Functions.BracketOne)
            {
                operatorStack.Push(op);
            }
            else
            {
                // Peek the operator stack and
                // let topOp be the top operator.
                Operator topOp = operatorStack.Peek();
                if (op.Precedence > topOp.Precedence)
                {
                    operatorStack.Push(op);
                }
                else
                {
                    // Pop all stacked operators with equal
                    // or higher precedence than op.
                    while (operatorStack.Count != 0
                           && op.Precedence <= topOp.Precedence)
                    {
                        operatorStack.Pop();
                        if (topOp.Display == Functions.BracketOne)
                        {
                            // Matching '(' popped - exit loop.
                            break;
                        }
                        postfix.AddToken(topOp);
                        if (operatorStack.Count != 0)
                        {
                            // Reset topOp.
                            topOp = operatorStack.Peek();
                        }
                    }

                    // assert: Operator stack is empty or
                    //         current operator precedence >
                    //         top of stack operator precedence.
                    if (op.Display != Functions.BracketTwo)
                        operatorStack.Push(op);
                }
            }
        }
    }
}
