using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tokenizing
{
    class HelpOptionToken : Token<string>
    {
        public HelpOptionToken(string value) : base(value)
        {
        }
    }
}
