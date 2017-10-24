using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo
{
    public class ViewAttribute : Attribute
    {
        public Type Type { get; }

        public ViewAttribute(Type viewType)
        {
            Type = viewType;
        }
    }
}
