using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cli.Mvc.Example
{
    public class NinjaListView : View
    {
        readonly string[] _ninjas;

        public NinjaListView(string[] ninjas)
        {
            _ninjas = ninjas;
        }

        public override void Render()
        {
            Renderer.WriteLine($"Total count: {_ninjas.Length}");

            foreach (var ninja in _ninjas.OrderBy(x => x))
            {
                Renderer.WriteLine($"- {ninja}");
            }
        }
    }
}
