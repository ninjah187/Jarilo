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

        [Flags]
        enum QuotationMarks
        {
            None = 0,
            Start = 1 << 0,
            End = 1 << 1,
            Both = Start | End
        }

        public IEnumerable<Token> Tokenize(AppMetadata appMetadata, string[] args)
        {
            var joinedArgs = string.Join(" ", args);
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
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var valueAccumulator = new List<string>();
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
                var quotationMarks = QuotationMarks.None;
                quotationMarks |= arg.StartsWith("\"")
                    ? QuotationMarks.Start
                    : QuotationMarks.None;
                quotationMarks |= arg.EndsWith("\"")
                    ? QuotationMarks.End
                    : QuotationMarks.None;
                if (quotationMarks == QuotationMarks.None)
                {
                    yield return new ValueToken(arg);
                    continue;
                }
                var value = arg.Trim('\"');
                if (quotationMarks == QuotationMarks.Both)
                {
                    yield return new ValueToken(value);
                    continue;
                }
                valueAccumulator.Add(value);
                if (quotationMarks == QuotationMarks.End)
                {
                    value = string.Join(" ", valueAccumulator);
                    valueAccumulator.Clear();
                    yield return new ValueToken(value);
                }
            }
        }
    }
}
