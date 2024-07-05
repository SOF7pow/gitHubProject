using System;
using UnityEngine;

namespace _gitProject.logic.Components {
    public sealed class Health {

        public Action OnHealthTriggered;
        public Action<int> OnHealthChanged;
        public Action OnDied;

        public int GetHealth { get; private set; }

        public Health (int health) => GetHealth = health;

        public void Reduce(int value) {
            Debug.Log($"Take damage");
            if (value < 0) 
                throw new ArgumentException(nameof(value));
            GetHealth -= value;
            OnHealthChanged?.Invoke(GetHealth);
            OnHealthTriggered?.Invoke();
            if (GetHealth <= 0) 
                OnDied?.Invoke();
        }
    }
}
