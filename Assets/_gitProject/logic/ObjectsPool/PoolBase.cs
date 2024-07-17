using System;
using System.Collections.Generic;
using UnityEngine;

namespace _gitProject.logic.ObjectsPool {
    public class PoolBase<T> {
        
        #region fields

        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private Queue<T> _pool = new();
        private List<T> _active = new();
        private Transform _container;

        #endregion

        #region constructor

        protected PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount, Transform container) {
            if (preloadFunc == null) 
            {
                throw new ArgumentException($"preload function is null");
            }
            
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;
            _container = container;
            
            //preload
            for (var i = 0; i < preloadCount; i++)
                Return(preloadFunc());
        }

        #endregion

        #region piblic methods

        public T Get() {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(item);
            _active.Add(item);
            return item;
        }
        public void Return(T item) {
            _returnAction(item);
            _pool.Enqueue(item);
            _active.Remove(item);
        }
        public void ReturnAll() {
            foreach (var item in _active.ToArray()) 
                Return(item);
        }
        
        #endregion
    }
}
