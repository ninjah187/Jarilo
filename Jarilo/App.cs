using Jarilo.Help;
using Jarilo.Metadata;
using Jarilo.Metadata.Builders;
using Jarilo.Parsing;
using Jarilo.Parsing.Exceptions;
using Jarilo.Reflection;
using Jarilo.Tokenizing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jarilo
{
    public class App
    {
        public IServiceCollection Services { get; }

        ServiceProvider ServiceProvider
            => _serviceProvider ?? (_serviceProvider = Services.BuildServiceProvider());
        ServiceProvider _serviceProvider;

        AppMetadata Metadata
            => _metadata ?? (_metadata = _metadataBuilder.Build(_types, ServiceProvider));
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
                new OptionParser(propertyValueParser),
                new MethodInvoker());
            _commandParser = new CommandParser();
            _helpDocs = new HelpDocs();
        }

        void Dispose()
        {
            _serviceProvider?.Dispose();
        }

        public async Task RunAsync(string[] args)
        {
            var tokens = _tokenizer.Tokenize(Metadata, args).ToArray();
            var commandMetadata = _commandParser.Parse(Metadata, tokens);
            var helpTokenExists = tokens.Any(token => token is HelpOptionToken);
            if (commandMetadata == null)
            {
                if (helpTokenExists)
                {
                    _helpDocs.Print(Metadata);
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
            try
            {
                await commandMetadata.Run(tokens);
            }
            catch (ParsingException exception)
            {
                Console.WriteLine(
                    $"Incorrect value \"{exception.Value}\" of {exception.Target.Map()} \"{exception.Name}\".");
            }
            if (_disposeAfterRun == true)
            {
                Dispose();
            }
        }

        public async Task ReadEvalPrintLoopAsync()
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
                await RunAsync(input.Split(" "));
            }
        }

        public void Run(string[] args)
        {
            RethrowAggregateInnerException(() => RunAsync(args).Wait());
        }

        public void ReadEvalPrintLoop()
        {
            RethrowAggregateInnerException(() => ReadEvalPrintLoopAsync().Wait());
        }

        static void RethrowAggregateInnerException(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException exception)
            {
                throw exception.InnerException;
            }
        }
    }
}
