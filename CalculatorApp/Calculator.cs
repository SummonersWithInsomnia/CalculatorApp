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
            // Initialize a counter for checking brackets
            // 0 means that the expression has brackets or no brackets
            int bracketCount = 0;

            // Loop through each character in the expression
            for (int i = 0; i < expression.Length; i++)
            {
                // Increment counter for each opening bracket '('
                if (expression[i] == '(')
                {
                    bracketCount++;
                }
                // Decrement counter for each closing bracket ')'
                else if (expression[i] == ')')
                {
                    bracketCount--;
                }
            }

            // check if the bracket count is zero, make sure they are pairs of brackets
            if (bracketCount != 0)
            {
                // Throw exception if brackets are mismatche
                throw new Exception("Invalid expression with wrong brackets: " + expression);
            }

            // Check if the expression length is zero
            if (expression.Length == 0)
            {
                throw new Exception("Invalid expression."); // Throw exception if the expression is empty
            }

            // Define a regular expression pattern to validate the expression
            string pattern = @"^[()\d+\-*/. ]+$";
            // Check if the expression matches the defined pattern
            if (!Regex.IsMatch(expression, pattern))
            {
                throw new Exception("Invalid expression: " + expression); // Throw exception if the expression contains invalid characters
            }

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