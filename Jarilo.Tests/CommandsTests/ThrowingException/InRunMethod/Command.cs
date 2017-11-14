using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.ThrowingException.InRunMethod
{
    [Command("commands-tests throwing-exception in-run-method", "Test command.")]
    class Command
    {
        public static string Name => "commands-tests throwing-exception in-run-method";

        public void Run()
        {
            throw new CustomException();
        }
    }
}
