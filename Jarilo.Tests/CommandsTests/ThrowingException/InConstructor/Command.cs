using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.ThrowingException.InConstructor
{
    [Command("commands-tests throwing-exception in-constructor", "Test command.")]
    class Command
    {
        public static string Name => "commands-tests throwing-exception in-constructor";

        public Command()
        {
            throw new CustomException();
        }

        public void Run()
        {
        }
    }
}
