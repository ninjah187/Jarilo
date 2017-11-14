using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DependencyInjection.ManyRegisteredDependencies
{
    [Command("commands-tests dependency-injection many-depdendencies", "Test command.")]
    class Command
    {
        public static string Name => "commands-tests dependency-injection many-depdendencies";

        readonly Service1 _service1;
        readonly Service2 _service2;

        public Command(Service1 service1, Service2 service2)
        {
            _service1 = service1;
            _service2 = service2;
        }

        public void Run()
        {
            _service1?.Write();
            _service2?.Write();
        }
    }
}
