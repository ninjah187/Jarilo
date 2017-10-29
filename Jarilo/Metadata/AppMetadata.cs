using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jarilo.Metadata
{
    class AppMetadata
    {
        public CommandMetadata[] Commands { get; }

        public AppMetadata(CommandMetadata[] commands)
        {
            Commands = commands;
        }
    }
}
