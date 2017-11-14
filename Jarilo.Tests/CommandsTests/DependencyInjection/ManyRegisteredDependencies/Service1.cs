using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.ManyRegisteredDependencies
{
    class Service1
    {
        public static string Message() => nameof(Service1);

        public void Write()
        {
            Console.WriteLine(Message());
        }
    }
}
