using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmCannotBeConvertedToRenderVm
{
    [Command(Name, "Test command.")]
    [View(typeof(View))]
    class Command
    {
        public const string Name = "views-tests types inheritance render-vm-extends-run-vm";
        
        public ViewModel Run()
        {
            return new ViewModel();
        }
    }
}
