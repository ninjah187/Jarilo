using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Double
{
    [Command("arguments-tests double", "Command for testing single double argument.")]
    class Command
    {
        public static string Name { get; } = "arguments-tests double";

        public void Run(Arguments arguments)
        {
            Console.WriteLine($"single double argument: {arguments.Double}");
        }
    }
}
