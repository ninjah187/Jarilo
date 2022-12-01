using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Router : IRouter
    {
        readonly IReadOnlyList<Route> _routes;

        public Router(IReadOnlyList<Route> routes)
        {
            _routes = routes;
        }

        public RuntimeAction Resolve(string command)
        {
            throw new NotImplementedException();
        }
    }
}
