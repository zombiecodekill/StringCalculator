# StringCalculator
ASP.NET Core 3.1 String Calculator

This calculator can take up to two numbers, separated by commas, and will return their sum.

For an empty string it will return 0

It accepts newline characters in place of a comma, as a delimiter. For example “1\n2,3” returns 6. However "1,\n2” is invalid.

You can also specify your own delimiters by beginning the input with //

//*\n1*2 will return 3

//*%\n1*2%3 will return 6
