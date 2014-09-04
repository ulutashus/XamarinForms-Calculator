using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public static class Operations
    {
        public static double Addition(double left, double right)
        {
            return left + right;
        }

        public static double Subtraction(double left, double right)
        {
            return left - right;
        }

        public static double Multiply(double left, double right)
        {
            return left * right;
        }

        public static double Division(double left, double right)
        {
            return left / right;
        }

        public static double Sin(double left, double right)
        {
            return Math.Sin(right);
        }

        public static double Cos(double left, double right)
        {
            return Math.Cos(right);
        }

        public static double Tan(double left, double right)
        {
            return Math.Tan(right);
        }

        public static double Ln(double left, double right)
        {
            return Math.Log(right);
        }

        public static double Log(double left, double right)
        {
            return Math.Log10(right);
        }

        public static double Factorial(double left, double right)
        {
            double result = 1;
            for (; right > 0; --right) result *= right;
            return result;
        }

        public static double Pow(double left, double right)
        {
            return Math.Pow(left, right);
        }

        public static double Root(double left, double right)
        {
            return Math.Sqrt(right);
        }

        public static double E(double left, double right)
        {
            return Math.E;
        }

        public static double Pi(double left, double right)
        {
            return Math.PI;
        }
    }

}
