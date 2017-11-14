using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunViewModelRenderViewModel
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types run-returns-view-model-render-takes-view-model";

        public static string Message => "run method";
        public static string ViewModel => "view-model";

        public string Run()
        {
            Console.WriteLine(Message);
            return ViewModel;
        }
    }
}
