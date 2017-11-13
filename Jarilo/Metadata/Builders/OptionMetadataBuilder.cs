using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata.Builders
{
    class OptionMetadataBuilder
    {
        readonly ValueMetadataBuilder _valueMetadataBuilder;

        public OptionMetadataBuilder(ValueMetadataBuilder valueMetadataBuilder)
        {
            _valueMetadataBuilder = valueMetadataBuilder;
        }

        public OptionMetadata[] Build(Type optionsType)
        {
            var metadata = optionsType
                ?.GetProperties()
                .Select(property => new
                {
                    Property = property,
                    OptionAttribute = property.GetCustomAttribute<OptionAttribute>()
                })
                .Where(aggregate => aggregate.OptionAttribute != null)
                .Select(aggregate => new
                {
                    Property = aggregate.Property,
                    OptionAttribute = aggregate.OptionAttribute,
                    PossibleValues = _valueMetadataBuilder.Build(aggregate.Property)
                })
                .Select(aggregate =>
                {
                    if (aggregate.PossibleValues == null)
                    {
                        return new OptionMetadata(
                            aggregate.OptionAttribute.Name,
                            aggregate.OptionAttribute.Description,
                            aggregate.Property);
                    }
                    else
                    {
                        return new OptionEnumMetadata(
                            aggregate.OptionAttribute.Name,
                            aggregate.OptionAttribute.Description,
                            aggregate.Property,
                            aggregate.PossibleValues);
                    }
                })
                .ToArray()
                ?? Array.Empty<OptionMetadata>();
            return metadata;
        }
    }
}
