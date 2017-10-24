using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo
{
    public class ArgumentAttribute : Attribute
    {
        public string Description { get; }

        public ArgumentAttribute(string description)
        {
            Description = description;
        }
    }
}
