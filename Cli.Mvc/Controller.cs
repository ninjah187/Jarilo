using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Controller
    {
        protected Params Arguments { get; }
        protected Params Options { get; }

        //protected IActionResult Ok(string message) => new OkView();
        //protected IActionResult Error(string message) => new ErrorView();
        //protected IActionResult Help() => throw new NotImplementedException();
    }
}
