using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cli.Mvc
{
    class Tokenizer
    {
        static Token[] EndToken { get; } = new[] { new Token(TokenType.End, null) };

        public IEnumerable<Token> Tokenize(string parameters)
        {
            return parameters.Select(Tokenify).Concat(EndToken);
        }

        Token Tokenify(char value) => new Token(ClassifyToken(value), value);

        TokenType ClassifyToken(char value)
        {
            if (char.IsWhiteSpace(value))
            {
                return TokenType.Whitespace;
            }
            if (value == '-')
            {
                return TokenType.Dash;
            }
            if (value == '\"')
            {
                return TokenType.Quotemark;
            }
            return TokenType.Char;
        }
    }
}
