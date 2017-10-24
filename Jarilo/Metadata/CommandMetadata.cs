using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class CommandMetadata
    {
        public string Name { get; }
        public string Description { get; }
        public Type Type { get; }
        public Type ArgumentsType { get; }
        public Type OptionsType { get; }
        public MethodInfo Run { get; }
        public ArgumentMetadata[] Arguments { get; }
        public OptionMetadata[] Options { get; }
        public ViewMetadata View { get; }
        public Func<object> Factory { get; }

        public CommandMetadata(
            string name,
            string description,
            Type commandType,
            MethodInfo runMethod,
            Type argumentsType,
            Type optionsType,
            ArgumentMetadata[] arguments,
            OptionMetadata[] options,
            ViewMetadata view,
            Func<object> factory)
        {
            Name = name;
            Description = description;
            Type = commandType;
            Run = runMethod;
            ArgumentsType = argumentsType;
            OptionsType = optionsType;
            Arguments = arguments;
            Options = options;
            View = view;
            Factory = factory;
        }
    }
}
