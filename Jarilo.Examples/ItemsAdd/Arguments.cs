using Jarilo.Examples.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.ItemsAdd
{
    class Arguments
    {
        [Argument("Ninja's name to add items.")]
        public string Name { get; set; }

        [Argument("Added items.")]
        public Item[] Items { get; set; } = new Item[0];
    }
}
