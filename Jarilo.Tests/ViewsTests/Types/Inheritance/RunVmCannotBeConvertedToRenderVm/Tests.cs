using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmCannotBeConvertedToRenderVm
{
    public class Tests
    {
        [Fact]
        public void ThrowsException()
        {
            AppTest.Throws<ArgumentException>(Command.Name);
        }
    }
}
