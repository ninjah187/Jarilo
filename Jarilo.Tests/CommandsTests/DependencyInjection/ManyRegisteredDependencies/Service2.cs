using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.ManyRegisteredDependencies
{
    class Service2
    {
        public static string Message() => nameof(Service2);

        public void Write()
        {
            Console.WriteLine(Message());
        }
    }
}
