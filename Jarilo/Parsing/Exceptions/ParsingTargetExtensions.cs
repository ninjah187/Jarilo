using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Parsing.Exceptions
{
    static class ParsingTargetExtensions
    {
        public static string Map(this ParsingTarget target)
        {
            switch (target)
            {
                case ParsingTarget.Argument:
                    return "argument";
                case ParsingTarget.Option:
                    return "option";
                default:
                    throw new InvalidOperationException(
                        $"{nameof(ParsingTarget)} to string conversion error. Unknown value: {target}.");
            }
        }
    }
}
