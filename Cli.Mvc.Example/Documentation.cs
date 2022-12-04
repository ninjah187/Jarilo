using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc.Example
{
    public class Documentation : Docs
    {
        public void Customize()
        {
            Describe<NinjaController>("Control group of ninjas.");

            Describe<NinjaController>("List all ninjas.", controller => controller.List());

            Describe<NinjaController>("Add a ninja.", controller => controller
                .Add(
                    Description<string>(() => "Ninja name."),
                    Description<int>(() => "HP points.")
                ));

            Describe<NinjaController>("Remove a ninja.", controller => controller
                .Remove(
                    Description<string>(() => "Ninja name."),
                    Description<bool>(() => "Remove all ninjas if present.")
                ));
        }
    }
}
