using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.OptionsTests
{
    static class Asserts
    {
        public static string[] AssertParsingException(this string[] output, string name, string value)
        {
            output.AssertLength(1);
            Assert.Equal($"Incorrect value \"{value}\" of option \"{name}\".", output[0]);
            return output;
        }
    }
}
