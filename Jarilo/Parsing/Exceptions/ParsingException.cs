using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Parsing.Exceptions
{
    class ParsingException : Exception
    {
        public ParsingTarget Target { get; }
        public string Name { get; }
        public string Value { get; }

        public ParsingException(
            ValueParsingException innerException,
            ParsingTarget target,
            string name)
            : base($"{target.Map()} parsing error. Name: {name}, value: {innerException.Value}.", innerException)
        {
            Target = target;
            Name = name;
            Value = innerException.Value;
        }
    }
}
