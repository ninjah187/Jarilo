using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.String
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
        public async Task SingleWord()
        {
            var args = $"{Command.Name} --string value";
            await AppTest.Run(args, output =>
            {
                output.AssertOption("value");
            });
        }

        [Fact]
        public async Task ManyWords_WithoutQuotationMarks_OnlyFirstRead()
        {
            var args = $"{Command.Name} --string value1 value2";
            await AppTest.Run(args, output =>
            {
                output.AssertOption("value1");
            });
        }

        [Fact]
        public async Task ManyWords_InQuotationMarks_ReadAsOneValue()
        {
            var args = $"{Command.Name} --string \"value1 value2\"";
            await AppTest.Run(args, output =>
            {
                output.AssertOption("value1 value2");
            });
        }

        [Fact]
        public async Task SingleWord_InQuotationMarks()
        {
            var args = $"{Command.Name} --string \"value\"";
            await AppTest.Run(args, output =>
            {
                output.AssertOption("value");
            });
        }

        [Fact]
        public async Task NumericValue()
        {
            var args = $"{Command.Name} --string 47";
            await AppTest.Run(args, output =>
            {
                output.AssertOption("47");
            });
        }
    }
}
