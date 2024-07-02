using _gitProject.logic.Interfaces;
using UnityEngine;

namespace _gitProject.logic.Player {
    public class ShootController : MonoBehaviour {
        
        private float _distance = Mathf.Infinity;
        private float _lastAttackTime;
        
        [SerializeField] private Transform _muzzle;
        [SerializeField, Range(0,2), Min(0)] private float attackDelay = 0.5f;
        [SerializeField, Min(0)] private int _damage = 1;
        [SerializeField] private LayerMask _layerMask;

        private void Update() {
            AttackCoolDown();
            if (Input.GetMouseButton(0)) {
                Attack();
            }
        }
        
        private void Attack() {
            if (!CanAttack()) return;
            TryAttack();
            print("Attack");
        }
        private void TryAttack() {
            var direction = _muzzle.forward;
            var position = _muzzle.position;
            var ray = new Ray(position, direction);
            if (!Physics.Raycast(ray, out var hit, _distance, _layerMask)) return;
            if (!hit.collider.TryGetComponent(out IDamageable damageable)) return;
            damageable.TakeDamage(_damage);
        }
        private void AttackCoolDown() {
            if (_lastAttackTime > 0) _lastAttackTime -= Time.deltaTime;
        }
        private bool CanAttack() {
            if (!(_lastAttackTime <= 0)) return false;
            _lastAttackTime = attackDelay;
            return true;
        }
    }
}
