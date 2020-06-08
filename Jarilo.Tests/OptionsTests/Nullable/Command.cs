using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Nullable
{
    [Command("options-tests nullable", "Command for testing nullable type option.")]
    class Command
    {
        public static string Name => "options-tests nullable";

        public static string Message(int? value)
            => $"nullable option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.Nullable));
        }
    }
}
