using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public class Route
    {
        public Type Controller { get; }
        public MethodInfo Method { get; }

        public string ControllerPath { get; }
        public string ActionPath { get; }

        public string Path => $"{ControllerPath} {ActionPath}";

        public Route(Type controller, MethodInfo method, string controllerPath, string actionPath)
        {
            Controller = controller;
            Method = method;
            ControllerPath = controllerPath;
            ActionPath = actionPath;
        }
    }
}
