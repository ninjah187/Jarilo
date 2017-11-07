using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jarilo.Tests
{
    public static class ConsoleOut
    {
        static readonly object ConsoleWriteLock = new object();

        public static async Task<string[]> Collect(Action collect)
        {
            var rawOutput = "";
            using (var writer = new StringWriter())
            {
                lock (ConsoleWriteLock)
                {
                    var oldOut = Console.Out;
                    Console.SetOut(writer);
                    collect();
                    Console.SetOut(oldOut);
                    rawOutput = writer.ToString();
                }
            }
            var output = await ReadOutput(rawOutput).ConfigureAwait(false);
            return output;
        }

        static async Task<string[]> ReadOutput(string rawOutput)
        {
            using (var reader = new StringReader(rawOutput))
            {
                var lines = new List<string>();
                while (true)
                {
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    if (line == null)
                    {
                        break;
                    }
                    lines.Add(line);
                }
                return lines.ToArray();
            }
        }
    }
}
