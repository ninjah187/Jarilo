using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ViewsTests.Types.RunViewModelRenderViewModel
{
    public class Tests
    {
        [Fact]
        public async Task Success()
        {
            await AppTest.Run(Command.Name, output =>
            {
                output.AssertView();
            });
        }
    }
}
