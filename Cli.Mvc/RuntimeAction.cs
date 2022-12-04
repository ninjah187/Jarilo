using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    // Make this more extendable for potential custom Router implementations.
    // Extract IRuntimeAction + provide more extendable implementation in RuntimeAction.

    // (make it open for extension and closed for modification)
    public class RuntimeAction
    {
        public string Command { get; }
        public Route Route { get; }

        public Params Arguments { get; }
        public Params Options { get; }
        
        public RuntimeAction(string command, Route route, ParsedParams @params)
        {
            Command = command;
            Route = route;
            Arguments = BuildArguments(@params.Arguments);
            Options = @params.Options;
        }

        // Add Execute method here? Or add external method executor ?

        public IActionResult Execute(IServiceProvider serviceProvider)
        {
            var constructor = Route
                .Controller
                .GetConstructors()
                .OrderByDescending(ctr => ctr.GetParameters().Length)
                .FirstOrDefault();

            var constructorParameters = constructor
                .GetParameters()
                .Select(param => serviceProvider.GetService(param.ParameterType))
                .ToArray();

            var controller = constructor.Invoke(constructorParameters); // TODO: implement controller ctr

            var context = BuildContext();

            controller.SetProperty("CommandContext", context);

            //controller.SetProperty("Arguments", Arguments);
            //controller.SetProperty("Options", Options);

            var methodParameters = Route
                .Method
                .GetParameters()
                .Select(param => GetParamValue(param.Name, param.ParameterType))
                .ToArray();

            return (IActionResult) Route.Method.Invoke(controller, methodParameters);
        }

        object GetParamValue(string name, Type type)
        {
            // TODO: Implement TryGetValue to avoid double lookup.
            if (Arguments.Exists(name))
            {
                return Arguments.Get(name, type);
            }

            // TODO: check for option attribute
            if (Options.Exists(name))
            {
                return Options.Get(name, type);
            }

            return null;
        }

        Params BuildArguments(IReadOnlyList<string> arguments)
        {
            var dictionary = new Dictionary<string, string>();

            var currentArg = 0;

            foreach (var param in Route.Method.GetParameters())
            {
                if (param.GetCustomAttribute<OptionAttribute>() == null)
                {
                    dictionary[param.Name] = currentArg < arguments.Count ? arguments[currentArg] : null;
                    ++currentArg;
                }
            }

            return new Params(dictionary);
        }

        ICommandContext BuildContext()
        {
            return new CommandContext(Command, Route.Path, Arguments, Options);
        }
    }
}
