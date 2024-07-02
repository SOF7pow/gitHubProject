using UnityEngine;

namespace _gitProject.logic.Player {
    public class InputHandler {
        
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly Movement _movement;
        private readonly MouseLook _mouseLook;

        public InputHandler
        (
            Transform transform, 
            Camera camera, 
            Movement movement, 
            MouseLook mouseLook
        ) 
        {
            _transform = transform;
            _camera = camera;
            _movement = movement;
            _mouseLook = mouseLook;
        }
        
        public void ManualUpdate() {
            var moveDirection = CalculateMoveDirection();
            if (moveDirection != Vector3.zero)
                _movement.Move(moveDirection);

            var lookDirection = CalculateLookDirection();
            _mouseLook.RotateToAimDirection(lookDirection);
        }
        
        private Vector3 CalculateMoveDirection() {
            var horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
            var vertical = UnityEngine.Input.GetAxisRaw("Vertical");
            return new Vector3(horizontal, 0, vertical).normalized;
        }
        private Vector3 CalculateLookDirection() {
            var mousePosition =
               _camera.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x, 
                    Input.mousePosition.y, 
                    _camera.transform.position.y));
            var direction = mousePosition - _transform.position;
            return direction;
        }
    }
}
