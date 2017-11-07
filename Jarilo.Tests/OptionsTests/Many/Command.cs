using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.OptionsTests.Many
{
    [Command("options-tests many", "Command for testing many options.")]
    class Command
    {
        public static string Name => "options-tests many";

        public static string BoolMessage(bool value)
            => $"bool option: {value}";

        public static string DoubleMessage(double value)
            => $"double option: {value}";

        public static string EnumMessage(EnumValues value)
            => $"enum option: {value}";

        public static string IntegerMessage(int value)
            => $"integer option: {value}";

        public static string StringMessage(string value)
            => $"string option: {value}";

        public void Run(Options options)
        {
            Console.WriteLine(BoolMessage(options.Bool));
            Console.WriteLine(DoubleMessage(options.Double));
            Console.WriteLine(EnumMessage(options.Enum));
            Console.WriteLine(IntegerMessage(options.Integer));
            Console.WriteLine(StringMessage(options.String));
        }
    }
}
