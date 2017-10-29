using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class AppMetadataBuilder
    {
        public AppMetadata Build(Type[] types, IServiceProvider services)
        {
            var commands = types
                .Select(type => new
                {
                    Type = type,
                    CommandAttribute = type.GetCustomAttribute<CommandAttribute>()
                })
                .Where(aggregate => aggregate.CommandAttribute != null)
                .Select(aggregate => new
                {
                    Type = aggregate.Type,
                    CommandAttribute = aggregate.CommandAttribute,
                    ViewAttribute = aggregate.Type.GetCustomAttribute<ViewAttribute>(),
                    Run = aggregate.Type.GetMethod("Run")
                })
                .Where(aggregate => aggregate.Run != null)
                .Select(aggregate => new
                {
                    Type = aggregate.Type,
                    CommandAttribute = aggregate.CommandAttribute,
                    ViewAttribute = aggregate.ViewAttribute,
                    Run = new
                    {
                        Method = aggregate.Run,
                        Parameters = GetRunMethodParameterTypes(aggregate.Run)
                    }
                })
                .Select(aggregate =>
                {
                    var arguments = BuildArgumentsMetadata(aggregate.Run.Parameters.argumentsType);
                    var options = BuildOptionsMetadata(aggregate.Run.Parameters.optionsType);
                    var view = new ViewMetadata(aggregate.ViewAttribute?.Type);
                    var constructor = aggregate.Type.GetConstructors().Single();
                    Func<object> factory = () =>
                    {
                        var parameters = constructor
                            .GetParameters()
                            .Select(parameter => services.GetService(parameter.ParameterType))
                            .ToArray();
                        return constructor.Invoke(parameters);
                    };
                    return new CommandMetadata(
                        aggregate.CommandAttribute.Name,
                        aggregate.CommandAttribute.Description,
                        aggregate.Type,
                        aggregate.Run.Method,
                        aggregate.Run.Parameters.argumentsType,
                        aggregate.Run.Parameters.optionsType,
                        arguments,
                        options,
                        view,
                        factory);
                })
                .ToArray();
            return new AppMetadata(commands);
        }

        (Type argumentsType, Type optionsType) GetRunMethodParameterTypes(MethodInfo runMethod)
        {
            Type argumentsType = null;
            Type optionsType = null;
            foreach (var parameter in runMethod.GetParameters())
            {
                var type = parameter.ParameterType;
                if (type.IsArgumentsType())
                {
                    argumentsType = type;
                    continue;
                }
                if (type.IsOptionsType())
                {
                    optionsType = type;
                }
            }
            return (argumentsType, optionsType);
        }

        ArgumentMetadata[] BuildArgumentsMetadata(Type argumentsType)
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
                    PossibleValues = BuildValuesMetadata(aggregate.Property)
                })
                .Select(aggregate =>
                {
                    if (aggregate.PossibleValues == null)
                    {
                        return new ArgumentMetadata(
                            aggregate.Property.Name,
                            aggregate.ArgumentAttribute.Description,
                            aggregate.Property);
                    }
                    else
                    {

                        return new ArgumentEnumMetadata(
                            aggregate.Property.Name,
                            aggregate.ArgumentAttribute.Description,
                            aggregate.Property,
                            aggregate.PossibleValues);
                    }
                })
                .ToArray()
                ?? Array.Empty<ArgumentMetadata>();
            return metadata;
        }

        OptionMetadata[] BuildOptionsMetadata(Type optionsType)
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
                    PossibleValues = BuildValuesMetadata(aggregate.Property)
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

        ValueMetadata[] BuildValuesMetadata(PropertyInfo property)
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
