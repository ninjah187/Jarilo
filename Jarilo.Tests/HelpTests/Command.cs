using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.HelpTests.Enum
{
    [Command("help-tests enum", "Command for testing help of an enum.")]
    class Command
    {
        public static string Name => "help-tests enum";

        public static string Message(string value)
            => $"enum option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Enum.ToString()));
        }
    }
}
