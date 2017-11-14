using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.Types.RunViewModelRenderNoViewModel
{
    class View
    {
        public static string Message => "no-view-model";

        public void Render()
        {
            Console.WriteLine(Message);
        }
    }
}
