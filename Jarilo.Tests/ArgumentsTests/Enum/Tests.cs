using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Enum
{
    public class Tests
    {
        [Fact]
        public async Task DefaultUsage()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(EnumValues.None);
            });
        }

        [Fact]
        public async Task Single()
        {
            var args = $"{Command.Name} value-1";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(EnumValues.Value1);
            });
        }

        [Fact]
        public void Single_NotEnum_ThrowsException()
        {
            var args = $"{Command.Name} not-enum-value";
            AppTest.Throws<FormatException>(args);
        }

        [Fact]
        public async Task Many_FirstEnum_OnlyFirstRead()
        {
            var args = $"{Command.Name} value-1 any value 1 2 3";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(EnumValues.Value1);
            });
        }

        [Fact]
        public void Many_FirstNotEnum_ThrowsException()
        {
            var args = $"{Command.Name} any value-1 1 2 3";
            AppTest.Throws<FormatException>(args);
        }
    }
}
