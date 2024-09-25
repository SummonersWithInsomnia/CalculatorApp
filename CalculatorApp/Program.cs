using System;

namespace CalculatorApp
{
    public class Program
    {
        public static string ProcessCommand(string input)
        {
            try
            {
                return new Calculator(input).Result;
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }

        static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                Console.WriteLine(ProcessCommand(input));
            }
        }
    }
}