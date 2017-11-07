using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Double
{
    [Command("options-tests double", "Command for testing single double option.")]
    class Command
    {
        public static string Name => "options-tests double";

        public static string Message(double value)
            => $"double option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Double));
        }
    }
}
