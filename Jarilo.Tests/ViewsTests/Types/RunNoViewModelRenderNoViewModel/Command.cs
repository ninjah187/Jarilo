using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.RunNoViewModelRenderNoViewModel
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests default-usage";

        public void Run()
        {
        }
    }
}
