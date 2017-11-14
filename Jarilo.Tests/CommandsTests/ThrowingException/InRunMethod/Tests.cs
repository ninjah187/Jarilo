using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jarilo.Tests.CommandsTests.ThrowingException.InRunMethod
{
    public class Tests
    {
        [Fact]
        public void CustomException()
        {
            AppTest.Throws<CustomException>(Command.Name);
        }
    }
}
