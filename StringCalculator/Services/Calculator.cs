using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator.Services
{
    public interface ICalculator
    {
        public int Calculate(string input);
    }

    public class Calculator : ICalculator
    {
        public List<char> Delimiters = new List<char> {',','\n'};
        public int Calculate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            input = FormatNewLineCharacters(input);

            string customDelimiters = GetCustomDelimiters(input);

            foreach (char delimiter in customDelimiters)
            {
                Delimiters.Add(delimiter);
            }

            if (!string.IsNullOrEmpty(customDelimiters) && input.Contains("\n"))
            {
                var expression = GetExpression(input);

                foreach (char delimiter in Delimiters)
                {
                    input = expression.Replace(delimiter, ',');
                }
            }

            int sum = 0;

            try
            {
                Array.ForEach(input.Split(Delimiters.ToArray()), s => sum += int.Parse(s));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
            return sum;
        }

        public string FormatNewLineCharacters(string input)
        {
            // Entering \n in the text box results in the escaped input \\n
            // There are more sophisticated ways to handle this, but for simplicity, just changing this back  
            return Regex.Replace(input, @"\\n", "\n");
        }

        private static string GetExpression(string input)
        {
            string expression = "";
            int index = input.IndexOf('\n');
            if (index > 0)
                expression = input.Substring(index + 1);
            return expression;
        }


        public string GetCustomDelimiters(string input)
        {
            if (input.StartsWith("//") && input.Contains("\n"))
            {
                input = input.Substring(2);
                int index = input.IndexOf('\n');
                if (index > 0)
                    input = input.Substring(0, index);

                return input;
            }

            return "";
        }
    }
}
