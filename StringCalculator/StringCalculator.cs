using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DGO.StringCalculatorKata
{
    public class StringCalculator
    {
        private List<string> _supportedDelimiters; 
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public StringCalculator()
        {
            _supportedDelimiters = new List<string>();
        }
        public int Add(string numbers)
        {
            int result = 0;
            if (string.IsNullOrEmpty(numbers)) { return 0; }

            if (IsHeaderPresent(numbers))
            {
                GetDelimitersFromHeader(numbers);
                numbers = RemoveHeader(numbers);
            }
            else
                UseDefaultDelimiters();

            int[] numbersToSum = GetNumbersFromDelimitersBasedString(numbers);

            if (HasOnlyPositiveValues(numbersToSum))
            {
                result = SumIntArray(numbersToSum);
            }
            
            return result;
        }

        private void UseDefaultDelimiters()
        {
            _supportedDelimiters.Add(",");
            _supportedDelimiters.Add("\n");
        }

        private bool HasOnlyPositiveValues(int[] numbersToSum)
        {
            int[] negativeNumbersFromArray = numbersToSum.Where(x => x < 0).ToArray();
            if (negativeNumbersFromArray.Length != 0)
                throw new Exception(string.Format("Negatives not allowed: {0}", negativeNumbersFromArray.ToString()));
            
            return true;
        }

        public int SumIntArray(int[] numbersToSum)
        {
            int result = 0;
            for (int i = 0; i < numbersToSum.Length; i++)
            {
                if (numbersToSum[i] < 1000)
                    result += numbersToSum[i];
            }
            return result;
        }
        
        public string[] SplitString(string stringToSplit)
        {
            return stringToSplit.Split(_supportedDelimiters.ToArray(),StringSplitOptions.RemoveEmptyEntries);
        }

        public bool IsHeaderPresent(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return true;
            }
            return false;
        }

        public void GetDelimitersFromHeader(string numbers) 
        {
            _supportedDelimiters.Add(numbers[2].ToString());
        }

        public string RemoveHeader(string numbers)
        {
            return numbers.Remove(0, 4);
        }

        public int[] GetNumbersFromDelimitersBasedString(string numbers)
        {
            string[] splittedNumbers = SplitString(numbers);
            return Array.ConvertAll(splittedNumbers, int.Parse); 
        }
    }
}
