using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.Models
{
    class Ninja
    {
        public string Name { get; }
        public int Health { get; }
        public int Attack { get; }
        public Weapon Weapon { get; }

        public Ninja(
            string name,
            int health,
            int attack,
            Weapon weapon)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Weapon = weapon;
        }
    }
}
