using Jarilo.Tokenizing;
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
        public bool IsArray { get; }
        public Func<Token[], object> Factory { get; }

        public ArgumentMetadata(
            string name,
            string description,
            bool isArray)
        {
            Name = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            Description = description;
            IsArray = isArray;
        }
    }
}
