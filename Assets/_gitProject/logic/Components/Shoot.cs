using _gitProject.logic.Components.Labels;
using _gitProject.logic.Events;
using _gitProject.logic.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _gitProject.logic.Components {
    public class Shoot {

        #region fields

        private const float Distance = Mathf.Infinity;
        private float _attackDelay;
        private float _lastAttackTime;
        private readonly int _damageMultiplierKoef;
        private readonly Transform _muzzle;
        private MeshRenderer _laserRenderer;
        private bool _canShoot = true;
        private readonly LayerMask _layer = LayerMask.NameToLayer("Damageable");

        #endregion

        #region properties
        
        public Vector3 HitPoint { get; private set; }
        
        #endregion

        #region constructor

        public Shoot(Transform muzzle, float delay, int damageMultiplierKoef) {
            _muzzle = muzzle;
            _attackDelay = delay;
            _damageMultiplierKoef = damageMultiplierKoef;

            _laserRenderer = _muzzle.GetComponentInChildren<LaserLabel>().GetComponent<MeshRenderer>();
            _laserRenderer.enabled = false;
        }

        #endregion

        #region public methods

        public bool IsShoot(int value) {
            if (!_canShoot) 
                return false;
            _laserRenderer.enabled = true;
            TryHit(value);
            return true;
        }
        public void Reload() {
            if (_lastAttackTime < _attackDelay) 
            {
                _lastAttackTime += Time.deltaTime;
                _canShoot = false;
                if (_lastAttackTime > _attackDelay * 0.4f) _laserRenderer.enabled = false;
            }
            else
                _canShoot = true;
        }

        #endregion

        #region private methods

        private void TryHit(int value) {
            _lastAttackTime = 0;
            var position = _muzzle.position;
            var direction = _muzzle.forward;
            var ray = new Ray(position, direction);
            if (!Physics.Raycast(ray, out var hit, Distance, ~_layer) || !hit.collider.TryGetComponent(out IDamageable damageable)) 
            {
                HitPoint = Vector3.zero;
                return;
            }
            if (IsMultipleAttack()) 
            {
                var criticalDamage = value * _damageMultiplierKoef;
                damageable.TakeDamage(criticalDamage);
                EventBus.Instance.OnCriticalShot?.Invoke();
            }
            else 
                damageable.TakeDamage(value);
            HitPoint = hit.point;
        }
        private bool IsMultipleAttack() => Random.Range(1, 100) > 85;

        #endregion
    }
}
