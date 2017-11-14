using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.ManyRegisteredDependencies
{
    static class Asserts
    {
        public static string[] AssertService1(this string[] output)
            => output.AssertService(Service1.Message());

        public static string[] AssertService2(this string[] output)
            => output.AssertService(Service2.Message());

        static string[] AssertService(this string[] output, string expectedOutput)
        {
            var serviceOutputExists = output.Contains(expectedOutput);
            Assert.Equal(true, serviceOutputExists);
            return output;
        }
    }
}
