using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    static class TypeExtensions
    {
        public static bool IsOptionsType(this Type type)
            => type
                .GetProperties()
                .Any(property => property.GetCustomAttribute<OptionAttribute>() != null);

        public static bool IsArgumentsType(this Type type)
            => type
                .GetProperties()
                .Any(property => property.GetCustomAttribute<ArgumentAttribute>() != null);
    }
}
