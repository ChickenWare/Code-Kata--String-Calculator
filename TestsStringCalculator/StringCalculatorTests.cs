using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DGO.StringCalculatorKata;

namespace DGO.TestsStringCalculatorKata
{
    [TestClass]
    public class StringCalculatorTests
    {
        StringCalculator StringCalculator;

        #region InitializeAndClean
        [TestInitialize]
        public void TestInitialize()
        {
            StringCalculator = new StringCalculator();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            StringCalculator.Dispose();
        }
        #endregion

        #region Individual Unit Tests
        [TestMethod]
        public void Test_Add_empty_zero()
        {
            int expected = 0;
            string input = string.Empty;

            int actual = StringCalculator.Add(input);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_Add_SingleNumber_NumberAsInt()
        {
            int[] expected = {1,2,15};
            string[] input = {"1","2","15"};

            for (int i = 0; i < input.Length; i++)
            {
                int actual = StringCalculator.Add(input[i]);
                Assert.AreEqual( expected[i],actual);
            }
        }
        
        [TestMethod]
        public void Test_Add_TwoCommaSeparatedNumbers_SumOfTheTwoNumbers()
        {
            int[] expected = { 2, 2, 21 };
            string[] input = { "1,1", "2", "15,6" };

            for (int i = 0; i < input.Length; i++)
            {
                int actual = StringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_MultpileCommaSeparatedNumbers_SumOfTheNumbers()
        {
            int[] expected = { 9};
            string[] input = { "1,1,1,2,4"};

            for (int i = 0; i < input.Length; i++)
            {
                int actual = StringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_MultpileNumberSpecialDelimiters_SumOfTheNumbers()
        {
            int[] expected = { 6};
            string[] input = { "1\n2,3"};

            for (int i = 0; i < input.Length; i++)
            {
                int actual = StringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_HeaderDelimiter_SumOfTheNumbers()
        {
            int[] expected = { 3 };
            string[] input = { "//;\n1;2" };

            for (int i = 0; i < input.Length; i++)
            {
                int actual = StringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        //[ExpectedException(typeof(ArgumentException), "A userId of null was inappropriately allowed.")]
        public void Test_Add_NegativeNumber_Exception() 
        {
            string input = "//;\n1;2;-5;2;-8";
            StringCalculator.Add(input);
        }

        [TestMethod]
        public void Test_Add_IgnoreNumberAbove1000_SumOfPositiveNumbersBelow1000()
        {
            string input = "//;\n1;2;1001";
            int expected = 3;
            Assert.AreEqual(expected, StringCalculator.Add(input));  
        }

        [TestMethod]
        public void Test_Add_MultipleCharDelimiter_SumOfNumbers()
        {
            string input = "//[***]\n1***2***3";
            int expected = 6;
            Assert.AreEqual(expected, StringCalculator.Add(input));
        }

        [TestMethod]
        public void Test_Add_SpecialCaseMultipleCharDelimiter_SumOfNumbers()
        {
            string input = "//[]]]]\n1]]]2]]]3";
            int expected = 6;
            Assert.AreEqual(expected, StringCalculator.Add(input));
        } 

        //[TestMethod]
        //public void Test_Add_SpecialCaseSingleDelimiter_SumOfNumbers()
        //{
        //    string input = "//[\n1[2[3";
        //    int expected = 6;
        //    Assert.AreEqual(expected, StringCalculator.Add(input));
        //}


        #endregion
        
    }
}
