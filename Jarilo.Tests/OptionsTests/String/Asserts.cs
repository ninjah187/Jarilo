using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.OptionsTests.String
{
    static class Asserts
    {
        public static void AssertOption(this string[] output, string expectedValue)
        {
            Assert.Equal(1, output.Length);
            Assert.Equal(Command.Message(expectedValue), output[0]);
        }
    }
}
