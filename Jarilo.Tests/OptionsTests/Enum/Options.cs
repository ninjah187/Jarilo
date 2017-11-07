using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Enum
{
    class Options
    {
        [Option("--enum", "Enum option.")]
        public EnumValues Enum { get; set; }
    }
}
