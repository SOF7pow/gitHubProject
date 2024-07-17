using System;

namespace _gitProject.logic.Components {
    public class Health {

        #region fields

        private readonly int _startHealth;
        public Action<int> OnHealthChanged;
        public Action OnDied;
        public int GetHealth { get; private set; }

        #endregion

        #region constructor

        public Health (int health) {
            GetHealth = health;
            _startHealth = GetHealth;
        }

        #endregion

        #region public methods

        public void Reduce(int value) {
            if (value < 0) throw new ArgumentException(nameof(value));
            GetHealth -= value;
            OnHealthChanged?.Invoke(GetHealth);
            
            if (GetHealth > 0) return;
            
            GetHealth = 0;
            OnDied?.Invoke();
        }
        
        public void Regenerate(int value) {
            if (GetHealth + value > _startHealth) GetHealth = _startHealth;
            else GetHealth += value;
        }

        #endregion
    }
}
