using UnityEngine;

namespace _gitProject.logic.Components {
    public class MouseLook {
        
        #region fields

        private readonly float _rotateSpeed;
        private readonly Transform _transform;

        #endregion

        #region constructor

        public MouseLook(Transform transform, float rotateSpeed) {
            _transform = transform;
            _rotateSpeed = rotateSpeed;
        }

        #endregion

        #region public methods

        public void RotateToLookDirection(Vector3 direction) {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var rotateStep = _rotateSpeed * Time.deltaTime;
            _transform.rotation =
                Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0, angle, 0), rotateStep);
        }

        #endregion
    }
}
