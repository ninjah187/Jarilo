using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.OptionsTests.Many
{
    public class Tests
    {
        [Fact]
        public async Task NoOptions_DefaultValues()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(false, 0, EnumValues.None, 0, null);
            });
        }

        [Fact]
        public async Task InOrder_Success()
        {
            var args = $"{Command.Name} --bool --double 0.47 --enum value-1 --integer 47 --string string-value";
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(true, 0.47, EnumValues.Value1, 47, "string-value");
            });
        }

        [Fact]
        public async Task OrderDoesntMatter_Success()
        {
            var args = $"{Command.Name} --enum value-1 --string string-value --bool --integer 47 --double 0.47";
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(true, 0.47, EnumValues.Value1, 47, "string-value");
            });
        }

        [Fact]
        public async Task NotRecognizedOptionsSkipped()
        {
            var args = $"{Command.Name} --bool --double 0.47 --enum value-1 --integer 47 --string string-value --other-option some-value";
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(true, 0.47, EnumValues.Value1, 47, "string-value");
            });
        }
    }
}
