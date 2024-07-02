using UnityEngine;

namespace _gitProject.logic.Player {
    public class MouseLook {
        private readonly float _rotateSpeed;
        private readonly Transform _transform;
        public MouseLook(Transform transform, float rotateSpeed) {
            _transform = transform;
            _rotateSpeed = rotateSpeed;
        }
        public void RotateToLookDirection(Vector3 direction) {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var rotateStep = _rotateSpeed * Time.deltaTime;
            _transform.rotation =
                Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, angle, 0), rotateStep);
        }
    }
}
