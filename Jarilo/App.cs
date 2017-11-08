using Jarilo.Help;
using Jarilo.Metadata;
using Jarilo.Parsing;
using Jarilo.Tokenizing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo
{
    public class App
    {
        public IServiceCollection Services { get; }

        ServiceProvider _serviceProvider;
        AppMetadata _metadata;

        readonly Type[] _types;
        readonly AppMetadataBuilder _metadataBuilder;
        readonly Tokenizer _tokenizer;
        readonly CommandParser _commandParser;
        readonly ArgumentParser _argumentParser;
        readonly OptionParser _optionParser;
        readonly HelpDocs _helpDocs;

        bool _disposeAfterRun = true;

        public App()
            : this(Assembly.GetEntryAssembly().GetTypes())
        {
        }

        public App(Type[] appTypes)
        {
            _types = appTypes;
            Services = new ServiceCollection();
            _metadataBuilder = new AppMetadataBuilder();
            _tokenizer = new Tokenizer();
            _commandParser = new CommandParser();
            _argumentParser = new ArgumentParser();
            _optionParser = new OptionParser();
            _helpDocs = new HelpDocs();
        }

        public void Run(string[] args)
        {
            _serviceProvider = _serviceProvider ?? (_serviceProvider = Services.BuildServiceProvider());
            _metadata = _metadata ?? (_metadata = _metadataBuilder.Build(_types, _serviceProvider));
            var tokens = _tokenizer.Tokenize(_metadata, args).ToArray();
            var commandMetadata = _commandParser.Parse(_metadata, tokens);
            var helpTokenExists = tokens.Any(token => token is HelpOptionToken);
            if (commandMetadata == null)
            {
                if (helpTokenExists)
                {
                    _helpDocs.Print(_metadata);
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
                return;
            }
            if (helpTokenExists)
            {
                _helpDocs.Print(commandMetadata);
                return;
            }
            var arguments = _argumentParser.Parse(commandMetadata.ArgumentsType, tokens);
            var options = _optionParser.Parse(commandMetadata.OptionsType, tokens);
            ExecuteCommand(commandMetadata, arguments, options);
            if (_disposeAfterRun == true)
            {
                Dispose();
            }
        }

        public void ReadEvalPrintLoop()
        {
            _disposeAfterRun = false;
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input == null)
                {
                    Dispose();
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
            if (parameters.Length == 1 && parameters.First().ParameterType.IsArgumentsType())
            {
                return new object[] { arguments };
            }
            if (parameters.Length == 1 && parameters.First().ParameterType.IsOptionsType())
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

        void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
