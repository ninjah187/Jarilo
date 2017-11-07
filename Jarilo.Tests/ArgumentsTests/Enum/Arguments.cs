using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Enum
{
    class Arguments
    {
        [Argument("Enum argument.")]
        public EnumValues Enum { get; set; }
    }
}
