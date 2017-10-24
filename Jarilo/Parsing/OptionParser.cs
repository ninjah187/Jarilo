using Jarilo.Tokenizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Parsing
{
    class OptionParser
    {
        readonly PropertyValueParser _propertyValueParser = new PropertyValueParser();

        public object Parse(Type optionsType, Token[] tokens)
        {
            var options = Activator.CreateInstance(optionsType);
            var propertyAggregates = ParseProperties(optionsType).ToArray();
            foreach (var split in SplitByOptionTokens(tokens))
            {
                var optionAggregate = propertyAggregates
                    .FirstOrDefault(aggregate => aggregate.optionAttribute.Name == split.delimiter.Value);
                if (optionAggregate.property == null)
                {
                    continue;
                }
                var propertyType = optionAggregate.property.PropertyType;
                var set = optionAggregate.property.GetSetMethod();
                var optionValues = split
                    .followedBy
                    .OfType<ValueToken>()
                    .Select(token => token.Value)
                    .ToArray();
                var setParameter = _propertyValueParser.ParseBool(propertyType)
                    ?? _propertyValueParser.ParseValues(propertyType, optionValues)
                    ?? _propertyValueParser.ParseValue(propertyType, ref optionValues);
                set.Invoke(options, new object[] { setParameter });
            }
            return options;
        }

        IEnumerable<(PropertyInfo property, OptionAttribute optionAttribute)> ParseProperties(Type optionsType)
            => optionsType
                .GetProperties()
                .Select(property => new
                {
                    Property = property,
                    OptionAttribute = property.GetCustomAttribute<OptionAttribute>()
                })
                .Where(aggregate => aggregate.OptionAttribute != null)
                .Select(aggregate => (aggregate.Property, aggregate.OptionAttribute));

        IEnumerable<(OptionToken delimiter, Token[] followedBy)> SplitByOptionTokens(Token[] tokens)
        {
            OptionToken delimiter = null;
            List<Token> followedBy = new List<Token>();
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case OptionToken optionToken when delimiter == null:
                        delimiter = optionToken;
                        followedBy.Clear();
                        break;
                    case OptionToken optionToken when delimiter != null:
                        yield return (delimiter, followedBy.ToArray());
                        delimiter = optionToken;
                        followedBy.Clear();
                        break;
                    case Token notOptionToken when !(token is OptionToken):
                        followedBy.Add(notOptionToken);
                        break;
                }
            }
            if (delimiter == null)
            {
                yield break;
            }
            yield return (delimiter, followedBy.ToArray());
        }
    }
}
