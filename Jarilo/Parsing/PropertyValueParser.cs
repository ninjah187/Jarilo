using System;
using System.Collections.Generic;
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
                : (Func<string, object>) (value => Convert.ChangeType(value, elementType));
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
            var convertedValue = Convert.ChangeType(value, propertyType);
            return convertedValue;
        }

        public object ParseEnum(Type enumType, string value)
        {
            var optionEnumValueAggregate = enumType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => new
                {
                    EnumValue = field.GetValue(null),
                    OptionEnumValueName = field.GetCustomAttribute<OptionEnumValueAttribute>()?.Name
                })
                .Where(aggregate => aggregate.OptionEnumValueName == value)
                .FirstOrDefault();
            if (optionEnumValueAggregate == null)
            {
                return null;
            }
            else
            {
                return optionEnumValueAggregate.EnumValue;
            }
        }
    }
}
