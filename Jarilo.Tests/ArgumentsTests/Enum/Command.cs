using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Enum
{
    [Command("arguments-tests enum", "Command for testing single enum argument.")]
    class Command
    {
        public static string Name { get; } = "arguments-tests enum";

        public void Run(Arguments arguments)
        {
            Console.WriteLine($"single enum argument: {arguments.Enum}");
        }
    }
}
