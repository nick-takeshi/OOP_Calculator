using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Calculator
{
    public class DefaultOperations
    {
        public static double InputDoubleValue(string line)
        {
            Console.WriteLine(line);
            return Convert.ToDouble(Console.ReadLine());
        }
        public static char InputOperation(string line)
        {
            Console.WriteLine(line);
            return Convert.ToChar(Console.ReadLine());
        }
    }
    class Calculator
    {
        private class Errors
        {
            public static Exception InvalidOperationError()
            {
                return new Exception("Invalid operation");
            }
            public static Exception DivideByZeroError()
            {
                return new Exception("Сan't divide by zero");
            }
        }

        private double num1;
        private double num2;
        private char operation;
        private List<char> allowedOperations = new List<char>()
        {
            '+', '-', '*', '/'
        };

        public double Num1 { get => num1; set => num1 = value; }
        public double Num2 
        { 
            get => num2; 
            set
            {
                DivideByZeroValidation();
                num2 = value;
            }
        }
        public char Operation 
        { 
            get => operation;
            set
            {
                InvalidOperationValidation();
                operation = value;
            }
        }
        public double Result { get => Calculate(); }

        public Calculator(double num1, double num2, char operation)
        {
            Num1 = num1;
            Operation = operation;
            Num2 = num2;
        }

        public static Calculator CreateCalculation()
        {
            double num1 = DefaultOperations.InputDoubleValue("Введите первое число");
            char operation = DefaultOperations.InputOperation("Введите операцию: +, -, *, /");
            double num2 = DefaultOperations.InputDoubleValue("Введите второе число");

            return new Calculator(num1, num2, operation);
        }
        private double Calculate()
        {
            switch (Operation)
            {
                case '-': return Num1 - Num2;
                case '*': return Num1 * Num2;
                case '/': return Num1 / Num2;
                default: return Num1 + Num2;
            }
        }
        private void DivideByZeroValidation()
        {
            if (Operation == '/' && Num2 == 0)
            {
                throw Errors.DivideByZeroError();
            }
        }
        private void InvalidOperationValidation()
        {
            if (!allowedOperations.Contains(Operation))
            {
                throw Errors.InvalidOperationError();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(Math.Round(Calculator.CreateCalculation().Result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
