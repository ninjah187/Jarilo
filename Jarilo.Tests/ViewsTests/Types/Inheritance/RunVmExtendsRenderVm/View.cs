using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.Inheritance.RunVmExtendsRenderVm
{
    class View
    {
        public void Render(ViewModelBase viewModel)
        {
            Console.WriteLine(viewModel.Message);
        }
    }
}
