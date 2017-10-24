using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.TeamAdd
{
    class Options
    {
        [Option("--health", "Set added ninja's health.")]
        public int Health { get; set; } = 100;

        [Option("--attack", "Set added ninja's attack.")]
        public int Attack { get; set; } = 10;

        [Option("--weapons", "Set added ninja's weapons.")]
        public string[] Weapons { get; set; }
    }
}
