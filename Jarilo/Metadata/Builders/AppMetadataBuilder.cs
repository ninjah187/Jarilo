using Jarilo.Parsing;
using Jarilo.Reflection;
using Jarilo.Tokenizing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata.Builders
{
    class AppMetadataBuilder
    {
        readonly ArgumentMetadataBuilder _argumentMetadataBuilder;
        readonly OptionMetadataBuilder _optionMetadataBuilder;
        readonly ArgumentParser _argumentParser;
        readonly OptionParser _optionParser;
        readonly MethodInvoker _invoker;

        public AppMetadataBuilder(
            ArgumentMetadataBuilder argumentMetadataBuilder,
            OptionMetadataBuilder optionMetadataBuilder,
            ArgumentParser argumentParser,
            OptionParser optionParser,
            MethodInvoker invoker)
        {
            _argumentMetadataBuilder = argumentMetadataBuilder;
            _optionMetadataBuilder = optionMetadataBuilder;
            _argumentParser = argumentParser;
            _optionParser = optionParser;
            _invoker = invoker;
        }

        public AppMetadata Build(Type[] types, IServiceProvider services)
        {
            var commands = types
                .Select(type => new
                {
                    Type = type,
                    CommandAttribute = type.GetCustomAttribute<CommandAttribute>(),
                    ViewAttribute = type.GetCustomAttribute<ViewAttribute>(),
                    Run = type.GetMethod("Run")
                })
                .Where(aggregate => aggregate.CommandAttribute != null)
                .Where(aggregate => aggregate.Run != null)
                .Select(aggregate => new
                {
                    Type = aggregate.Type,
                    CommandAttribute = aggregate.CommandAttribute,
                    View = new
                    {
                        Type = aggregate.ViewAttribute?.Type,
                        Render = aggregate.ViewAttribute?.Type.GetMethod("Render")
                    },
                    Run = new
                    {
                        Method = aggregate.Run,
                        ParameterTypes = GetRunMethodParameterTypes(aggregate.Run)
                    }
                })
                .Select(aggregate =>
                {
                    var argumentsMetadata = _argumentMetadataBuilder.Build(aggregate.Run.ParameterTypes.arguments);
                    var optionsMetadata = _optionMetadataBuilder.Build(aggregate.Run.ParameterTypes.options);
                    Func<object> viewFactory = () =>
                    {
                        if (aggregate.View.Type == null)
                        {
                            return null;
                        }
                        return Activator.CreateInstance(aggregate.View.Type);
                    };
                    var viewMetadata = new ViewMetadata(viewFactory);
                    var constructor = aggregate.Type.GetConstructors().Single();
                    Func<object> commandFactory = () =>
                    {
                        var parameters = constructor
                            .GetParameters()
                            .Select(parameter => services.GetService(parameter.ParameterType))
                            .ToArray();
                        return _invoker.Invoke(() => constructor.Invoke(parameters));
                    };
                    Func<Token[], object> argumentsFactory = tokens => _argumentParser.Parse(aggregate.Run.ParameterTypes.arguments, tokens);
                    Func<Token[], object> optionsFactory = tokens => _optionParser.Parse(aggregate.Run.ParameterTypes.options, tokens);
                    Func<Token[], object[]> runMethodParametersFactory = tokens =>
                    {
                        var parameterTypes = aggregate.Run.ParameterTypes;
                        var parameters = new List<object>();
                        if (parameterTypes.arguments != null)
                        {
                            var arguments = _argumentParser.Parse(parameterTypes.arguments, tokens);
                            parameters.Add(arguments);
                        }
                        if (parameterTypes.options != null)
                        {
                            var options = _optionParser.Parse(parameterTypes.options, tokens);
                            parameters.Add(options);
                        }
                        return parameters.ToArray();
                    };
                    Action<Token[]> run = tokens =>
                    {
                        var runMethod = aggregate.Run.Method;
                        var command = commandFactory();
                        var runMethodParameters = runMethodParametersFactory(tokens);
                        var viewModel = _invoker.Invoke(() => runMethod.Invoke(command, runMethodParameters));
                        if (!viewMetadata.Exists || aggregate.View.Render == null)
                        {
                            return;
                        }
                        var renderMethodParameters = aggregate.View.Render.GetParameters().Length == 0
                            ? Array.Empty<object>()
                            : new[] { viewModel };
                        _invoker.Invoke(() =>
                        {
                            aggregate.View.Render.Invoke(viewMetadata.Instance, renderMethodParameters);
                        });
                    };
                    return new CommandMetadata(
                        aggregate.CommandAttribute.Name,
                        aggregate.CommandAttribute.Description,
                        argumentsMetadata,
                        optionsMetadata,
                        viewMetadata,
                        commandFactory,
                        argumentsFactory,
                        optionsFactory,
                        run);
                })
                .ToArray();
            return new AppMetadata(commands);
        }

        (Type arguments, Type options) GetRunMethodParameterTypes(MethodInfo runMethod)
        {
            if (runMethod == null)
            {
                return (null, null);
            }
            Type arguments = null;
            Type options = null;
            foreach (var parameter in runMethod.GetParameters())
            {
                var type = parameter.ParameterType;
                if (type.IsArgumentsType())
                {
                    arguments = type;
                    continue;
                }
                if (type.IsOptionsType())
                {
                    options = type;
                }
            }
            return (arguments, options);
        }
    }
}
