using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Calculator.ViewModels.Helpers
{
    public abstract class ViewModelBase : ObservableProperty
    {
        public CommandList Commands { get; protected set; }
    }
}
