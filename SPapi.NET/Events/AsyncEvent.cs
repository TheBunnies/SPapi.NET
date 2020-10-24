using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPapi.NET.Events
{
    public class AsyncEvent<TEventArgs> where TEventArgs : EventArgs
    {
        private readonly List<Func<object, TEventArgs, Task>> invocationList;
        private readonly object locker;

        private AsyncEvent()
        {
            invocationList = new List<Func<object, TEventArgs, Task>>();
            locker = new object();
        }

        public static AsyncEvent<TEventArgs> operator +(
            AsyncEvent<TEventArgs> e, Func<object, TEventArgs, Task> callback)
        {
            if (callback == null) throw new NullReferenceException("callback is null");

            if (e == null) e = new AsyncEvent<TEventArgs>();

            lock (e.locker)
            {
                e.invocationList.Add(callback);
            }

            return e;
        }

        public static AsyncEvent<TEventArgs> operator -(
            AsyncEvent<TEventArgs> e, Func<object, TEventArgs, Task> callback)
        {
            if (callback == null) throw new NullReferenceException("callback is null");
            if (e == null) return null;

            lock (e.locker)
            {
                e.invocationList.Remove(callback);
            }

            return e;
        }

        public async Task InvokeAsync(object sender, TEventArgs eventArgs)
        {
            List<Func<object, TEventArgs, Task>> tmpInvocationList;
            lock (locker)
            {
                tmpInvocationList = new List<Func<object, TEventArgs, Task>>(invocationList);
            }

            foreach (var callback in tmpInvocationList)
            {
                await callback(sender, eventArgs);
            }
        }
    }
}
