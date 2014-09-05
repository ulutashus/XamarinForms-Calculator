using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using Calculator.ViewModels.Helpers;

namespace Calculator.Models
{
    public static class Functions
    {
        private static Dictionary<string, Function> functions;

        static Functions()
        {
            functions = new Dictionary<string, Function>();

            var assembly = typeof(Functions).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Calculator.functions.xml");
            var elements = XDocument.Load(stream).Element("functions").Elements();

            foreach (XElement elem in elements)
            {
                string funcName = elem.Attribute("name").Value;
                Function func;
                if( elem.Name == "function" ) 
                {
                    func = new Function(funcName)
                    {
                        Display = elem.Attribute("display").Value
                    };
                }
                else
                {
                    func = new Operator(funcName)
                    {
                        Display = elem.Attribute("display").Value,
                        Precedence = Int32.Parse(elem.Attribute("precedence").Value),
                        Type = (TermType)Enum.Parse(typeof(TermType), elem.Attribute("type").Value)
                    };
                }
                functions.Add(funcName, func);
            }
        }

        public static Operator GetOperator(string display)
        {
            var op = functions.Values.FirstOrDefault(p => p is Operator && p.Display == display);
            return op != null ? (Operator)op : null;
        }

        public static IEnumerable<Operator> GetOperators(string display)
        {
            var op = functions.Values.
                Where(p => p is Operator && p.Display == display).
                Select(p => (Operator)p);
            return op;
        }

        public static bool IsOperator(string display)
        {
            var value = functions.FirstOrDefault(p => p.Value.Display == display).Value;
            return value != null && value is Operator;
        }

        #region Function Properties
        public static string Addition   { get { return GetDisplay("Addition"); } }
        public static string Subtraction{ get { return GetDisplay("Subtraction"); } }
        public static string Multiply { get { return GetDisplay("Multiply"); } }
        public static string Division { get { return GetDisplay("Division"); } }
        public static string Equal { get { return GetDisplay("Equal"); } }
        public static string Delete { get { return GetDisplay("Delete"); } }
        public static string Dot { get { return GetDisplay("Dot"); } }
        public static string Sin { get { return GetDisplay("Sin"); } }
        public static string Cos { get { return GetDisplay("Cos"); } }
        public static string Tan { get { return GetDisplay("Tan"); } }
        public static string Ln { get { return GetDisplay("Ln"); } }
        public static string Log { get { return GetDisplay("Log"); } }
        public static string Factorial { get { return GetDisplay("Factorial"); } }
        public static string Pi { get { return GetDisplay("Pi"); } }
        public static string E { get { return GetDisplay("E"); } }
        public static string Pow { get { return GetDisplay("Pow"); } }
        public static string BracketOne { get { return GetDisplay("BracketOne"); } }
        public static string BracketTwo { get { return GetDisplay("BracketTwo"); } }
        public static string Root { get { return GetDisplay("Root"); } }

        private static string GetDisplay(string name)
        {
            return functions[name].Display;
        }
        #endregion
    }

}
