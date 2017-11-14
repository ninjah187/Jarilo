using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests
{
    static class Asserts
    {
        public static string[] AssertLength(this string[] output, int expectedLength)
        {
            Assert.Equal(expectedLength, output.Length);
            return output;
        }
    }
}
