using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Double
{
    static class Asserts
    {
        public static string[] AssertOption(this string[] output, double expectedValue)
        {
            Assert.Equal(1, output.Length);
            Assert.Equal(Command.Message(expectedValue), output[0]);
            return output;
        }
    }
}
