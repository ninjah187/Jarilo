using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Integer
{
    static class Asserts
    {
        public static string[] AssertOption(this string[] output, int expectedValue)
        {
            Assert.Equal(1, output.Length);
            Assert.Equal(Command.Message(expectedValue), output[0]);
            return output;
        }
    }
}
