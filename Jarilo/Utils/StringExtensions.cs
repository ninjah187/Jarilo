using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Utils
{
    static class StringExtensions
    {
        public static string FirstToLower(this string input)
        {
            if (input.Length == 0)
            {
                return input;
            }
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
