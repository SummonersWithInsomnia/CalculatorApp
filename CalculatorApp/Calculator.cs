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
            e = checker(e);
            e = processBrackets(e);
            e = multiplyAndDivide(e);
            e = addAndSubtract(e);

            // Save the result
            CalculatorHelper.LastResult = e;
            
            return e;
        }

        private string checker(string expression)
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

            // Find 'ans'
            if (expression.IndexOf("ans") != -1)
            {
                // replace 'ans'
                expression = expression.Substring(0,
                    expression.IndexOf("ans")) + CalculatorHelper.LastResult +
                    expression.Substring(expression.IndexOf("ans") + 3);
            }
            
            // Find x=???
            string setXValuePattern = @"([x])([=])([-]?\d+\.?\d*)";

            if (Regex.IsMatch(expression, setXValuePattern))
            {
                // Save the x value
                CalculatorHelper.XValue = Regex.Match(expression, setXValuePattern).Groups[3].Value;
                expression = CalculatorHelper.XValue;
            }

            // Find x
            if (expression.IndexOf("x") != -1)
            {
                expression = expression.Substring(0, expression.IndexOf("x")) + CalculatorHelper.XValue +
                             expression.Substring(expression.IndexOf("x") + 1);
            }

            // Define a regular expression pattern to validate the expression
            string pattern = @"^[()\d+\-*/. ]+$";
            // Check if the expression matches the defined pattern
            if (!Regex.IsMatch(expression, pattern))
            {
                // Throw exception if the expression contains invalid characters
                throw new Exception("Invalid expression: " + expression);
            }

            return expression;
        }

        private string processBrackets(string expression)
        {
            // To find the opening bracket in the expression
            int start = expression.IndexOf('(');
            int end = -1;

            // If no opening bracket, return expression as is (no calculation)
            if (start == -1)
            {
                return expression;
            }

            int bracketDepth = 0;

            // Loop expression staring from opening bracket
            for (int i = start; i < expression.Length; i++)
            {
                // Tracks nested bracketDepth
                // To make sure we have one opening and one closing bracket
                if (expression[i] == '(')
                {
                    bracketDepth++;
                }
                else if (expression[i] == ')')
                {
                    bracketDepth--;
                }

                // To find the correct closing bracket position
                if (end == -1 && expression[i] == ')' && bracketDepth == 0)
                {
                    end = i;
                }
            }

            if (bracketDepth != 0)
            {
                throw new Exception("Invalid expression with wrong brackets: " + expression);
            }

            // Evaluate expression inside the matching () brackets
            if (start != -1 && end != -1 && start < end)
            {
                return new Calculator(expression.Substring(0, start)
                                      // Recuring expressions until no brackets
                                      + new Calculator(expression.Substring(start + 1, end - start - 1)).Result
                                          .ToString() + expression.Substring(end + 1)).Result.ToString();
            }
            else
            {
                // if no valid brackets, nothing to process, return the expression unchanged
                return expression;
            }
        }

        private string multiplyAndDivide(string expression)
        {
            // Define a pattern to find multiplication or division
            string pattern = @"([-]?\d+\.?\d*)([*/])([-]?\d+\.?\d*)";

            // Frind the first match in the expression
            Match match = Regex.Match(expression, pattern);

            // loops while there are matches
            while (match.Success)
            {
                // Gets the left number
                double left = double.Parse(match.Groups[1].Value);

                // Gets the right number 
                double right = double.Parse(match.Groups[3].Value);

                // Store the results
                double result = 0.0f;

                // Checks if it's multiplication then calculates the products
                if (match.Groups[2].Value == "*")
                {
                    result = left * right;
                }

                // Checks if it's division then calculates the quotient 
                else if (match.Groups[2].Value == "/")
                {
                    result = left / right;
                }

                // Replace the operation with the result in the expression
                expression = expression.Replace(match.Groups[1].Value + match.Groups[2].Value + match.Groups[3].Value,
                    result.ToString());

                // Find the next match
                match = Regex.Match(expression, pattern);
            }

            // Find the next match
            return expression;
        }

        private string addAndSubtract(string expression)
        {
            // A pattern to find addition or sutraction
            string pattern = @"([-]?\d+\.?\d*)([+-])([-]?\d+\.?\d*)";

            // uses the expresssion string to search for a substring that matches the pattern
            Match match = Regex.Match(expression, pattern);

            // loop while match is successful
            while (match.Success)
            {
                // converts the left number from string to double
                double left = double.Parse(match.Groups[1].Value);
                // converts the right number from string to double
                double right = double.Parse(match.Groups[3].Value);
                // A variable to hold the result of addition or subtraction
                double result = 0.0f;

                // checks if the operator is a plus sign and adds accordingly
                if (match.Groups[2].Value == "+")
                {
                    result = left + right;
                }
                // check if the operator captured is a negative sign and subtracts accordingly
                else if (match.Groups[2].Value == "-")
                {
                    result = left - right;
                }

                // replaces the result, with the matched portion then convert it back to string
                expression = expression.Replace(match.Groups[1].Value + match.Groups[2].Value + match.Groups[3].Value,
                    result.ToString());
                match = Regex.Match(expression, pattern);
            }

            // close while loop
            return expression;
        }
    }
}