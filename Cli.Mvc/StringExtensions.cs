using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public static class StringExtensions
    {
        // TODO: Implement as SplitBy(Func<char, bool> predicate)
        public static IEnumerable<string> SplitByUpperCase(this string input) => SplitBy(input, char.IsUpper);

        public static IEnumerable<string> SplitBy(this string input, Func<char, bool> predicate)
        {
            var accumulator = "";

            foreach (var character in input)
            {
                if (predicate(character))
                {
                    if (accumulator != "")
                    {
                        yield return accumulator;
                        accumulator = "";
                    }
                    accumulator += character;
                }
            }

            if (accumulator != "")
            {
                yield return accumulator;
            }
        }
    }
}
