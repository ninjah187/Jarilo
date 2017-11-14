using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmImplementsRenderVm
{
    class View
    {
        public void Render(IViewModel viewModel)
        {
            Console.WriteLine(viewModel.Message);
        }
    }
}
