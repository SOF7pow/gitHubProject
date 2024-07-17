using UnityEngine;

namespace _gitProject.logic.Components {
    public class InputHandler {

        #region fields

        private readonly Camera _camera;
        private readonly Transform _transform;

        #endregion

        #region constructor
        public InputHandler(Transform transform, Camera camera) {
            _transform = transform;
            _camera = camera;
        }

        #endregion

        #region public methods

        public Vector3 CalculateMoveDirection() {
            var right = Input.GetAxisRaw("Horizontal");
            var forward = Input.GetAxisRaw("Vertical");
            return new Vector3(right, 0, forward).normalized;
        }
        public Vector3 CalculateLookDirection() {
            var mousePosition =
                _camera.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    _camera.transform.position.y));
            var direction = mousePosition - _transform.position;
            return direction;
        }
        public bool IsJump() => Input.GetKeyDown(KeyCode.Space);
        public bool IsShoot() => Input.GetMouseButton(0);
        public bool IsDash() => Input.GetKeyDown(KeyCode.LeftShift);

        #endregion
    }
}
