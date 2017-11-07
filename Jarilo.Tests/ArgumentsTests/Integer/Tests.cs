using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.ArgumentsTests.Integer
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
                    .AssertArgument(0);
            });
        }

        [Fact]
        public async Task Single()
        {
            var value = 47;
            var args = $"{Command.Name} {value}";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(value);
            });
        }

        [Fact]
        public void Single_NotInteger_ThrowsException()
        {
            var args = $"{Command.Name} string";
            AppTest.Throws<FormatException>(args);
        }

        [Fact]
        public async Task Many_FirstInteger_OnlyFirstRead()
        {
            var value = 47;
            var args = $"{Command.Name} {value} 8 4 some other values";
            await AppTest.Run(args, output =>
            {
                output
                    .AssertLength()
                    .AssertArgument(value);
            });
        }

        [Fact]
        public void Many_FirstNotInteger_ThrowsException()
        {
            var args = $"{Command.Name} string 1 2 3";
            AppTest.Throws<FormatException>(args);
        }
    }
}
