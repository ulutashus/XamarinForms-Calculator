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
        private const string Ops = "+/*-^";

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
            if (tokens.Count == 0)
                tokens.AddLast("");

            switch (status)
            {
                case Status.Num:
                    if (Ops.Contains(par))
                    {
                        tokens.AddLast(par);
                        status = Status.Op;
                    }
                    else if (par == Functions.Dot)
                    {
                        string last = tokens.Last();
                        if (!last.Contains(Functions.Dot))
                        {
                            if (last.Length == 0)
                                last += "0";
                            last += Functions.Dot;
                            tokens.Last.Value = last;
                        }
                    }
                    else
                    {
                        string last = tokens.Last();
                        if (par != "0")
                            tokens.Last.Value += par;
                        else if (last.Length > 0 && last[0] != '0')
                            tokens.Last.Value += par;
                    }
                    break;

                case Status.Op:
                    if (Ops.Contains(par))
                    {
                        tokens.Last.Value = par;
                    }
                    else if (par != Functions.Dot)
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
                string newLast = last.Substring(0, last.Length - 1);
                if (newLast.Length == 0)
                    tokens.RemoveLast();
                else
                    tokens.Last.Value = newLast;
                status = (tokens.Count > 0 && Ops.Contains(tokens.Last())) ? 
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
