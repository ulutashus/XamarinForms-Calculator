
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Operator : Function
    {
        private int precedence;
        private TermType type;
        private event Operation operation;

        public Operator(string name) : base(name)
        {
            Type[] parameters = {typeof(double),typeof(double)};
            MethodInfo methodInf  = typeof(Operations).GetRuntimeMethod(name,parameters);
            if (methodInf != null)
            {
                operation = (Operation)
                    methodInf.CreateDelegate(typeof(Operation));
            }
        }

        public bool CanComeAfter(Operator opp)
        {
            if (opp.Type == TermType.None)
                return true;

            if (type == TermType.Biominal)
                if (opp.Type != TermType.LeftMono)
                    return false;
            if (type == TermType.LeftMono)
                if (opp.Type != TermType.LeftMono)
                    return false;
            if (type == TermType.RightMono)
                if (opp.Type == TermType.LeftMono)
                    return false;

            return true;
        }

        #region Properties
        public Operation Operation
        {
            get { return operation; }
        }

        public int Precedence
        {
            get { return precedence; }
            set { precedence = value; }
        }

        public TermType Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion
    }

    public delegate double Operation(double left, double right);

    public enum TermType
    {
        LeftMono, RightMono, Biominal, None
    }
}
