using Jarilo.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class AppMetadata
    {
        public CommandMetadata[] Commands { get; private set; }

        public void Build(ServiceCollection services)
        {
            Commands = Assembly
                .GetEntryAssembly()
                .GetTypes()
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
                .Select(aggregate =>
                {
                    var runParameters = GetRunMethodParameterTypes(aggregate.Run);
                    var arguments = runParameters
                        .argumentsType
                        ?.GetProperties()
                        .Select(property => new
                        {
                            Property = property,
                            ArgumentAttribute = property.GetCustomAttribute<ArgumentAttribute>()
                        })
                        .Where(argumentAggregate => argumentAggregate.ArgumentAttribute != null)
                        .Select(argumentAggregate => new ArgumentMetadata(
                            argumentAggregate.Property.Name.ToLower(),
                            argumentAggregate.ArgumentAttribute.Description,
                            argumentAggregate.Property))
                        .ToArray()
                        ?? Array.Empty<ArgumentMetadata>();
                    var options = runParameters
                        .optionsType
                        ?.GetProperties()
                        .Select(property => new
                        {
                            Property = property,
                            OptionAttribute = property.GetCustomAttribute<OptionAttribute>()
                        })
                        .Where(optionAggregate => optionAggregate.OptionAttribute != null)
                        .Select(optionAggregate => new OptionMetadata(
                            optionAggregate.OptionAttribute.Name,
                            optionAggregate.OptionAttribute.Description,
                            optionAggregate.Property))
                        .ToArray()
                        ?? Array.Empty<OptionMetadata>();
                    var view = new ViewMetadata(aggregate.ViewAttribute?.Type);
                    var constructor = aggregate.Type.GetConstructors().Single();
                    Func<object> factory = () =>
                    {
                        var parameters = constructor
                            .GetParameters()
                            .Select(parameter => services.Get(parameter.ParameterType))
                            .ToArray();
                        return constructor.Invoke(parameters);
                    };
                    return new
                    {
                        Type = aggregate.Type,
                        CommandAttribute = aggregate.CommandAttribute,
                        ViewAttribute = aggregate.ViewAttribute,
                        Run = aggregate.Run,
                        ArgumentsType = runParameters.argumentsType,
                        Arguments = arguments,
                        OptionsType = runParameters.optionsType,
                        Options = options,
                        View = view,
                        Factory = factory
                    };
                })
                .Select(aggregate => new CommandMetadata(
                    aggregate.CommandAttribute.Name,
                    aggregate.CommandAttribute.Description,
                    aggregate.Type,
                    aggregate.Run,
                    aggregate.ArgumentsType,
                    aggregate.OptionsType,
                    aggregate.Arguments,
                    aggregate.Options,
                    aggregate.View,
                    aggregate.Factory))
                .ToArray();
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
    }
}
