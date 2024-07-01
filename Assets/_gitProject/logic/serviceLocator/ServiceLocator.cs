using System;
using System.Collections.Generic;
using UnityEngine;

namespace _gitProject.logic.ServiceLocator {
    public class ServiceLocator {
        private ServiceLocator() {}

        private readonly Dictionary<Type, IService> _services = new();
        public static ServiceLocator Current { get; private set; }

        public static void Initialize() => Current = new ServiceLocator();

        public T Get<T>() where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key)) {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T) _services[key];
        }

        public void Register<T>(T service) where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key)) {
                _services.Add(key, service);
                Debug.Log($"{key} is registered");
            }
            else {
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
            }
        }

        public void UnRegister<T>() where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
            else
                _services.Remove(key);
        }
    }
}
