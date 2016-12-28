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

            string header = string.Empty;

            if (IsHeaderPresent(numbers))
            {
                int endOfHeaderIndex = GetIndexOfFirstNumber(numbers);
                header = numbers.Substring(0, endOfHeaderIndex);
                numbers = numbers.Substring(endOfHeaderIndex);
                GetDelimitersFromHeader_recursiv(header);
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

        private void GetDelimitersFromHeader_recursiv(string header)
        {
            if (header.StartsWith("//"))
            {
                header = header.Remove(0, 2);
            }

            if ('[' == header[0])
            {
                header = header.Remove(0, 1);
                //Brackets delimited delimiters
                bool continueLookingForHeaders = true;
                while (continueLookingForHeaders)
                {
                    int nextDelimiterIndex = header.IndexOf("][");
                    int endOfHeader = header.IndexOf("]\n");

                    if (nextDelimiterIndex > 0)
                    {
                        _supportedDelimiters.Add(header.Substring(0, nextDelimiterIndex));
                        header = header.Remove(0, nextDelimiterIndex + 2);
                    }
                    else
                    {
                        _supportedDelimiters.Add(header.Substring(0, endOfHeader));
                        continueLookingForHeaders = false;
                    }
                }
            }
            else 
            {
                _supportedDelimiters.Add(header[0].ToString());
            }
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

        public int GetIndexOfFirstNumber(string numbers)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(numbers);

            return match.Index;
        }

        public int[] GetNumbersFromDelimitersBasedString(string numbers)
        {
            string[] splittedNumbers = SplitString(numbers);
            return Array.ConvertAll(splittedNumbers, int.Parse); 
        }
    }
}
