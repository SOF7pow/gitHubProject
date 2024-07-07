using UnityEngine;

namespace _gitProject.logic.Player {
    public class InputHandler {
        private readonly Camera _camera;
        private readonly Transform _transform;
        private const float Gravity = 9.81f;
        public InputHandler(Transform transform, Camera camera) {
            _transform = transform;
            _camera = camera;
        }
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

        public bool IsJump() {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsShoot() {
            return Input.GetMouseButton(0);
        }
        
        public bool IsDash() {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
}
