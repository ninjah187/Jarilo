using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OptionEnumValueAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public OptionEnumValueAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
