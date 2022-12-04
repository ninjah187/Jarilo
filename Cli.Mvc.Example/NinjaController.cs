using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cli.Mvc.Example  
{
    public class NinjaController : Controller
    {
        readonly INinjaService _service;

        public NinjaController(INinjaService service)
        {
            _service = service;
        }

        public IActionResult List()
        {
            var names = _service
                .List()
                .Select(x => x.Name)
                .ToArray();

            return new NinjaListView(names);
        }

        public IActionResult Add(string name, [Option] int hp)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Help();
            }

            _service.Add(name);

            return Ok($"Added ninja {name} (HP: {Options["hp"]}).");
        }

        public IActionResult Remove(string name, [Option] bool all = false)
        {
            if (all)
            {
                _service.RemoveAll();

                return Ok($"Removed all ninjas.");
            }

            if (string.IsNullOrEmpty(name))
            {
                return Help();
            }

            _service.Remove(name);

            return Ok($"Removed ninja {name}.");
        }
    }
}
