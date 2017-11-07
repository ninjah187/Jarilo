using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.String
{
    static class Asserts
    {
        public static string[] AssertLength(this string[] output)
        {
            Assert.Equal(1, output.Length);
            return output;
        }

        public static string[] AssertArgument(this string[] output, string expectedValue)
        {
            Assert.Equal($"single string argument: {expectedValue}", output[0]);
            return output;
        }
    }
}
