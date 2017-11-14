using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.CommandsTests.UnknownCommand
{
    public class Tests
    {
        [Fact]
        public async Task UnknownCommand_PrintsCommunicate()
        {
            var args = "unknown command";
            await AppTest.Run(args, output =>
            {
                output.AssertLength(1);
                Assert.Equal("Unknown command.", output[0]);
            });
        }
    }
}
