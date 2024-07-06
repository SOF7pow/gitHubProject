using System;
using _gitProject.logic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _gitProject.logic.Player {
    public class Shoot {
        
        private const float Distance = Mathf.Infinity;
        private float _lastAttackTime;
        private float _attackDelay;
        private readonly int _damage;
        private readonly Transform _muzzle;
        private readonly LayerMask _layer = LayerMask.NameToLayer("Damageable");
        private MeshRenderer _laser;

        public Action OnShoot;
        public Shoot(Transform muzzle,MeshRenderer laser , int damage, float delay) {
            _muzzle = muzzle;
            _laser = laser;
            _damage = damage;
            _attackDelay = delay;
        }
        
        public void ShootFire() {
            if (!CanShoot()) return;
            OnShoot?.Invoke();
            TryHit();
        }
        private void TryHit() {
            _laser.enabled = true;
            var direction = _muzzle.forward;
            var position = _muzzle.position;
            var ray = new Ray(position, direction);
            
            if (!Physics.Raycast(ray, out var hit, Distance, ~_layer)) return;
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) return;

            var multipleAttack = TryMultipleAttack(_damage);
            damageable.TakeDamage(multipleAttack);
        }
        public void ShootCoolDown() {
            if (_lastAttackTime > 0) _lastAttackTime -= Time.deltaTime;
            if (_lastAttackTime < _attackDelay * 0.5f)
                _laser.enabled = false;
        }
        private bool CanShoot() {
            if (!(_lastAttackTime <= 0)) return false;
            _lastAttackTime = _attackDelay;
            return true;
        }

        private int TryMultipleAttack(int value) => Random.Range(value, value * 5 + 1);
    }
}
