using Jarilo.Examples.Services;
using System;

namespace Jarilo.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            app.Services.AddSingleton<NinjaTeam>();
            app.ReadEvalPrintLoop();
        }
    }
}
