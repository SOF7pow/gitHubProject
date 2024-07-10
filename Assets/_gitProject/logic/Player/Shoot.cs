using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _gitProject.logic.Player {
    public class Shoot {
        
        private const float Distance = Mathf.Infinity;
        private float _attackDelay;
        private float _lastAttackTime;
        private readonly int _damageMultiplierKoef;
        private readonly Transform _muzzle;
        private MeshRenderer _laserRenderer;
        private readonly LayerMask _layer = LayerMask.NameToLayer("Damageable");
        private bool _canShoot = true;
        public Vector3 HitPoint { get; private set; }
        public Shoot(Transform muzzle, float delay, int damageMultiplierKoef) {
            _muzzle = muzzle;
            _attackDelay = delay;
            _damageMultiplierKoef = damageMultiplierKoef;

            _laserRenderer = _muzzle.GetComponentInChildren<LaserLabel>().GetComponent<MeshRenderer>();
            _laserRenderer.enabled = false;
        }
        public bool IsShoot(int value) {
            if (!_canShoot) return false;
            _laserRenderer.enabled = true;
            TryHit(value);
            return true;
        }
        private void TryHit(int value) {
            _lastAttackTime = 0;
            var position = _muzzle.position;
            var direction = _muzzle.forward;
            var ray = new Ray(position, direction);
            if (!Physics.Raycast(ray, out var hit, Distance, ~_layer) || !hit.collider.TryGetComponent(out IDamageable damageable)) {
                HitPoint = Vector3.zero;
                return;
            }
            if (IsMultipleAttack()) {
                var criticalDamage = value * _damageMultiplierKoef;
                damageable.TakeDamage(criticalDamage);
                EventsStorage.Instance.OnCriticalShot?.Invoke();
            }
            else damageable.TakeDamage(value);
            HitPoint = hit.point;
        }

        public void Reload() {
            if (_lastAttackTime < _attackDelay) {
                _lastAttackTime += Time.deltaTime;
                _canShoot = false;
                if (_lastAttackTime > _attackDelay * 0.2f) _laserRenderer.enabled = false;
            }
            else {
                _canShoot = true;
            }
        }
        private bool IsMultipleAttack() => Random.Range(1, 100) > 50;
    }
}
