using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Enum
{
    [Command("options-tests enum", "Command for testing single option argument.")]
    class Command
    {
        public static string Name => "options-tests enum";

        public static string Message(EnumValues value)
            => $"enum option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Enum));
        }
    }
}
