using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Controller
    {
        // rename to non-plural form? Argument / Option ? works better with Option.Exists()
        
        public ICommandContext CommandContext { get; private set; }

        public Params Arguments => CommandContext.Arguments;
        public Params Options => CommandContext.Options;

        protected IActionResult Ok(string message) => new MessageView(message);
        protected IActionResult Error(string message) => new MessageView(message);
        protected IActionResult Help() => new HelpView(CommandContext.Path);
    }
}
