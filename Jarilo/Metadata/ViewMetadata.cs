using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class ViewMetadata
    {
        public Type Type { get; }
        public MethodInfo Render { get; }
        public bool Exists => Type != null;

        public object Instance
        {
            get
            {
                if (Type == null)
                {
                    return null;
                }
                return _instance ?? (_instance = Activator.CreateInstance(Type));
            }
        }
        object _instance;

        public ViewMetadata(Type viewType)
        {
            Type = viewType;
            Render = viewType?.GetMethod("Render");
        }
    }
}
