using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.OneRegisteredDependency
{
    static class Asserts
    {
        public static string[] AssertLength(this string[] output, int expectedLength)
        {
            Assert.Equal(expectedLength, output.Length);
            return output;
        }

        public static string[] AssertService(this string[] output)
        {
            Assert.Equal(Service.Message(), output[0]);
            return output;
        }
    }
}
