using Calculator.Models;
using Calculator.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class CalculatorVModel : ViewModelBase
    {
        private Expression expression;
        private Evaluator evaluator;
        private string resultText;
        private bool[] pageActive;

        public CalculatorVModel()
        {
            resultText = "0";
            expression = new Expression();
            evaluator = new Evaluator();
            pageActive = new bool[] {true, false};
         
            Commands = new CommandList();
            Commands.AddCommand("NUM", new DelegateCommand<string>(new Action<string>(Cmd_Num)));
            Commands.AddCommand("NUM2", new DelegateCommand<string>(new Action<string>(Cmd_Num2)));
            Commands.AddCommand("SWITCH", new DelegateCommand(new Action(Cmd_Switch)));
        }

        private void Cmd_Num(string par)
        {
            double result;
            if (par == Functions.Equal)
            {
                result = evaluator.Evaluate(expression);
                ExpressionText = result.ToString();
            }
            else
            {
                UpdateExpression(par);
                result = evaluator.Evaluate(expression);
            }
            DisplayResult();
        }

        private void Cmd_Num2(string par)
        {
            Cmd_Num(par);
            Cmd_Switch();
        }

        private void Cmd_Switch()
        {
            bool temp = pageActive[0];
            pageActive[0] = pageActive[1];
            pageActive[1] = temp;
            OnPropertyChanged("PageActive");
        }

        #region Helpers
        private void DisplayResult()
        {
            if (expression.ToString().Length == 0)
                ResultText = "0";
            else if (evaluator.Status == ResultStatus.Success)
                ResultText = evaluator.Result.ToString();
            else
                ResultText = "Hata";
        }

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

        public ObservableCollection<bool> PageActive
        {
            get { return new ObservableCollection<bool>(pageActive); }
        }

        public string ResultText
        {
            get { return resultText; }
            set { resultText = value; OnPropertyChanged("ResultText"); }
        }
        #endregion
    }
}
