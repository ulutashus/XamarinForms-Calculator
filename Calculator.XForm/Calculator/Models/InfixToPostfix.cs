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
        private static Stack<char> operatorStack;
        // The operators
        private static String OPERATORS = "+-*/()";
        // The precedence of the operators, matches order of OPERATORS.
        private static int[] PRECEDENCE = { 1, 1, 2, 2, -1, -1 };
        // The postfix string
        private static Expression postfix;

        public static Expression Convert(Expression infix)
        {
            operatorStack = new Stack<char>();
            postfix = new Expression();
            try
            {
                // Process each token in the infix string.
                foreach (string nextToken in infix.Tokens)
                {
                    char firstChar = nextToken[0];
                    // Is it an operand?
                    if (Char.IsDigit(firstChar))
                    {
                        postfix.AddToken(nextToken);
                    } // Is it an operator?
                    else if (IsOperator(firstChar))
                    {
                        ProcessOperator(firstChar);
                    }
                    else
                    {
                        throw new Exception("Unexpected Character Encountered: "
                             + firstChar);
                    }
                } 
                // Pop any remaining operators
                // and append them to postfix.
                while (operatorStack.Count != 0)
                {
                    char op = operatorStack.Pop();
                    // Any '(' on the stack is not matched.
                    if (op == '(')
                        throw new Exception("Unmatched opening parenthesis");
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

        private static void ProcessOperator(char op)
        {
            if (operatorStack.Count == 0 || op == '(')
            {
                operatorStack.Push(op);
            }
            else
            {
                // Peek the operator stack and
                // let topOp be the top operator.
                char topOp = operatorStack.Peek();
                if (Precedence(op) > Precedence(topOp))
                {
                    operatorStack.Push(op);
                }
                else
                {
                    // Pop all stacked operators with equal
                    // or higher precedence than op.
                    while (operatorStack.Count != 0
                           && Precedence(op) <= Precedence(topOp))
                    {
                        operatorStack.Pop();
                        if (topOp == '(')
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
                    if (op != ')')
                        operatorStack.Push(op);
                }
            }
        }

        private static bool IsOperator(char ch)
        {
            return OPERATORS.Contains(ch.ToString());
        }

        private static int Precedence(char op)
        {
            return PRECEDENCE[OPERATORS.IndexOf(op)];
        }
    }
}
