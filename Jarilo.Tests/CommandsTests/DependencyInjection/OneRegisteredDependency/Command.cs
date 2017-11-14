using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.OneRegisteredDependency
{
    [Command("commands-tests dependency-injection one-depdendency", "Test command.")]
    class Command
    {
        public static string Name => "commands-tests dependency-injection one-depdendency";

        readonly Service _service;

        public Command(Service service)
        {
            _service = service;
        }

        public void Run()
        {
            _service?.Write();
        }
    }
}
