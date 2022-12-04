using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cli.Mvc
{
    public class AppBuilder
    {
        Type[] _types;
        Func<IRouter> _routerFactory;

        IServiceCollection _serviceCollection = new ServiceCollection();

        public AppBuilder UseTypes(params Type[] types)
        {
            _types = types;
            return this;
        }

        public AppBuilder UseRouter<T>(Func<IRouter> factory = null) where T : IRouter
        {
            _routerFactory = factory ?? (() => (T) Activator.CreateInstance(typeof(T)));
            return this;
        }

        public AppBuilder AddSingleton<T>() where T : class
        {
            _serviceCollection.AddSingleton<T>();
            return this;
        }

        public App Build()
        {
            var types = _types ?? Assembly.GetCallingAssembly().GetTypes();
            var router = _routerFactory?.Invoke() ?? new RouterBuilder(types).Build();

            _serviceCollection.AddSingleton(typeof(IRouter), router);
            _serviceCollection.AddSingleton(typeof(IRenderer), typeof(ConsoleRenderer));

            Startup(types);

            var serviceProvider = _serviceCollection.BuildServiceProvider();

            return new App(serviceProvider);
        }

        void Startup(Type[] types)
        {
            var startupType = types.FirstOrDefault(type => type.Name == "Startup");

            if (startupType == null)
            {
                return;
            }

            var startup = Activator.CreateInstance(startupType);

            startupType.GetMethod("ConfigureServices")?.Invoke(startup, new[] { _serviceCollection });
        }
    }
}
