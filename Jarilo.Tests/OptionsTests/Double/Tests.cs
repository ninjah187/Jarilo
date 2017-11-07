using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Double
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
            var args = $"{Command.Name} --double 0.47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(0.47);
            });
        }

        [Fact]
        public async Task CommaSeparator_Success()
        {
            var args = $"{Command.Name} --double 0,47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(0.47);
            });
        }

        [Fact]
        public async Task Many_FirstDouble_OnlyFirstRead()
        {
            var args = $"{Command.Name} --double 0.47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(0.47);
            });
        }

        [Fact]
        public void Many_FirstNotDouble_ThrowsException()
        {
            var args = $"{Command.Name} --double not-double 0.47 any values 1 2 3";
            AppTest.Throws<FormatException>(args);
        }
    }
}
