using System;
using System.Collections.Generic;
using System.Text;

namespace Cli.Mvc
{
    public class Documentation
    {
        readonly IRouter _router;

        public Documentation(IRouter router)
        {
            _router = router;
        }

        //public string GetDescription(string path)
        //{
        //    foreach (var route in _router.Routes)
        //    {
        //        if (route.Path == path)
        //        {
        //            // action
        //        }
        //        if (route.ControllerPath == path)
        //        {
        //            // controller
        //        }
        //    }
        //}

        //string GetActionDescription(string path, Route route)
        //{
            
        //}
    }
}
