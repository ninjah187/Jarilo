using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class MessageView : View
    {
        readonly string _message;

        public MessageView(string message)
        {
            _message = message;
        }

        public override void Render()
        {
            Renderer.WriteLine(_message);
        }
    }
}
