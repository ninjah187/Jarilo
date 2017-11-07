using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.String
{
    public class Tests
    {
        [Fact]
        public async Task DefaultUsage()
        {
            var args = $"{Command.Name}";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(null);
            });
        }

        [Fact]
        public async Task SingleWord()
        {
            var value = "value";
            var args = $"{Command.Name} {value}";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(value);
            });
        }

        [Fact]
        public async Task ManyWords_WithoutQuotationMarks_OnlyFirstRead()
        {
            var value = "value1 value2";
            var args = $"{Command.Name} {value}";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument("value1");
            });
        }

        [Fact]
        public async Task ManyWords_InQuotationMarks_ReadAsOneValue()
        {
            var value = "value1 value2";
            var args = $"{Command.Name} \"{value}\"";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(value);
            });
        }

        [Fact]
        public async Task SingleWord_InQuotationMarks()
        {
            var value = "value";
            var args = $"{Command.Name} \"{value}\"";
            await AppTest.Run(args, output =>
            {
                output.AssertArgument(value);
            });
        }

        [Fact]
        public async Task NumericValue()
        {
            var args = $"{Command.Name} 47";
            await AppTest.Run(args, output =>
            {
                output.AssertArgument("47");
            });
        }
    }
}
