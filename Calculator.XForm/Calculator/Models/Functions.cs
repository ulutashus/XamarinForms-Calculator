using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public static class Functions
    {
        private static string addition = "+";
        private static string subtraction = "-";
        private static string multiply = "*";
        private static string division = "/";
        private static string equal = "=";
        private static string delete = "Del";
        private static string dot = ".";

        public static string Addition { get { return addition; } }
        public static string Subtraction { get { return subtraction; } }
        public static string Multiply { get { return multiply; } }
        public static string Division { get { return division; } }
        public static string Equal { get { return equal; } }
        public static string Delete { get { return delete; } }
        public static string Dot { get { return dot; } }
    }
}
