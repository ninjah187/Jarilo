using Jarilo.Examples.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.TeamAdd
{
    [Command("ninja team add", "Adds ninja to your team.")]
    class Command
    {
        readonly NinjaTeam _ninjaTeam;

        public Command(NinjaTeam ninjaTeam)
        {
            _ninjaTeam = ninjaTeam;
        }

        public void Run(Arguments arguments, Options options)
        {
            _ninjaTeam.Add(arguments.Name, options.Health, options.Attack, options.Weapon);
            Console.WriteLine($"ninja {arguments.Name} added");
        }
    }
}
