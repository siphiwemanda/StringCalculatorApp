using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorApp
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {

            if (HasCustomSeparator(numbers))
            {
                return Sum(GetNumbersString(numbers), GetCustomSeparator(numbers));
            }

            return Sum(numbers, new[] { ",", "\n" });
        }

        public string GetNumbersString(string definition)
        {
            return definition.Substring(FindEndOfDefinition(definition) + 1);
        }

        private int FindEndOfDefinition(string definition) { return definition.IndexOf('\n'); }

        public string[] GetCustomSeparator(string definition)
        {
            const int startOfDefinition = 2;

            var customSeparator =
                definition.Substring(startOfDefinition, FindEndOfDefinition(definition) - startOfDefinition);
            return customSeparator.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool HasCustomSeparator(string numbers)
        {

            return numbers.StartsWith("//");
        }

        private int Sum(string numbers, string[] separator)
        {
            var actualNumbers = RemoveNumbersOverThousand(SplitStringsIntoNumbers(numbers, separator));
            CheckForNegativeNumbers(actualNumbers);

            return actualNumbers.Sum();
        }

        private void CheckForNegativeNumbers(int[] actualNumbers)
        {
            if (actualNumbers.Any(i => i < 0))
            {
                throw new Exception($"negatives not allowed: {string.Join(",", actualNumbers.Where(i => i < 0))}");
            }
        }

        private IEnumerable<int> SplitStringsIntoNumbers(string numbers, string[] separator)
        {
            var splitNumbers = numbers.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return splitNumbers.Select(int.Parse);
        }

        private int[] RemoveNumbersOverThousand(IEnumerable<int> actualNumbers)
        {
            return actualNumbers.Where(i => i < 1001).ToArray();

        }
    }
}