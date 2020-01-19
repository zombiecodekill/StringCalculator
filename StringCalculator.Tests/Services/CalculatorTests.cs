using System;
using NUnit.Framework;
using Calculator = StringCalculator.Services.Calculator;

namespace StringCalculator.Tests.Services
{
    public class CalculatorTests
    {
        private Calculator _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Calculate_EmptyString_Returns0()
        {
            Assert.That(_calculator.Calculate(""), Is.EqualTo(0));
        }

        [TestCase("123",123)]
        [TestCase("123456", 123456)]
        public void Calculate_SingleNumber_ReturnsNumber(string input, int output)
        {
            Assert.That(_calculator.Calculate(input), Is.EqualTo(output));
        }

        [TestCase("1,2", 3)]
        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4", 10)]
        public void Calculate_MultipleCommaSeparatedNumbers_ReturnsSumOfTheNumbers(string input, int output)
        {
            Assert.That(_calculator.Calculate(input), Is.EqualTo(output));
        }

        [TestCase("1\\n2", 3)]
        [TestCase("1\\n2\\n3", 6)]
        [TestCase("1\\n2\\n3\\n4", 10)]
        public void Calculate_MultipleNewlineSeparatedNumbers_ReturnsSumOfTheNumbers(string input, int output)
        {
            // Act
            var result = _calculator.Calculate(input);

            // Assert
            Assert.That(result, Is.EqualTo(output));
        }

        [TestCase("1\n2,3", 6)]
        public void Calculate_NewLineAndCommaSeparators_ReturnsSumOfTheNumbers(string input, int output)
        {
            // Act
            var result = _calculator.Calculate(input);

            // Assert
            Assert.That(result, Is.EqualTo(output));
        }

        [Test]
        public void Calculate_ConsecutiveDelimiters_ThrowsArgumentException()
        {
            void Calculate() => _calculator.Calculate("1,\n2");
            Assert.Throws<ArgumentException>(Calculate, "Input string was not in a correct format.");
        }

        [TestCase("//*\n1*2", 3)]
        [TestCase("//*\n1*2*3", 6)]
        [TestCase("//*\n1*2*3*4", 10)]
        public void Calculate_ChangeSingleDelimiter_ReturnsSumOfTheNumbers(string input, int output)
        {
            Assert.That(_calculator.Calculate(input), Is.EqualTo(output));
        }

        [TestCase("//*%\n1*2%3", 6)]
        [TestCase("//*&^\n1*2&3^4", 10)]
        [TestCase("//*&^\n1*2&3^4*5", 15)]
        public void Calculate_ChangeMultipleDelimiters_ReturnsSumOfTheNumbers(string input, int output)
        {
            Assert.That(_calculator.Calculate(input), Is.EqualTo(output));
        }


        [Test]
        public void GetCustomDelimiters_WithoutNewLine_ReturnsEmptyString()
        {
            Assert.That(_calculator.GetCustomDelimiters("//*&^1*2&3^4"), Is.EqualTo(""));
        }

        [Test]
        public void GetCustomDelimiters_StartsWithDoubleForwardSlashAndHasNewLine_ReturnsSubstringBetweenDoubleForwardSlashAndNewline()
        {
            Assert.That(_calculator.GetCustomDelimiters("//*&^\n1*2&3"), Is.EqualTo("*&^"));
        }

        [TestCase("\\n1\\n2", "\n1\n2")]
        public void FormatNewLineCharacters_EscapedNewLineCharacters_AreAllReplaced(string input, string output)
        {
            var result = _calculator.FormatNewLineCharacters(input);

            Assert.That(result, Is.EqualTo(output));
        }
    }
}