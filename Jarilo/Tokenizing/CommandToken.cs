using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Tokenizing
{
    class CommandToken : Token<string>
    {
        public CommandToken(string value) : base(value)
        {
        }
    }
}
