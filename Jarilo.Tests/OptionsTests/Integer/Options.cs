using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Integer
{
    class Options
    {
        [Option("--integer", "Integer option.")]
        public int Integer { get; set; }
    }
}
