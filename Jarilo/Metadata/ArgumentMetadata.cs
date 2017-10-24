using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class ArgumentMetadata
    {
        public string Name { get; }
        public string Description { get; }
        public PropertyInfo Property { get; }

        public ArgumentMetadata(string name, string description, PropertyInfo property)
        {
            Name = name;
            Description = description;
            Property = property;
        }
    }
}
