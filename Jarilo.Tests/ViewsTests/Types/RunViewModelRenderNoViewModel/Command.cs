using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunViewModelRenderNoViewModel
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types run-returns-view-model-render-takes-no-view-model";

        public static string Message => "run method";

        public string Run()
        {
            Console.WriteLine(Message);
            return "view-model";
        }
    }
}
