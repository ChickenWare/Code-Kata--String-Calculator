﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGO.StringCalculatorKata
{
    public class StringCalculator
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) {return 0;}

            return int.Parse(numbers);
        }
    }
}
