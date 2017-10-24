using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.TeamAdd
{
    class Arguments
    {
        [Argument("Added ninja's name.")]
        public string Name { get; set; }
    }
}
