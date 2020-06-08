using Jarilo.Parsing.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Parsing
{
    class PropertyValueParser
    {
        public object ParseBool(Type propertyType)
        {
            if (propertyType != typeof(bool))
            {
                return null;
            }
            return true;
        }

        public object ParseValues(Type propertyType, string[] values)
        {
            if (!propertyType.IsArray)
            {
                return null;
            }
            var elementType = propertyType.GetElementType();
            Func<string, object> parseElement = elementType.IsEnum
                ? value => ParseEnum(elementType, value)
                : (Func<string, object>) (value => Convert.ChangeType(value, elementType, CultureInfo.InvariantCulture));
            var convertedElements = values
                .Select(value => parseElement(value))
                .Where(value => value != null)
                .ToArray();
            var convertedArray = Array.CreateInstance(elementType, convertedElements.Length);
            Array.Copy(convertedElements, convertedArray, convertedElements.Length);
            return convertedArray;
        }

        public object ParseValue(Type propertyType, ref string[] values)
        {
            var value = values.FirstOrDefault();
            if (value == null)
            {
                return null;
            }
            values = values.Skip(1).ToArray();
            if (propertyType.IsEnum)
            {
                return ParseEnum(propertyType, value);
            }
            else if (IsNullable(propertyType))
            {
                string[] valueAsArray = new string[] { value };
                return ParseValue(Nullable.GetUnderlyingType(propertyType), ref valueAsArray);
            }
            try
            {
                return Convert.ChangeType(value, propertyType, CultureInfo.InvariantCulture);
            }
            catch (FormatException exception)
            {
                throw new ValueParsingException(exception, value);
            }
        }

        public object ParseEnum(Type enumType, string value)
        {
            var optionEnumValueAggregate = enumType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => new
                {
                    EnumValue = field.GetValue(null),
                    OptionEnumValueName = field.GetCustomAttribute<ValueAttribute>()?.Name
                })
                .Where(aggregate => aggregate.OptionEnumValueName == value)
                .FirstOrDefault();
            if (optionEnumValueAggregate == null)
            {
                var formatException = new FormatException($"Parsing error in enum value {value} of type {enumType.FullName}.");
                throw new ValueParsingException(formatException, value);
            }
            else
            {
                return optionEnumValueAggregate.EnumValue;
            }
        }

        private static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;

    }
}
