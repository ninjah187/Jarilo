using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Bool
{
    [Command("options-tests bool", "Command for testing single bool option.")]
    class Command
    {
        public static string Name => "options-tests bool";

        public static string Message(bool value)
            => $"bool option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Bool));
        }
    }
}
