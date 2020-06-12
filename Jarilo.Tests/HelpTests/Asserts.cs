using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.HelpTests.Enum
{
    static class Asserts
    {
        public static void AssertText(this string[] output, int index, string expectedValue)
        {
            Assert.Equal(expectedValue, output[index].TrimStart());
        }
    }
}
