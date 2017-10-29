using Jarilo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jarilo.Tokenizing
{
    class Tokenizer
    {
        static string[] HelpOptionNames = new[] { "-?", "-h", "--help" };

        public IEnumerable<Token> Tokenize(AppMetadata appMetadata, string[] args)
        {
            var joinedArgs = string.Join(' ', args);
            var inputCommandName = appMetadata
                .Commands
                .Select(command => command.Name)
                .OrderByDescending(commandName => commandName.Length)
                .FirstOrDefault(commandName => joinedArgs.StartsWith(commandName));
            if (inputCommandName != null)
            {
                yield return new CommandToken(inputCommandName);
            }
            args = joinedArgs
                .Remove(0, inputCommandName?.Length ?? 0)
                .TrimStart(' ')
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var arg in args)
            {
                if (HelpOptionNames.Contains(arg))
                {
                    yield return new HelpOptionToken(arg);
                    continue;
                }
                if (arg.StartsWith("-"))
                {
                    yield return new OptionToken(arg);
                    continue;
                }
                yield return new ValueToken(arg);
            }
        }
    }
}
