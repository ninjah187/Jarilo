using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cli.Mvc
{
    public interface ICommandContext
    {
        string Command { get; }

        string Path { get; }

        Params Arguments { get; }

        Params Options { get; }
    }

    public class CommandContext : ICommandContext
    {
        public string Command { get; }
        public string Path { get; }
        public Params Arguments { get; }
        public Params Options { get; }

        public CommandContext(string command, string path, Params arguments, Params options)
        {
            Command = command;
            Path = path;
            Arguments = arguments;
            Options = options;
        }
    }

    public class HelpView : View
    {
        // Maybe I should disallow injecting by field or property in view?
        // Instead of injecting IRouter, view's model should be Route[].
        [Service] readonly IRouter _router;

        readonly string _path; // make it injectable service?

        public HelpView(string path)
        {
            _path = path;
        }

        public override void Render()
        {
            var commands = GetCommands();

            Renderer.WriteLine("Commands:");

            //foreach (var command in commands)
            //{
            //    Renderer.WriteLine($"- {command}");
            //}

            foreach (var path in _router.Routes.Select(route => route.Path))
            {
                if (_path.StartsWith(path)) // make DRYer - the same line is in Router
                {
                    Renderer.WriteLine(path);
                }

                // Renderer.WriteLine($"- {route}");
            }
        }

        string[] GetCommands()
        {
            return _router
                .Routes
                .Select(route => route.ControllerPath)
                .Distinct()
                .ToArray();
        }
    }
}
