using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Many
{
    public class Tests
    {
        [Fact]
        public async Task NoArguments_DefaultValues()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(0, EnumValues.None, 0, null);
            });
        }

        [Fact]
        public async Task InOrder_Success()
        {
            var args = $"{Command.Name} 0.47 value-1 47 string-value";
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(0.47, EnumValues.Value1, 47, "string-value");
            });
        }

        [Fact]
        public async Task InOrder_TooManyArguments_ExcessiveArgumentsSkipped()
        {
            var args = $"{Command.Name} 0.47 value-1 47 string-value more values 1 2 3 4.5";
            await AppTest.Run(args, output =>
            {
                output.AssertArguments(0.47, EnumValues.Value1, 47, "string-value");
            });
        }
    }
}
