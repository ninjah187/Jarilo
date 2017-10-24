using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tokenizing
{
    class OptionToken : Token<string>
    {
        public OptionToken(string value) : base(value)
        {
        }
    }
}
