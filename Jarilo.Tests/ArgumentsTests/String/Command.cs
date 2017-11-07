using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.String
{
    [Command("arguments-tests string", "Command for testing single string argument.")]
    class Command
    {
        public static string Name { get; } = "arguments-tests string";

        public void Run(Arguments arguments)
        {
            Console.WriteLine($"single string argument: {arguments.String}");
        }
    }
}
