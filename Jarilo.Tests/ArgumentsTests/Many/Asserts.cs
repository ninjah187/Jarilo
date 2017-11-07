using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Many
{
    static class Asserts
    {
        public static void AssertArguments(
            this string[] output,
            double expectedDouble,
            EnumValues expectedEnum,
            int expectedInteger,
            string expectedString)
        {
            Assert.Equal(4, output.Length);
            Assert.Equal(Command.DoubleMessage(expectedDouble), output[0]);
            Assert.Equal(Command.EnumMessage(expectedEnum), output[1]);
            Assert.Equal(Command.IntegerMessage(expectedInteger), output[2]);
            Assert.Equal(Command.StringMessage(expectedString), output[3]);
        }
    }
}
