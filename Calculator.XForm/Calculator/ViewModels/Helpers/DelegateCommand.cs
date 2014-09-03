using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Calculator.ViewModels.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _handler;

        public DelegateCommand(Action handler)
        {
            _handler = handler;
        }

        public bool IsEnabled
        {
            get;
            set;
        }
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }

        #endregion
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeMethod;

        public DelegateCommand(Action<T> executeMethod)
        {
            this.executeMethod = executeMethod;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            T par = (T)parameter;
            executeMethod(par);
        }
        #endregion
    }
}
