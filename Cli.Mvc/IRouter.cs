using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public interface IRouter
    {
        IReadOnlyList<Route> Routes { get; }
        RuntimeAction Resolve(string command);
    }
}
