using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.ViewModels.Helpers
{
    public class CommandList
    {
        #region Fields
        private Dictionary<string, ICommand> commandList;
        #endregion

        public CommandList()
        {
            commandList = new Dictionary<string, ICommand>();
        }

        public ICommand this[string name]
        {
            get
            {
                if (commandList.ContainsKey(name))
                    return commandList[name];
                else 
                    return null;
            }
        }

        public Dictionary<string, ICommand> List
        {
            get { return commandList; }
        }

        public void AddCommand(string name, ICommand cmd)
        {
            if (commandList.ContainsKey(name))
                commandList[name] = cmd;
            else
                commandList.Add(name, cmd);
        }
    }
}
