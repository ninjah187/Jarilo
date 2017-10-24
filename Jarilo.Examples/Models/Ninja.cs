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

        public Ninja(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            Attack = attack;
        }
    }
}
