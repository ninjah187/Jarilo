using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Parsing.Exceptions
{
    class ValueParsingException : Exception
    {
        public string Value { get; }

        public ValueParsingException(FormatException innerException, string value)
            : base($"Value parsing error. Value: {value}.", innerException)
        {
            Value = value;
        }
    }
}
