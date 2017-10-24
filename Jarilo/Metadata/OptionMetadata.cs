using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class OptionMetadata
    {
        public string Name { get; }
        public string Description { get; }
        public PropertyInfo Property { get; }

        public OptionMetadata(string name, string description, PropertyInfo property)
        {
            Name = name;
            Description = description;
            Property = property;
        }
    }
}
