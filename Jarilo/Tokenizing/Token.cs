using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tokenizing
{
    abstract class Token
    {
    }

    abstract class Token<TValue> : Token
    {
        public TValue Value { get; }

        public Token(TValue value)
        {
            Value = value;
        }
    }
}
