using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Bool
{
    class Options
    {
        [Option("--bool", "Boolean option.")]
        public bool Bool { get; set; }
    }
}
