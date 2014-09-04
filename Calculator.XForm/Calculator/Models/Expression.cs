using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Expression
    {
        private LinkedList<string> tokens;
        private Status status;

        public Expression()
        {
            status = Status.Num;
            tokens = new LinkedList<string>();
        }

        public Expression(string exp) : this()
        {
            string[] tokens = exp.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            foreach(string token in tokens)
            {
                this.tokens.AddLast(token);
            }
        }

        public void AddToken(object token)
        {
            tokens.AddLast(token.ToString());
        }

        public void Append(string par)
        {
            Operator currentOp = Functions.GetOperator(par);
            Operator lastOp = Functions.GetOperator(tokens.LastOrDefault());

            switch (status)
            {
                case Status.Num:
                    if (currentOp != null)                  /* Num + Op  */
                    {
                        if (tokens.LastOrDefault() != null &&
                            currentOp.Type == TermType.RightMono)
                        {
                            tokens.AddLast(Functions.Multiply);
                        }
                        tokens.AddLast(par);
                        status = Status.Op;
                    }
                    else                                    
                    {
                        if (tokens.Count == 0)              /* _ + All   */
                            tokens.AddLast("");

                        string last = tokens.Last();
                        if (par == Functions.Dot)           /* Num + Dot */
                        {
                            if (!last.Contains(Functions.Dot))
                            {
                                if (last.Length == 0)
                                    tokens.Last.Value += "0";
                                tokens.Last.Value += Functions.Dot;
                            }
                        }
                        else                                /* Num + Num */
                        {
                            if (par != "0")
                                tokens.Last.Value += par;
                            else if (last.Length > 0 && last[0] != '0')
                                tokens.Last.Value += par;
                        }
                    }
                    break;

                case Status.Op:
                    if (currentOp != null)                  /* Op + Op   */
                    {
                        
                        if (currentOp.CanComeAfter(lastOp))
                        {
                            if (lastOp.Type == TermType.None &&
                                currentOp.Type == TermType.RightMono)
                            {
                                tokens.AddLast(Functions.Multiply);
                            }
                            tokens.AddLast(par);
                        }
                        else if (currentOp.Type == lastOp.Type)
                            tokens.Last.Value = par;
                    }
                    else if (par != Functions.Dot)          /* Op + Num  */
                    {
                        tokens.AddLast(par);
                        status = Status.Num;
                    }
                    break;
            }

            if (tokens.Last().Length == 0)
                tokens.RemoveLast();
        }

        public void Delete()
        {
            if (tokens.Count == 0)
                return;

            string last = tokens.Last();
            if(!String.IsNullOrEmpty(last))
            {
                if (status == Status.Op)
                {
                    tokens.RemoveLast();
                }
                else
                {
                    string newLast = last.Substring(0, last.Length - 1);
                    if (newLast.Length == 0)
                        tokens.RemoveLast();
                    else
                        tokens.Last.Value = newLast;
                }
                status = tokens.Count > 0 && Functions.IsOperator(tokens.Last()) ? 
                        Status.Op : Status.Num;
            }
        }

        public override string ToString()
        {
            return String.Join(" ", tokens);
        }

        public IEnumerable<string> Tokens
        {
            get { return tokens; }
        }
    }

    enum Status
    {
        Num, Op
    }
}
