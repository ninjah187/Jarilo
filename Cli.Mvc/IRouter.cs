using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public interface IRouter
    {
        RuntimeAction Resolve(string command);
    }
}
