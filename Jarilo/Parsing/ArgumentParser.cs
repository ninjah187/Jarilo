using Jarilo.Parsing.Exceptions;
using Jarilo.Tokenizing;
using Jarilo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Parsing
{
    class ArgumentParser
    {
        readonly PropertyValueParser _propertyValueParser;

        public ArgumentParser(PropertyValueParser propertyValueParser)
        {
            _propertyValueParser = propertyValueParser;
        }

        // TODO: pass metadata to parsers?
        public object Parse(Type argumentsType, Token[] tokens)
        {
            if (argumentsType == null)
            {
                return null;
            }
            var arguments = Activator.CreateInstance(argumentsType);
            var propertyAggregates = ParseProperties(argumentsType).ToArray();
            var argumentValues = ParseValues(tokens).ToArray();
            foreach (var aggregate in propertyAggregates)
            {
                var set = aggregate.property.GetSetMethod();
                var setParameter = ParseProperty(aggregate.property, ref argumentValues);
                set.Invoke(arguments, new object[] { setParameter });
            }
            return arguments;
        }

        IEnumerable<(PropertyInfo property, ArgumentAttribute argumentAttribute)> ParseProperties(Type argumentsType)
        {
            return argumentsType
                .GetProperties()
                .Select(property => new
                {
                    Property = property,
                    ArgumentAttribute = property.GetCustomAttribute<ArgumentAttribute>()
                })
                .Where(aggregate => aggregate.ArgumentAttribute != null)
                .Select(aggregate => (aggregate.Property, aggregate.ArgumentAttribute));
        }

        IEnumerable<string> ParseValues(Token[] tokens)
        {
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case OptionToken optionToken:
                        yield break;
                    case ValueToken valueToken:
                        yield return valueToken.Value;
                        break;
                }
            }
        }

        object ParseProperty(PropertyInfo propertyInfo, ref string[] argumentValues)
        {
            try
            {
                var propertyType = propertyInfo.PropertyType;
                return _propertyValueParser.ParseValues(propertyType, argumentValues)
                    ?? _propertyValueParser.ParseValue(propertyType, ref argumentValues);
            }
            catch (ValueParsingException exception)
            {
                throw new ParsingException(
                    exception,
                    ParsingTarget.Argument,
                    propertyInfo.Name.FirstToLower());
            }
        }
    }
}
