using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmExtendsRenderVm
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types inheritance run-vm-extends-render-vm";

        public static string Message => "view-model";

        public ViewModel Run()
        {
            return new ViewModel
            {
                Message = Message
            };
        }
    }
}
