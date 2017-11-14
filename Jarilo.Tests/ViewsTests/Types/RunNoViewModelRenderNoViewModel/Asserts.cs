using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.ViewsTests.RunNoViewModelRenderNoViewModel
{
    static class Asserts
    {
        public static string[] AssertView(this string[] output)
        {
            output.AssertLength(1);
            Assert.Equal(View.Message, output[0]);
            return output;
        }
    }
}
