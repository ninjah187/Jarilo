using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Metadata
{
    class ValueMetadata
    {
        public string Name { get; }
        public string Description { get; }

        public ValueMetadata(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
