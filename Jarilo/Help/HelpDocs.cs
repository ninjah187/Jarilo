using Jarilo.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.Help
{
    class HelpDocs
    {
        public void Print(AppMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public void Print(CommandMetadata metadata)
        {
            Console.WriteLine("Command:");
            Console.WriteLine($"  {metadata.Description}");
            Console.WriteLine("Usage:");
            Console.WriteLine($"  {CommandUsage(metadata)}");
            if (metadata.Arguments.Length != 0)
            {
                Console.WriteLine("Arguments:");
                foreach (var argument in metadata.Arguments)
                {
                    Console.WriteLine($"  {Argument(argument, true)}");
                }
            }
            if (metadata.Options.Length != 0)
            {
                Console.WriteLine("Options:");
                foreach (var option in metadata.Options)
                {
                    Console.WriteLine($"  {Option(option)}");
                }
            }
        }

        string CommandUsage(CommandMetadata metadata)
        {
            var usage = metadata.Name;
            foreach (var argument in metadata.Arguments)
            {
                usage += $" {Argument(argument)}";
            }
            if (metadata.Options.Length != 0)
            {
                usage += " [options]";
            }
            return usage;
        }

        string Argument(ArgumentMetadata metadata, bool withDescription = false)
        {
            var argument = $"<{metadata.Name}";
            if (metadata.Property.PropertyType.IsArray)
            {
                argument += "...";
            }
            argument += ">";
            if (withDescription)
            {
                argument += $" - {metadata.Description}";
            }
            return argument;
        }

        string Option(OptionMetadata metadata)
        {
            var option = metadata.Name;
            var optionType = metadata.Property.PropertyType;
            if (optionType != typeof(bool))
            {
                option += optionType.IsArray
                    ? " <args...>"
                    : " <arg>";
            }
            option += $" - {metadata.Description}";
            return option;
        }
    }
}
