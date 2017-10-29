using Jarilo.Tokenizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Parsing
{
    class ArgumentParser
    {
        readonly PropertyValueParser _propertyValueParser = new PropertyValueParser();

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
                var propertyType = aggregate.property.PropertyType;
                var set = aggregate.property.GetSetMethod();
                var setParameter = _propertyValueParser.ParseValues(propertyType, argumentValues)
                    ?? _propertyValueParser.ParseValue(propertyType, ref argumentValues);
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
    }
}
