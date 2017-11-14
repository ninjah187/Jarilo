using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmImplementsRenderVm
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types inheritance run-vm-implements-render-vm";

        public static string Message => "view-model";

        public IViewModel Run()
        {
            return new ViewModel
            {
                Message = Message
            };
        }
    }
}
