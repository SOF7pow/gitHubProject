using System;

namespace _gitProject.logic.Components {
    public sealed class Health {

        public Action OnHealthTriggered;
        public Action<int> OnHealthChanged;
        public Action OnDied;
        public int GetHealth { get; private set; }
        private readonly int _startHealth;

        public Health (int health) {
            GetHealth = health;
            _startHealth = GetHealth;
        }
        public void Reduce(int value) {
            if (value < 0) throw new ArgumentException(nameof(value));
            
            OnHealthTriggered?.Invoke();
            GetHealth -= value;
            OnHealthChanged?.Invoke(GetHealth);
            
            if (GetHealth > 0) return;
            OnDied?.Invoke();
            GetHealth = 0;
        }
        public void Regenerate(int value) {
            if (GetHealth + value > _startHealth) GetHealth = _startHealth;
            else GetHealth += value;
        }
    }
}
