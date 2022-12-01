using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Params
    {
        readonly Dictionary<string, string> _params;

        public Params(Dictionary<string, string> @params)
        {
            _params = @params;
        }

        public T Get<T>(string name)
        {
            return (T) Convert.ChangeType(_params[name], typeof(T));
        }

        public object Get(string name, Type type)
        {
            return Convert.ChangeType(_params[name], type);
        }

        public string this[string name]
        {
            get
            {
                return _params[name];
            }
        }
    }
}
