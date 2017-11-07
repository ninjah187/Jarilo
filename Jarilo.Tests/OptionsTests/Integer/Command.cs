using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Integer
{
    [Command("options-tests integer", "Command for testing single integer option.")]
    class Command
    {
        public static string Name => "options-tests integer";

        public static string Message(int value)
            => $"integer option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Integer));
        }
    }
}
