using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValueAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public ValueAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
