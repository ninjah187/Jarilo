using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public class RuntimeAction
    {
        public Route Route { get; }
        public ParsedParams Params { get; }

        public RuntimeAction(Route route, ParsedParams @params)
        {
            Route = route;
            Params = @params;
        }
    }
}
