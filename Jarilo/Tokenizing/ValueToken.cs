using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tokenizing
{
    class ValueToken : Token<string>
    {
        public ValueToken(string value) : base(value)
        {
        }
    }
}
