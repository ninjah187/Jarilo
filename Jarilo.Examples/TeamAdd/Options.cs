using Jarilo.Examples.Models;
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

        [Option("--weapon", "Set added ninja's weapon type.")]
        public Weapon Weapon { get; set; } = Weapon.None;

        [Option("--items", "Set added ninja's additional equipment.")]
        public Item[] Items { get; set; } = new Item[0];
    }
}
