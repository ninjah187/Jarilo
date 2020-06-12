using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.HelpTests.Enum
{
    static class EnumInfo
    {
        internal const string Value1txt = "value-1";
        internal const string Value1Description = "Enum value 1.";
        internal const string Value2txt = "value-2";
        internal const string Value3txt = "value-3";
    }

    enum EnumValues
    {
        None,

        [Value(EnumInfo.Value1txt, EnumInfo.Value1Description)]
        Value1,

        [Value(EnumInfo.Value2txt, "")]
        Value2,

        [Value(EnumInfo.Value3txt, null)]
        Value3
    }
}
