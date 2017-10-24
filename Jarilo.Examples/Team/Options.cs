using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.Team
{
    class Options
    {
        [Option("--all", "Lists detailed info about your ninjas.")]
        public bool All { get; set; }
    }
}
