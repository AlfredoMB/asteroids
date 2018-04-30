using System;
using System.Collections.Generic;

namespace AlfredoMB.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static void Register<T>(T instance)
        {
            _instances.Add(typeof(T), instance);
        }

        public static T Get<T>()
        {
            return (T)_instances[typeof(T)];
        }

        public static void Reset()
        {
            _instances.Clear();
        }
    }
}