using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.CommandsTests.DefaultUsage
{
    public class Tests
    {
        [Fact]
        public async Task Success()
        {
            await AppTest.Run(Command.Name, output =>
            {
                output.AssertDefaultUsage();
            });
        }
    }
}
