using _gitProject.logic.Components;
using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Enemies {
    public class Enemy : MonoBehaviour, IDamageable {

        [SerializeField] private int _enemyHealth = 10;
        private Health _health;
        
        public void Initialize(Health health) {
            _health = health;
        }
        
        private void Awake() {
            _health = new Health(_enemyHealth);
        }
        
        private void OnEnable() {
            _health.OnDied += DestroySelf;
        }
        private void OnDisable() {
            _health.OnDied -= DestroySelf;
        }
        public void TakeDamage(int amount) => _health.Reduce(amount);
        private void DestroySelf() => Destroy(gameObject);
    }
}
