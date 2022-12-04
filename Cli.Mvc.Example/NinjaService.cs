using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cli.Mvc.Example
{
    public interface INinjaService
    {
        IReadOnlyList<Ninja> List();
        void Add(string name);
        void Remove(string name);
        void RemoveAll();
    }

    public class NinjaService : INinjaService
    {
        readonly List<Ninja> _ninjas = new List<Ninja>();

        public void Add(string name)
        {
            _ninjas.Add(new Ninja(name));
        }

        public IReadOnlyList<Ninja> List()
        {
            return _ninjas;
        }

        public void Remove(string name)
        {
            var ninja = _ninjas.FirstOrDefault(x => x.Name == name);
            
            if (ninja == null)
            {
                return;
            }

            _ninjas.Remove(ninja);
        }

        public void RemoveAll()
        {
            _ninjas.RemoveAll(_ => true);
        }
    }
}
