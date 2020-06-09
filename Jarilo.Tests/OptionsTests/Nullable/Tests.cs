using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Nullable
{
    public class Tests
    {
        [Fact]
        public async Task NoOption_DefaultValue()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertOption(null);
            });
        }

        [Fact]
        public async Task NormalUse_Success()
        {
            var args = $"{Command.Name} --nullable 47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(47);
            });
        }

        [Fact]
        public async Task Many_FirstInteger_OnlyFirstRead()
        {
            var args = $"{Command.Name} --nullable 47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(47);
            });
        }

        [Fact]
        public async Task Many_FirstNotInteger_ThrowsException()
        {
            var args = $"{Command.Name} --nullable not-integer 47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("--nullable", "not-integer");
            });
        }
    }
}
