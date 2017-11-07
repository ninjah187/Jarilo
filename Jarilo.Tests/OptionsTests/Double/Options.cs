using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Double
{
    class Options
    {
        [Option("--double", "Double option.")]
        public double Double { get; set; }
    }
}
