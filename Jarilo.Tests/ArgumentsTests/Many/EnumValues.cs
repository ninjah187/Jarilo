using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Many
{
    enum EnumValues
    {
        None,

        [Value("value-1", "Enum value 1.")]
        Value1,

        [Value("value-2", "Enum value 2.")]
        Value2
    }
}
