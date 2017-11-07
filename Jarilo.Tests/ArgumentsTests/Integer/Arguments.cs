using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Integer
{
    class Arguments
    {
        [Argument("Integer argument.")]
        public int Integer { get; set; }
    }
}
