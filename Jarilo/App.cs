using Jarilo.Help;
using Jarilo.Metadata;
using Jarilo.Metadata.Builders;
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
        readonly Tokenizer _tokenizer;
        readonly AppMetadataBuilder _metadataBuilder;
        readonly CommandParser _commandParser;
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
            _tokenizer = new Tokenizer();
            var valueMetadataBuilder = new ValueMetadataBuilder();
            var propertyValueParser = new PropertyValueParser();
            _metadataBuilder = new AppMetadataBuilder(
                new ArgumentMetadataBuilder(valueMetadataBuilder),
                new OptionMetadataBuilder(valueMetadataBuilder),
                new ArgumentParser(propertyValueParser),
                new OptionParser(propertyValueParser));
            _commandParser = new CommandParser();
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
            commandMetadata.Run(tokens);
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

        void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
