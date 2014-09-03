using Calculator.ViewModels;
using Calculator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Calculator
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new CalculatorView()
            {
                BindingContext = new CalculatorVModel()
            };
        }
    }
}
