using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ViewsTests.Types.RunNoViewModelRenderViewModel
{
    static class Asserts
    {
        public static string[] AssertView(this string[] output)
        {
            output.AssertLength(2);
            Assert.Equal(Command.Message, output[0]);
            Assert.Equal(View.NoViewModelMessage, output[1]);
            return output;
        }
    }
}
