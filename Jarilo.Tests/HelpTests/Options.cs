using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.HelpTests.Enum
{
    class Options
    {
        [Option("--enum", "Enum option.")]
        public EnumValues Enum { get; set; }
    }
}
