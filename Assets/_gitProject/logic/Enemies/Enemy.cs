using _gitProject.logic.Components;
using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Enemies {
    public class Enemy : MonoBehaviour, IDamageable {

        private Health _health;
        private int _enemyHealth = 10;
        private void Awake() {
            _health = new Health(_enemyHealth);
        }
        public void TakeDamage(int amount) => _health.Reduce(amount);
    }
}
