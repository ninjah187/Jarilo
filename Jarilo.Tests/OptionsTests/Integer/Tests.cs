using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Integer
{
    public class Tests
    {
        [Fact]
        public async Task NoOption_DefaultValue()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertOption(0);
            });
        }

        [Fact]
        public async Task DotSeparator_Success()
        {
            var args = $"{Command.Name} --integer 47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(47);
            });
        }

        [Fact]
        public async Task CommaSeparator_Success()
        {
            var args = $"{Command.Name} --integer 47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(47);
            });
        }

        [Fact]
        public async Task Many_FirstInteger_OnlyFirstRead()
        {
            var args = $"{Command.Name} --integer 47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(47);
            });
        }

        [Fact]
        public async Task Many_FirstNotInteger_ThrowsException()
        {
            var args = $"{Command.Name} --integer not-integer 47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("--integer", "not-integer");
            });
        }
    }
}
