using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Enum
{
    public class Tests
    {
        [Fact]
        public async Task NoOption_DefaultValue()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertOption(EnumValues.None);
            });
        }

        [Fact]
        public async Task Single_Success()
        {
            var args = $"{Command.Name} --enum value-1";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(EnumValues.Value1);
            });
        }

        [Fact]
        public async Task Many_FirstEnum_OnlyFirstRead()
        {
            var args = $"{Command.Name} --enum value-1 value-2 values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertOption(EnumValues.Value1);
            });
        }

        [Fact]
        public void Many_FirstNotEnum_ThrowsException()
        {
            var args = $"{Command.Name} --enum not-enum value-1 values 1 2 3";
            AppTest.Throws<FormatException>(args);
        }
    }
}
