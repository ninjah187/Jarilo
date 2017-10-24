using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo
{
    public class OptionAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public OptionAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
