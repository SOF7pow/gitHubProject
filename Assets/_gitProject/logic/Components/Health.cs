using System;
using UnityEngine;

namespace _gitProject.logic.Components {
    public class Health{
        
        public Action<int> OnHealthChanged;
        public Action OnDied;
        
        private int _value;
        public Health (int value) => _value = value;

        public virtual void Reduce(int value) {
            Debug.Log($"Take damage");
            if (value < 0) 
                throw new ArgumentException(nameof(value));
            _value -= value;
            OnHealthChanged?.Invoke(_value);
            if (_value <= 0) 
                OnDied?.Invoke();
        }
    }
}
