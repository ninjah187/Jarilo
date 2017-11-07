using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Integer
{
    static class Asserts
    {
        public static string[] AssertLength(this string[] output)
        {
            Assert.Equal(1, output.Length);
            return output;
        }

        public static string[] AssertArgument(this string[] output, int expectedValue)
        {
            Assert.Equal($"single integer argument: {expectedValue}", output[0]);
            return output;
        }
    }
}
