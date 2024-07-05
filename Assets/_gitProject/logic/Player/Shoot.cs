using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Player {
    public class Shoot {
        
        private const float Distance = Mathf.Infinity;
        private float _lastAttackTime;
        private float _attackDelay;
        private readonly int _damage;
        private readonly Transform _muzzle;
        private readonly LayerMask _layer = LayerMask.NameToLayer("Damageable");
        private MeshRenderer _laser;
        
        public Shoot(Transform muzzle,MeshRenderer laser , int damage, float delay) {
            _muzzle = muzzle;
            _laser = laser;
            _damage = damage;
            _attackDelay = delay;
        }
        public void Attack() {
            AttackCoolDown();
            if (CanAttack()) TryAttack();
        }
        private void TryAttack() {
            _laser.enabled = true;
            var direction = _muzzle.forward;
            var position = _muzzle.position;
            var ray = new Ray(position, direction);
            if (!Physics.Raycast(ray, out var hit, Distance, ~_layer)) return;
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) return;
            
            Debug.Log(hit.collider.name);
            damageable.TakeDamage(_damage);
        }
        public void AttackCoolDown() {
            if (_lastAttackTime > 0) _lastAttackTime -= Time.deltaTime;
            if (_lastAttackTime < _attackDelay * 0.75f)
                _laser.enabled = false;
        }
        private bool CanAttack() {
            if (!(_lastAttackTime <= 0)) return false;
            _lastAttackTime = _attackDelay;
            return true;
        }
    }
}
