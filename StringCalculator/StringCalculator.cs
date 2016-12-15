using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGO.StringCalculatorKata
{
    public class StringCalculator
    {
        private char[] _supportedDelimiters = {',','\n' }; 
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int Add(string numbers)
        {
            int result = 0;
            if (IsHeaderPresent(numbers))
            {
                UpdateDelimiters(numbers);
                numbers = RemoveHeader(numbers);
            }

            if (string.IsNullOrEmpty(numbers)) {return 0;}

            string[] splittedNumbers = SplitString(numbers);
            int[] numbersToSum = Array.ConvertAll(splittedNumbers, int.Parse);

            int[] negativeNumbersFromArray = numbersToSum.Where(x => x < 0).ToArray();
            if (negativeNumbersFromArray.Length != 0)
                throw new Exception(string.Format("Negatives not allowed: {0}",negativeNumbersFromArray.ToString()));


            for (int i = 0; i < numbersToSum.Length; i++)
            {
                result += numbersToSum[i]; 
            }

            return result;
        }

        public string[] SplitString(string stringToSplit)
        {
            return stringToSplit.Split(_supportedDelimiters);
        }

        public bool IsHeaderPresent(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                return true;
            }
            return false;
        }

        public void UpdateDelimiters(string numbers) 
        {
            _supportedDelimiters = new char[1];
            _supportedDelimiters[0] = numbers[2];
        }

        public string RemoveHeader(string numbers)
        {
            return numbers.Remove(0, 4);
        }

    }
}
