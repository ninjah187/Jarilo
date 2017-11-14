using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.CommandsTests.DefaultUsage
{
    [Command("commands-tests default-usage", "Command for testing simplest command possible.")]
    class Command
    {
        public static string Name => "commands-tests default-usage";

        public static string Message() => "ok";

        public void Run()
        {
            Console.WriteLine(Message());
        }
    }
}
