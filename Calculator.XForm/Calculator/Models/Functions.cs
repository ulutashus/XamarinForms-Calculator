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
                Function func = elem.Name == "function" ?
                    new Function(elem.Attribute("name").Value)
                    {
                        Value = elem.Attribute("value").Value
                    } :
                    new Operator(elem.Attribute("name").Value)
                    {
                        Value = elem.Attribute("value").Value,
                        Precedence = Int32.Parse(elem.Attribute("precedence").Value),
                        Type = (TermType)Enum.Parse(typeof(TermType), elem.Attribute("type").Value)
                    };

                functions.Add(elem.Attribute("name").Value, func);
            }
        }

        public static Operator GetOperator(string name)
        {
            var op = functions.Values.FirstOrDefault(p => p is Operator && p.Value == name);
            return op != null ? (Operator)op : null;
        }

        public static bool IsOperator(string name)
        {
            var value = functions.FirstOrDefault(p => p.Value.Value == name).Value;
            return value != null && value is Operator;
        }

        #region Function Properties
        public static string Addition   { get { return GetValue("Addition"); } }
        public static string Subtraction{ get { return GetValue("Subtraction"); } }
        public static string Multiply { get { return GetValue("Multiply"); } }
        public static string Division { get { return GetValue("Division"); } }
        public static string Equal { get { return GetValue("Equal"); } }
        public static string Delete { get { return GetValue("Delete"); } }
        public static string Dot { get { return GetValue("Dot"); } }
        public static string Sin { get { return GetValue("Sin"); } }
        public static string Cos { get { return GetValue("Cos"); } }
        public static string Tan { get { return GetValue("Tan"); } }
        public static string Ln { get { return GetValue("Ln"); } }
        public static string Log { get { return GetValue("Log"); } }
        public static string Factorial { get { return GetValue("Factorial"); } }
        public static string Pi { get { return GetValue("Pi"); } }
        public static string E { get { return GetValue("E"); } }
        public static string Pow { get { return GetValue("Pow"); } }
        public static string BracketOne { get { return GetValue("BracketOne"); } }
        public static string BracketTwo { get { return GetValue("BracketTwo"); } }
        public static string Root { get { return GetValue("Root"); } }

        private static string GetValue(string name)
        {
            return functions[name].Value;
        }
        #endregion
    }

}
