using System;

namespace UnitTestingDemo
{
    public class Calculator
    {
        public decimal Addition(string first, string second)
        {
            if (!decimal.TryParse(first, out decimal firstNumber))
            {
                throw new ArgumentException($"{first} is not a valid number", nameof(first));
            }
            if (!decimal.TryParse(second, out decimal secondNumber))
            {
                throw new ArgumentException($"{second} is not a valid number", nameof(second));
            }

            return firstNumber + secondNumber;
        }
    }
}
