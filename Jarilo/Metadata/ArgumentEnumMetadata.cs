using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class ArgumentEnumMetadata : ArgumentMetadata
    {
        public ValueMetadata[] PossibleValues { get; }

        public ArgumentEnumMetadata(
            string name,
            string description,
            PropertyInfo property,
            ValueMetadata[] possibleValues)
            : base(name, description, property)
        {
            PossibleValues = possibleValues;
        }
    }
}
