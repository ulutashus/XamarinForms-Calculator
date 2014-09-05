using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Function
    {
        public string Name { get; private set; }
        public string Display { get; set; }

        public Function(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
