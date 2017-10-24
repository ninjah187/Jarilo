using System;
using System.Collections.Generic;
using System.Text;

namespace Jarilo.DependencyInjection
{
    public class ServiceCollection
    {
        readonly Dictionary<Type, Func<object>> _register;

        public ServiceCollection()
        {
            _register = new Dictionary<Type, Func<object>>();
        }

        public ServiceCollection AddSingleton<TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, new()
        {
            return AddSingleton<TImplementation, TImplementation>(factory);
        }

        public ServiceCollection AddSingleton<TImplementation>()
            where TImplementation : class, new()
        {
            return AddSingleton<TImplementation, TImplementation>();
        }

        public ServiceCollection AddSingleton<TAbstraction, TImplementation>()
            where TImplementation : class, new()
        {
            TImplementation singleton = null;
            Func<TImplementation> factory = () => singleton ?? (singleton = new TImplementation());
            return AddSingleton(factory);
        }

        public ServiceCollection AddSingleton<TAbstraction, TImplementation>(Func<TImplementation> factory)
            where TImplementation : class, new()
        {
            var abstractionType = typeof(TAbstraction);
            CheckIfRegistered(abstractionType);
            _register.Add(abstractionType, factory);
            return this;
        }

        public ServiceCollection AddTransient<TImplementation>()
            where TImplementation : class, new()
        {
            return AddTransient<TImplementation, TImplementation>();
        }

        public ServiceCollection AddTransient<TAbstraction, TImplementation>()
            where TImplementation : class, new()
        {
            var abstractionType = typeof(TAbstraction);
            CheckIfRegistered(abstractionType);
            Func<TImplementation> factory = () => new TImplementation();
            _register.Add(abstractionType, factory);
            return this;
        }

        public TService Get<TService>()
        {
            var type = typeof(TService);
            var factory = _register[type];
            var service = (TService)factory();
            return service;
        }

        public object Get(Type type)
        {
            var factory = _register[type];
            var service = factory();
            return service;
        }

        void CheckIfRegistered(Type type)
        {
            if (_register.ContainsKey(type))
            {
                throw new InvalidOperationException($"Type {type} is already registered.");
            }
        }
    }
}
