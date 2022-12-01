using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class ParsedParams
    {
        public IReadOnlyList<string> Arguments { get; }
        public Params Options { get; }

        public ParsedParams(IReadOnlyList<string> arguments, Params options)
        {
            Arguments = arguments;
            Options = options;
        }
    }
}
