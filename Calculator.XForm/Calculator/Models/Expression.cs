using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Expression
    {
        private LinkedList<object> tokens;
        private Status status;

        public Expression()
        {
            status = Status.Num;
            tokens = new LinkedList<object>();
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
            tokens.AddLast(token);
        }

        public void Append(string par)
        {
            switch (status)
            {
                case Status.Num:
                    AppendAfterNum(par);
                    break;

                case Status.Op:
                    AppendAfterOp(par);
                    break;
            }

            //if (tokens.Last().Length == 0)
                //tokens.RemoveLast();
        }

        public void Delete()
        {
            if (tokens.Count == 0)
                return;

            //if(!String.IsNullOrEmpty(last))
            //{
                if (status == Status.Op)
                {
                    tokens.RemoveLast();
                }
                else
                {
                    string last = tokens.Last() as string;
                    string newLast = last.Substring(0, last.Length - 1);
                    if (newLast.Length == 0)
                        tokens.RemoveLast();
                    else
                        tokens.Last.Value = newLast;
                }
                status = tokens.Count > 0 && tokens.Last() is Operator ? 
                        Status.Op : Status.Num;
            //}
        }

        public override string ToString()
        {
            return String.Join(" ", tokens);
        }

        #region Helpers
        private void AppendAfterNum(string par)
        {
            var ops = Functions.GetOperators(par);
            Operator preOp = tokens.Count == 0 ? Operator.Begin : tokens.Last() as Operator;
            Operator currentOp = ops.FirstOrDefault(p => p.CanComeAfter(preOp));

            if (currentOp != null)                  /* Num + Op  */
            {
                if (tokens.LastOrDefault() != null &&
                    currentOp.Type == TermType.RightMono)
                {
                    tokens.AddLast(Functions.GetOperator(Functions.Multiply));
                }
                tokens.AddLast(currentOp);
                status = Status.Op;
            }
            else if(ops.Count() == 0)
            {
                if (tokens.Count == 0)              /* _ + All   */
                    tokens.AddLast("");

                string last = tokens.Last() as string;
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
        }

        private void AppendAfterOp(string par)
        {
            var ops = Functions.GetOperators(par).ToList();
            Operator preOp = tokens.Count == 0 ? Operator.Begin : tokens.Last() as Operator;
            Operator currentOp = ops.FirstOrDefault(p => p.CanComeAfter(preOp));

            if (ops.Count() != 0)                  /* Op + Op   */
            {
                if (currentOp != null && currentOp.CanComeAfter(preOp))
                {
                    if (preOp.Type == TermType.None &&
                        currentOp.Type == TermType.RightMono)
                    {
                        tokens.AddLast(Functions.GetOperator(Functions.Multiply));
                    }
                    tokens.AddLast(currentOp);
                }
                else if (ops.Any(p => p.Type == preOp.Type))
                    tokens.Last.Value = ops.FirstOrDefault(p => p.Type == preOp.Type);
            }
            else if (par != Functions.Dot)          /* Op + Num  */
            {
                tokens.AddLast(par);
                status = Status.Num;
            }
        }
        #endregion

        #region Properties
        public IEnumerable<LinkedListNode<object>> Tokens
        {
            get 
            { 
                var nodes = new List<LinkedListNode<object>>();
                if (tokens.Count > 0)
                {
                    for (var node = tokens.First; node != null; node = node.Next)
                    {
                        nodes.Add(node);
                    }
                }
                return nodes;
            }
        }
        #endregion
    }

    enum Status
    {
        Num, Op
    }
}
