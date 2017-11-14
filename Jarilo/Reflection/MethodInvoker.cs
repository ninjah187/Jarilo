using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Jarilo.Reflection
{
    class MethodInvoker
    {
        public void Invoke(Action invoke)
        {
            try
            {
                invoke();
            }
            catch (TargetInvocationException exception)
            {
                Debug.WriteLine(exception.ToString());
                throw exception.InnerException ?? exception;
            }
        }

        public TResult Invoke<TResult>(Func<TResult> invoke)
        {
            try
            {
                return invoke();
            }
            catch (TargetInvocationException exception)
            {
                Debug.WriteLine(exception.ToString());
                throw exception.InnerException ?? exception;
            }
        }
    }
}
