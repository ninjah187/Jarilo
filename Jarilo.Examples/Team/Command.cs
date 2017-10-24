using Jarilo.Examples.Models;
using Jarilo.Examples.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jarilo.Examples.Team
{
    [Command("ninja team", "Adds ninja to your team.")]
    [View(typeof(View))]
    class Command
    {
        readonly NinjaTeam _ninjaTeam;

        public Command(NinjaTeam ninjaTeam)
        {
            _ninjaTeam = ninjaTeam;
        }

        public (string name, int? health)[] Run(Options options)
        {
            return _ninjaTeam
                .Ninjas
                .Select(ninja =>
                {
                    var name = ninja.Name;
                    var health = options.All
                        ? ninja.Health
                        : (int?) null;
                    return (name, health);
                })
                .ToArray();
        }
    }
}
