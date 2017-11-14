using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.OneRegisteredDependency
{
    class Service
    {
        public static string Message() => nameof(Service);

        public void Write()
        {
            Console.WriteLine(Message());
        }
    }
}
