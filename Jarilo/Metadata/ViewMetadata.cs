using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class ViewMetadata
    {
        public bool Exists => Instance != null;

        public object Instance
        {
            get
            {
                return _instance ?? (_instance = _factory());
            }
        }
        object _instance;

        readonly Func<object> _factory;

        public ViewMetadata(Func<object> factory)
        {
            _factory = factory;
        }
    }
}
