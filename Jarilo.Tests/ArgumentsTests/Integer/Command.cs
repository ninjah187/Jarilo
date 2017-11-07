using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Integer
{
    [Command("arguments-tests integer", "Command for testing single integer argument.")]
    class Command
    {
        public static string Name { get; } = "arguments-tests integer";

        public void Run(Arguments arguments)
        {
            Console.WriteLine($"single integer argument: {arguments.Integer}");
        }
    }
}
