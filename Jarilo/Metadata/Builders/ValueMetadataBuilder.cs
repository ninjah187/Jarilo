using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata.Builders
{
    class ValueMetadataBuilder
    {
        public ValueMetadata[] Build(PropertyInfo property)
        {
            var propertyCoreType = property.PropertyType.IsArray
                ? property.PropertyType.GetElementType()
                : property.PropertyType;
            if (!propertyCoreType.IsEnum)
            {
                return null;
            }
            return propertyCoreType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetCustomAttribute<ValueAttribute>())
                .Where(enumValueAttribute => enumValueAttribute != null)
                .Select(enumValueAttribute => new ValueMetadata(
                    enumValueAttribute.Name,
                    enumValueAttribute.Description))
                .ToArray();
        }
    }
}
