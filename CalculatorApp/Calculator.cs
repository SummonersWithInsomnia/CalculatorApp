using System;
using System.Text.RegularExpressions;

namespace CalculatorApp
{
    public class Calculator
    {
        public string Expression { get; private set; }
        public string Result { get; private set; }

        public Calculator(string expression)
        {
            // Take out all spaces from the expression
            // and store it in the calculator object
            Expression = Regex.Replace(expression, @"\s+", "");

            // calculate the result
            Result = this.calculate();
        }

        private string calculate()
        {
            string e = Expression;

            // the procedure of handling the expression
            checker(e);
            e = processBrackets(e);
            e = multiplyAndDivide(e);
            e = addAndSubtract(e);

            return e;
        }

        private void checker(string expression)
        {
        }

        private string processBrackets(string expression)
        {
        }

        private string multiplyAndDivide(string expression)
        {
        }

        private string addAndSubtract(string expression)
        {
        }
    }
}