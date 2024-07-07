using System;
using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _gitProject.logic.Player {
    public class Shoot {
        
        private const float Distance = Mathf.Infinity;
        private float _lastAttackTime;
        private float _attackDelay;
        private readonly int _damageMultiplierKoef;
        private readonly Transform _muzzle;
        private readonly LayerMask _layer = LayerMask.NameToLayer("Damageable");
        private MeshRenderer _laserMeshRenderer;
        public Vector3 HitPoint { get; private set; }

        public Shoot(Transform muzzle, LaserLabel laser, float delay, int damageMultiplierKoef) {
            _muzzle = muzzle;
            _laserMeshRenderer = laser.GetComponent<MeshRenderer>();
            _attackDelay = delay;
            _damageMultiplierKoef = damageMultiplierKoef;
        }
        
        public bool IsShooted(int value) {
            if (!CanShoot()) return false;
            TryHit(value);
            return true;
        }
        
        private void TryHit(int value) {
            _laserMeshRenderer.enabled = true;
            var position = _muzzle.position;
            var direction = _muzzle.forward;
            var ray = new Ray(position, direction);

            if (!Physics.Raycast(ray, out var hit, Distance, ~_layer) ||
                !hit.collider.TryGetComponent(out IDamageable damageable)) {
                HitPoint = Vector3.zero;
                return;
            }
            if (IsMultipleAttack()) {
                EventBus.Instance.OnCriticalShot?.Invoke();
                var criticalDamage = value * _damageMultiplierKoef;
                damageable.TakeDamage(criticalDamage);
            }
            else damageable.TakeDamage(value);
            HitPoint = hit.point;
        }
        public void ShootCoolDown() {
            if (_lastAttackTime > 0) _lastAttackTime -= Time.deltaTime;
            if (_lastAttackTime < _attackDelay * 0.5f)
                _laserMeshRenderer.enabled = false;
        }
        private bool CanShoot() {
            if (!(_lastAttackTime <= 0)) return false;
            _lastAttackTime = _attackDelay;
            return true;
        }
        public bool IsMultipleAttack() => Random.Range(1, 100) > 80;
    }
}
