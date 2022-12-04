using System;

namespace Cli.Mvc.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            new Documentation().Customize();

            // var command = "ninja list --help";
            // var command = "ninja add karol --hp 1000";
            var command = "ninja add --hp 1000";

            Console.WriteLine($"> {command}");

            // new AppBuilder().Build().Run("ninja add Karol --hp 1000");
            // new AppBuilder().Build().Run("ninja list");

            new AppBuilder().Build().Run(command);

            Console.ReadKey();
        }
    }
}
