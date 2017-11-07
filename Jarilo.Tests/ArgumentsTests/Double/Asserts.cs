using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Double
{
    static class Asserts
    {
        public static string[] AssertArgument(this string[] output, double expectedValue)
        {
            Assert.Equal(1, output.Length);
            Assert.Equal($"single double argument: {expectedValue}", output[0]);
            return output;
        }
    }
}
