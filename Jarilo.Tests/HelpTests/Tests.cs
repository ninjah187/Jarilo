using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests.HelpTests.Enum
{
    public class Tests
    {
        [Fact]
        public async Task ExpectedOutput()
        {
            var args = Command.Name + "--help";
            await AppTest.Run(args, output =>
            {
                output.AssertText(output.Length-1, EnumInfo.Value3txt);
                output.AssertText(output.Length-2, EnumInfo.Value2txt + " - ");
                output.AssertText(output.Length-3, EnumInfo.Value1txt + " - " + EnumInfo.Value1Description);
            });
        }

    }
}
