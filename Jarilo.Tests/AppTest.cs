using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jarilo.Tests
{
    public class AppTest
    {
        public static Task Run(string args, Action<string[]> assert)
            => ArrangeAndRun(args, app => {}, assert);

        public static async Task ArrangeAndRun(string args, Action<App> arrange, Action<string[]> assert)
        {
            var app = CreateTestApp();
            arrange(app);
            Action act = () =>
            {
                app.Run(args.Split(" "));
            };
            var output = await ConsoleOut.Collect(act);
            assert(output);
        }

        public static void Throws<TException>(string args)
            where TException : Exception
        {
            var app = CreateTestApp();
            Action act = () =>
            {
                app.Run(args.Split(" "));
            };
            Assert.Throws<TException>(act);
        }

        static App CreateTestApp()
        {
            var appTypes = Assembly.GetExecutingAssembly().GetTypes();
            var app = new App(appTypes);
            return app;
        }
    }
}
