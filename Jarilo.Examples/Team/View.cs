using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Examples.Team
{
    class View
    {
        public void Render((string name, int? health)[] ninjas)
        {
            foreach (var ninja in ninjas)
            {
                var text = $"- {ninja.name}";
                if (ninja.health != null)
                {
                    text += $" (hp: {ninja.health})";
                }
                Console.WriteLine(text);
            }
        }
    }
}
