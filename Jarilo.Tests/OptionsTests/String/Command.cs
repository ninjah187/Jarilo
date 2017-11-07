using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.String
{
    [Command("options-tests string", "Command for testing single string option.")]
    class Command
    {
        public static string Name => "options-tests string";

        public static string Message(string expectedValue)
            => $"string option: {expectedValue}";

        public void Run(Options options)
        {
            Console.WriteLine(Message(options.String));
        }
    }
}
