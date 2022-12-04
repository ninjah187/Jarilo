using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cli.Mvc
{
    public abstract class Docs
    {
        public void Describe<T>(string description)
        {
        }

        public void Describe<T>(string description, Expression<Action<T>> descriptor)
        {
        }

        public T Description<T>(params Expression<Func<string>>[] description)
        {
            return default;
        }
    }
}
