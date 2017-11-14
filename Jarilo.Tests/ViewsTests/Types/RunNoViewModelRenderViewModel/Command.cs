using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunNoViewModelRenderViewModel
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types run-returns-no-view-model-render-takes-view-model";

        public static string Message => "run method";

        public void Run()
        {
            Console.WriteLine(Message);
        }
    }
}
