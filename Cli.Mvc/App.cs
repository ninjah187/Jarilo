using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public class App
    {
        readonly IServiceProvider _serviceProvider;
        readonly IRouter _router;
        readonly IRenderer _renderer;

        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _router = _serviceProvider.GetRequiredService<IRouter>();
            _renderer = _serviceProvider.GetRequiredService<IRenderer>();
        }

        public void Run(string command)
        {
            var action = _router.Resolve(command);
            
            var view = NeedsHelp(action) ? new HelpView(action.Route.Path) : action.Execute(_serviceProvider);
            InitializeView(view);
            view.Render();
        }

        bool NeedsHelp(RuntimeAction action)
        {
            return action.Options.Exists("help") || action.Options.Exists("h");
        }

        void InitializeView(IActionResult result) // <- This is a leaking abstraction. Looks like I don't need any other IActionResult than IView.
        {
            var type = result.GetType();

            if (!typeof(View).IsAssignableFrom(type))
            {
                return;
            }

            result.SetProperty("Renderer", _renderer);

            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var serviceAttribute = field.GetCustomAttribute<ServiceAttribute>();
                
                if (serviceAttribute == null)
                {
                    continue;
                }

                result.SetField(field.Name, _serviceProvider.GetService(field.FieldType));
            }

            //var rendererProperty = type.GetProperty("Renderer", BindingFlags.Instance | BindingFlags.NonPublic);

            //rendererProperty = rendererProperty.DeclaringType.GetProperty("Renderer", BindingFlags.Instance | BindingFlags.NonPublic);

            //var set = rendererProperty.GetSetMethod(true);

            //set.Invoke(result, new[] { _renderer });
        }
    }
}
