using Jarilo.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jarilo.Examples.Services
{
    class NinjaTeam
    {
        public IReadOnlyCollection<Ninja> Ninjas => _ninjas;
        List<Ninja> _ninjas;

        public NinjaTeam()
        {
            _ninjas = new List<Ninja>
            {
                new Ninja("Sensei", 250, 25, Weapon.Katana)
            };
        }

        public void Add(string name, int health = 100, int attack = 10, Weapon weapon = Weapon.None)
        {
            _ninjas.Add(new Ninja(name, health, attack, weapon));
        }

        public void Remove(string name, bool caseSensitive = true)
        {
            var comparison = caseSensitive
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;
            var ninja = _ninjas.FirstOrDefault(n => string.Equals(n.Name, name, comparison));
            if (ninja == null)
            {
                return;
            }
            _ninjas.Remove(ninja);
        }

        public void Clear() => _ninjas.Clear();
    }
}
