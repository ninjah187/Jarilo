using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public interface IActionExecutor
    {
        IActionResult Execute(RuntimeAction action);
    }

    public class ActionExecutor : IActionExecutor
    {
        // Should this logic be moved directly to RuntimeAction ?
        public IActionResult Execute(RuntimeAction action)
        {
            //var arguments = action
            //    .Route
            //    .Method
            //    .GetParameters()
            //    .Where(param => param.GetCustomAttribute<OptionAttribute>() == null);

            var constructor = action
                .Route
                .Controller
                .GetConstructors()
                .OrderByDescending(ctr => ctr.GetParameters().Length)
                .FirstOrDefault();

            var method = action.Route.Method;

            var controller = constructor.Invoke(Array.Empty<object>());

            var arguments = BuildArguments(action);

            var parameters = method
                .GetParameters()
                .Select(param => GetParamValue(param.Name, param.ParameterType, action.Arguments, action.Options))
                .ToArray();

            return (IActionResult) method.Invoke(controller, parameters);
        }

        object GetParamValue(string name, Type type, Params arguments, Params options)
        {
            throw new NotImplementedException();
        }

        Params BuildArguments(RuntimeAction action)
        {
            throw new NotImplementedException();
        }
    }

    public class RouterBuilder
    {
        readonly Type[] _types;

        public RouterBuilder(Type[] types)
        {
            _types = types;
        }

        public IRouter Build()
        {
            var routes = new List<Route>();

            var controllers = _types
                .Where(type => typeof(Controller).IsAssignableFrom(type)) // TODO: add controller attribute
                .ToArray();

            foreach (var controller in controllers)
            {
                var controllerPath = GetControllerPath(controller);

                //var actions = controller
                //    .GetMethods(BindingFlags.Public)
                //    // .Where(IsActionMethod)
                //    .Select(method => new
                //    {
                //        method,
                //        routeAttribute = method.GetCustomAttribute<RouteAttribute>()
                //    })
                //    .ToArray();

                //var hasExplicitRoutes = actions.Any(action => action.routeAttribute != null);

                //routes.Add(new Route(controller, method, controllerPath))

                // Add following feature: if has explicit [Route], then only methods with [Route] are actions.

                foreach (var method in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!IsActionMethod(method))
                    {
                        continue;
                    }

                    var actionPath = GetActionPath(method);

                    routes.Add(new Route(controller, method, controllerPath, actionPath));
                }
            }

            return new Router(routes);
        }

        bool IsActionMethod(MethodInfo methodInfo)
        {
            if (methodInfo.GetCustomAttribute<RouteAttribute>() != null)
            {
                return true;
            }
            if (typeof(IActionResult).IsAssignableFrom(methodInfo.ReturnType))
            {
                return true;
            }
            return false;
        }

        static string GetControllerPath(Type type)
        {
            var routeAttribute = type.GetCustomAttribute<RouteAttribute>();
            var controllerPath = routeAttribute?.Path ?? GetPathByConvention(type.Name);
            return controllerPath;
        }

        static string GetActionPath(MethodInfo method)
        {
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();
            var actionPath = routeAttribute?.Path ?? GetPathByConvention(method.Name);
            return actionPath;
        }

        static string GetPathByConvention(string name)
        {
            //var path = name
            //    .Replace("Controller", "")
            //    .SplitBy(char.IsUpper)
            //    .Select(x => x.ToLower())
            //    .ToArray();

            //return string.Join(" ", path);

            return name.Replace("Controller", "").ToLower();
        }
    }
}
