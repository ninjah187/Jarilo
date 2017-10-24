using Jarilo.Metadata;
using Jarilo.Tokenizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jarilo.Parsing
{
    class CommandParser
    {
        public CommandMetadata Parse(AppMetadata appMetadata, Token[] tokens)
        {
            var commandName = ParseCommandName(tokens);
            var metadata = appMetadata
                .Commands
                .First(command => command.Name == commandName);
            return metadata;
        }

        string ParseCommandName(Token[] tokens)
            => tokens
                .OfType<CommandToken>()
                .First()
                .Value;
    }
}
