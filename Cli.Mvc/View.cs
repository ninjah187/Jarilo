using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public abstract class View : IView
    {
        [Service] protected IRenderer Renderer { get; private set; }

        public abstract void Render();
    }
}
