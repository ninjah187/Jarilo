using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunNoViewModelRenderViewModel
{
    class View
    {
        public static string NoViewModelMessage => "[no-view-model]";

        public void Render(string viewModel)
        {
            viewModel = viewModel ?? NoViewModelMessage;
            Console.WriteLine(viewModel);
        }
    }
}
