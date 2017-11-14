using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DefaultUsage
{
    static class Asserts
    {
        public static void AssertDefaultUsage(this string[] output)
        {
            output.AssertLength(1);
            Assert.Equal(Command.Message(), output[0]);
        }
    }
}
