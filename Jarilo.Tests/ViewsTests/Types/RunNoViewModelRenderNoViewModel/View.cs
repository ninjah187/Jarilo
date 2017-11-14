using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ViewsTests.RunNoViewModelRenderNoViewModel
{
    class View
    {
        public static string Message => "ok";

        public void Render()
        {
            Console.WriteLine(Message);
        }
    }
}
