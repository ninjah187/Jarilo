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
        public async Task Single_NotEnum_ErrorMessage()
        {
            var args = $"{Command.Name} not-enum-value";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("enum", "not-enum-value");
            });
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
        public async Task Many_FirstNotEnum_ErrorMessage()
        {
            var args = $"{Command.Name} any value-1 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("enum", "any");
            });
        }
    }
}
