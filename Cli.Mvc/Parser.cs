using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Parser
    {
        // TODO: refactor at some point, so Parser is immutable and purely functional
        // (context can be passed in states between function calls)

        delegate void TokenOperation(char? value);

        public static ParsedParams Parse(IEnumerable<Token> tokens) => new Parser(tokens).Parse();

        enum Context
        {
            None,
            InsideQuotemark,
            ParsingOptionKey,
        }


        Context _currentContext;

        string _optionKey = "";
        string _value = "";

        readonly List<string> _arguments = new List<string>();
        readonly Dictionary<string, string> _options = new Dictionary<string, string>();

        readonly IEnumerable<Token> _tokens;

        public Parser(string args)
        {
            _tokens = new Tokenizer().Tokenize(args);
        }

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens;
        }

        public ParsedParams Parse()
        {
            foreach (var token in _tokens)
            {
                ParseToken(token)(token.Value);
            }

            return new ParsedParams(_arguments, new Params(_options));
        }

        TokenOperation ParseToken(Token token)
        {
            if (token.Type == TokenType.Char)
            {
                switch (_currentContext)
                {
                    case Context.None:             return AccumulateValue;
                    case Context.InsideQuotemark:  return AccumulateValue;
                    case Context.ParsingOptionKey: return AccumulateOptionKey;
                }
            }

            if (token.Type == TokenType.Whitespace)
            {
                switch (_currentContext)
                {
                    case Context.None:             return SaveValue;
                    case Context.InsideQuotemark:  return AccumulateValue;
                    case Context.ParsingOptionKey: return SwitchContext(Context.None);
                }
            }

            if (token.Type == TokenType.Dash)
            {
                switch (_currentContext)
                {
                    case Context.None:             return SwitchContext(Context.ParsingOptionKey);
                    case Context.InsideQuotemark:  return AccumulateValue;
                    case Context.ParsingOptionKey: return Noop;
                }
            }

            if (token.Type == TokenType.Quotemark)
            {
                switch (_currentContext)
                {
                    case Context.None:             return SwitchContext(Context.InsideQuotemark);
                    case Context.InsideQuotemark:  return SwitchContext(Context.None);
                    case Context.ParsingOptionKey: return Error;
                }
            }

            if (token.Type == TokenType.End)
            {
                return SaveValue;
            }

            throw new InvalidOperationException($"Unknown token type {token.Type} for value {token.Value}.");
        }

        void AccumulateValue(char? value)
        {
            _value += value;
        }

        void SaveValue(char? value)
        {
            if (_optionKey == "")
            {
                if (_value != "")
                {
                    _arguments.Add(_value);
                }
            }
            else
            {
                _options[_optionKey] = _value;
                _optionKey = "";
            }

            _value = "";
        }

        void AccumulateOptionKey(char? value)
        {
            _optionKey += value;
        }

        TokenOperation SwitchContext(Context context) => value =>
        {
            _currentContext = context;
        };

        void Noop(char? value)
        {
        }

        void Error(char? value)
        {
            throw new NotImplementedException();
        }
    }
}
