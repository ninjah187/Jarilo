using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Many
{
    static class Asserts
    {
        public static void AssertArguments(
            this string[] output,
            bool expectedBool,
            double expectedDouble,
            EnumValues expectedEnum,
            int expectedInteger,
            string expectedString)
        {
            Assert.Equal(5, output.Length);
            Assert.Equal(Command.BoolMessage(expectedBool), output[0]);
            Assert.Equal(Command.DoubleMessage(expectedDouble), output[1]);
            Assert.Equal(Command.EnumMessage(expectedEnum), output[2]);
            Assert.Equal(Command.IntegerMessage(expectedInteger), output[3]);
            Assert.Equal(Command.StringMessage(expectedString), output[4]);
        }
    }
}
