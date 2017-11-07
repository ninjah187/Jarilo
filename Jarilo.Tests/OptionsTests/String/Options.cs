using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.String
{
    class Options
    {
        [Option("--string", "String option.")]
        public string String { get; set; }
    }
}
