using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Nullable
{
    class Options
    {
        [Option("--nullable", "Nullable option.")]
        public int? Nullable { get; set; }
    }
}
