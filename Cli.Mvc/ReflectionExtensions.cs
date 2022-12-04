using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public static class ReflectionExtensions
    {
        public static void SetProperty<T>(this T source, string name, object value)
        {
            var property = source.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            property = property.DeclaringType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var setter = property.GetSetMethod(true);

            setter.Invoke(source, new[] { value });
        }

        public static void SetField<T>(this T source, string name, object value)
        {
            var field = source.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            field = field.DeclaringType.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            field.SetValue(source, value);
        }
    }
}
