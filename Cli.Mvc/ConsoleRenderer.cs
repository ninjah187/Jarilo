using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class ConsoleRenderer : IRenderer
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
