using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Cli.Mvc.Tests
{
    public class ParserTests
    {
        [Fact]
        public void CanParseValidArgumentsAndOptions()
        {
            var command = "img.png --width 128 --height 64";
            var parser = new Parser(command);

            var parsed = parser.Parse();

            Assert.Equal("img.png", parsed.Arguments[0]);
            Assert.Equal("128", parsed.Options["width"]);
            Assert.Equal("64", parsed.Options["height"]);
        }
    }
}
