using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tests.ArgumentsTests.Many
{
    [Command("arguments-tests many", "Command for testing many arguments without options.")]
    class Command
    {
        public static string Name => "arguments-tests many";

        public static string DoubleMessage(double value)
            => $"double argument: {value}";

        public static string EnumMessage(EnumValues value)
            => $"enum argument: {value}";

        public static string IntegerMessage(int value)
            => $"integer argument: {value}";

        public static string StringMessage(string value)
            => $"string argument: {value}";

        public void Run(Arguments arguments)
        {
            Console.WriteLine(DoubleMessage(arguments.Double));
            Console.WriteLine(EnumMessage(arguments.Enum));
            Console.WriteLine(IntegerMessage(arguments.Integer));
            Console.WriteLine(StringMessage(arguments.String));
        }
    }
}
