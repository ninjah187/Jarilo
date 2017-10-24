using Jarilo.DependencyInjection;
using Jarilo.Help;
using Jarilo.Metadata;
using Jarilo.Parsing;
using Jarilo.Tokenizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo
{
    public class App
    {
        public ServiceCollection Services { get; }

        readonly AppMetadata _metadata;
        readonly Tokenizer _tokenizer;
        readonly CommandParser _commandParser;
        readonly ArgumentParser _argumentParser;
        readonly OptionParser _optionParser;
        readonly HelpDocs _helpDocs;

        public App()
        {
            Services = new ServiceCollection();
            _metadata = new AppMetadata();
            _tokenizer = new Tokenizer();
            _commandParser = new CommandParser();
            _argumentParser = new ArgumentParser();
            _optionParser = new OptionParser();
            _helpDocs = new HelpDocs();
        }

        public void Run(string[] args)
        {
            _metadata.Build(Services);
            var tokens = _tokenizer.Tokenize(_metadata, args).ToArray();
            var commandMetadata = _commandParser.Parse(_metadata, tokens);
            if (tokens.Any(token => token is HelpOptionToken))
            {
                _helpDocs.Print(commandMetadata);
                return;
            }
            var arguments = _argumentParser.Parse(commandMetadata.ArgumentsType, tokens);
            var options = _optionParser.Parse(commandMetadata.OptionsType, tokens);
            ExecuteCommand(commandMetadata, arguments, options);
        }

        public void ReadEvalPrintLoop()
        {
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input == ":q")
                {
                    return;
                }
                Run(input.Split(" "));
            }
        }

        void ExecuteCommand(CommandMetadata commandMetadata, object arguments, object options)
        {
            var runMethod = commandMetadata.Run;
            var command = commandMetadata.Factory();
            var runMethodParameters = GetRunMethodParameters(runMethod, arguments, options);
            if (commandMetadata.View.Exists)
            {
                var viewModel = runMethod.Invoke(command, runMethodParameters);
                var renderMethod = commandMetadata.View.Render;
                var renderMethodParameters = GetRenderMethodParameters(renderMethod, viewModel);
                renderMethod.Invoke(commandMetadata.View.Instance, renderMethodParameters);
            }
            else
            {
                // Run method returned value is ignored when view doesn't exist
                runMethod.Invoke(command, runMethodParameters);
            }
        }

        object[] GetRunMethodParameters(MethodInfo runMethod, object arguments, object options)
        {
            var parameters = runMethod.GetParameters();
            if (parameters.Length == 0)
            {
                return new object[0];
            }
            if (parameters.Length == 1 && IsArgumentsType(parameters.First().ParameterType))
            {
                return new object[] { arguments };
            }
            if (parameters.Length == 1 && IsOptionsType(parameters.First().ParameterType))
            {
                return new object[] { options };
            }
            if (parameters.Length > 2)
            {
                throw new NotImplementedException($"Run method needs 0, 1 or 2 parameters.");
            }
            var first = parameters.First();
            var second = parameters.Skip(1).First();
            if (first.ParameterType.IsArgumentsType() && second.ParameterType.IsOptionsType())
            {
                return new object[] { arguments, options };
            }
            throw new NotImplementedException();
        }

        object[] GetRenderMethodParameters(MethodInfo renderMethod, object viewModel)
        {
            var parameters = renderMethod.GetParameters();
            if (parameters.Length == 0)
            {
                return new object[0];
            }
            if (parameters.Length == 1 && parameters.First().ParameterType.IsAssignableFrom(viewModel.GetType()))
            {
                return new object[] { viewModel };
            }
            if (parameters.Length > 1)
            {
                throw new NotImplementedException($"Render method needs 0 or 1 parameter.");
            }
            throw new NotImplementedException();
        }

        bool IsArgumentsType(Type type)
            => type
                .GetProperties()
                .Any(property => property.GetCustomAttribute<ArgumentAttribute>() != null);

        bool IsOptionsType(Type type)
            => type
                .GetProperties()
                .Any(property => property.GetCustomAttribute<OptionAttribute>() != null);
    }
}
