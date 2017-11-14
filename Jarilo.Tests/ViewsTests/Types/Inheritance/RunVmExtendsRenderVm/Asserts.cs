using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmExtendsRenderVm
{
    static class Asserts
    {
        public static void AssertView(this string[] output)
        {
            output.AssertLength(1);
            Assert.Equal(Command.Message, output[0]);
        }
    }
}
