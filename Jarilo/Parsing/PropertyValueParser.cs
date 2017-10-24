using System;
using System.Collections.Generic;
using System.Linq;
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
            var convertedElements = values
                .Select(value => Convert.ChangeType(value, elementType))
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
            var convertedValue = Convert.ChangeType(value, propertyType);
            return convertedValue;
        }
    }
}
