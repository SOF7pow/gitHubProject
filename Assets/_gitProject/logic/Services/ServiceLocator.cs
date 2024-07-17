using System;
using System.Collections.Generic;
using UnityEngine;

namespace _gitProject.logic.Services {
    public class ServiceLocator {

        private readonly Dictionary<Type, IService> _services = new();
        private ServiceLocator() {}
        public static ServiceLocator Current { get; private set; }
        public static void Initialize() => Current = new ServiceLocator();

        #region public methods

        public T Get<T>() where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key)) 
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }
            return (T) _services[key];
        }

        public void Register<T>(T service) where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
                _services.Add(key, service);
            else
                Debug.LogError(
                    $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
        }

        public void UnRegister<T>() where T : IService {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
                Debug.LogError(
                    $"Attempted to unregister service of type {key} " +
                    $"which is not registered with the {GetType().Name}."
                );
            else
                _services.Remove(key);
        }

        #endregion
    }
}
