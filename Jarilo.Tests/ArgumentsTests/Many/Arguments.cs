using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Many
{
    class Arguments
    {
        [Argument("Double argument.")]
        public double Double { get; set; }

        [Argument("Enum argument.")]
        public EnumValues Enum { get; set; }

        [Argument("Integer argument.")]
        public int Integer { get; set; }

        [Argument("String argument.")]
        public string String { get; set; }
    }
}
