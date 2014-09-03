using Calculator.Models;
using Calculator.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public class CalculatorVModel : ViewModelBase
    {
        private Expression expression;
        private string resultText;

        public CalculatorVModel()
        {
            resultText = "0";
            expression = new Expression();
         
            Commands = new CommandList();
            Commands.AddCommand("NUM", new DelegateCommand<string>(new Action<string>(Cmd_Num)));
        }

        private void Cmd_Num(string par)
        {
            if (par == Functions.Equal)
            {
                ResultText = ExpressionText =
                    Evaluator.Evaluate(expression).ToString();
            }
            else
            {
                UpdateExpression(par);
                ResultText = Evaluator.Evaluate(expression).ToString();
            }
        }

        #region Update Expression
        private void UpdateExpression(string par)
        {
            if (par == Functions.Delete)
                expression.Delete();
            else
                expression.Append(par);
            OnPropertyChanged("ExpressionText");
        }
        #endregion

        #region UI Properties
        public string ExpressionText
        {
            get { return expression.ToString(); }
            set { expression = new Expression(value); OnPropertyChanged("ExpressionText"); }
        }

        public string ResultText
        {
            get { return resultText; }
            set { resultText = value; OnPropertyChanged("ResultText"); }
        }
        #endregion
    }
}
