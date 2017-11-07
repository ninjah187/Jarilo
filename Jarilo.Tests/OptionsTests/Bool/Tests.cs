using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Bool
{
    public class Tests
    {
        [Fact]
        public async Task NoOption_DefaultValue()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertOption(false);
            });
        }

        [Fact]
        public async Task Exists_WithoutInputValue_TrueValue()
        {
            var args = $"{Command.Name} --bool";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(true);
            });
        }

        [Fact]
        public async Task Exists_WithInputValue_InputValueIgnoredAndTrueAsResult()
        {
            var args = $"{Command.Name} --bool value1 value2";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(true);
            });
        }
    }
}
