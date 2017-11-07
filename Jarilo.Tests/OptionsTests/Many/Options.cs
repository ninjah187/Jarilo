using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Many
{
    class Options
    {
        [Option("--bool", "Bool option.")]
        public bool Bool { get; set; }

        [Option("--double", "Double option.")]
        public double Double { get; set; }

        [Option("--enum", "Enum option.")]
        public EnumValues Enum { get; set; }

        [Option("--integer", "Integer option.")]
        public int Integer { get; set; }

        [Option("--string", "String option.")]
        public string String { get; set; }
    }
}
