using Jarilo.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata.Builders
{
    class ArgumentMetadataBuilder
    {
        readonly ValueMetadataBuilder _valueMetadataBuilder;

        public ArgumentMetadataBuilder(ValueMetadataBuilder valueMetadataBuilder)
        {
            _valueMetadataBuilder = valueMetadataBuilder;
        }

        public ArgumentMetadata[] Build(Type argumentsType)
        {
            var metadata = argumentsType
                ?.GetProperties()
                .Select(property => new
                {
                    Property = property,
                    ArgumentAttribute = property.GetCustomAttribute<ArgumentAttribute>()
                })
                .Where(aggregate => aggregate.ArgumentAttribute != null)
                .Select(aggregate => new
                {
                    Property = aggregate.Property,
                    ArgumentAttribute = aggregate.ArgumentAttribute,
                    PossibleValues = _valueMetadataBuilder.Build(aggregate.Property)
                })
                .Select(aggregate =>
                {
                    if (aggregate.PossibleValues == null)
                    {
                        return new ArgumentMetadata(
                            aggregate.Property.Name,
                            aggregate.ArgumentAttribute.Description,
                            aggregate.Property.PropertyType.IsArray);
                    }
                    else
                    {

                        return new ArgumentEnumMetadata(
                            aggregate.Property.Name,
                            aggregate.ArgumentAttribute.Description,
                            aggregate.Property.PropertyType.IsArray,
                            aggregate.PossibleValues);
                    }
                })
                .ToArray()
                ?? Array.Empty<ArgumentMetadata>();
            return metadata;
        }
    }
}
