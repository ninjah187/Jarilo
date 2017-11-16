using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Double
{
    public class Tests
    {
        [Fact]
        public async Task DefaultUsage()
        {
            var args = Command.Name;
            await AppTest.Run(args, output =>
            {
                output.AssertArgument(0);
            });
        }

        [Fact]
        public async Task Single_WithDotSeparator_Success()
        {
            var args = $"{Command.Name} 0.47";
            await AppTest.Run(args, output =>
            {
                output.AssertArgument(0.47);
            });
        }

        [Fact(Skip = "Minor feature. To be implemented in future.")]
        public async Task Single_WithCommaSeparator_Succcess()
        {
            var args = $"{Command.Name} 0,47";
            await AppTest.Run(args, output =>
            {
                output.AssertArgument(0.47);
            });
        }

        [Fact]
        public async Task Single_NotDouble_ErrorMessage()
        {
            var args = $"{Command.Name} not-double";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("double", "not-double");
            });
        }

        [Fact]
        public async Task Many_FirstDouble_OnlyFirstRead()
        {
            var args = $"{Command.Name} 0.47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertArgument(0.47);
            });
        }

        [Fact]
        public async Task Many_FirstNotDouble_ErrorMessage()
        {
            var args = $"{Command.Name} not-double 0.47 any values 1 2 3";
            await AppTest.Run(args, output =>
            {
                output.AssertParsingException("double", "not-double");
            });
        }
    }
}
