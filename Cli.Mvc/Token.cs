using System;

namespace Cli.Mvc
{
    public class Token
    {
        public TokenType Type { get; }
        public char? Value { get; }

        public Token(TokenType type, char? value)
        {
            Type = type;
            Value = value;
        }
    }
}
