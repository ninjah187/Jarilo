using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunViewModelRenderViewModel
{
    class View
    {
        public static string ViewModelMessage(string viewModel)
            => $"viewModel in view: {viewModel}";

        public void Render(string viewModel)
        {
            Console.WriteLine(ViewModelMessage(viewModel));
        }
    }
}
