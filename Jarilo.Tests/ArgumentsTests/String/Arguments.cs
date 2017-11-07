using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.String
{
    class Arguments
    {
        [Argument("String argument.")]
        public string String { get; set; }
    }
}
