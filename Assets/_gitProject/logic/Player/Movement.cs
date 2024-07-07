using UnityEngine;

namespace _gitProject.logic.Player {
    public class Movement {

        private Vector3 _moveDirection;
        private readonly CharacterController _characterController;
        private const float Gravity = 15f;
        public float DashCoolDown { get; private set; } = 3f;
        public Movement(CharacterController characterController) {
            _characterController = characterController;
        }
        public void Move(Vector3 direction, float moveSpeed) {
            _moveDirection.x = direction.x * moveSpeed;
            _moveDirection.z = direction.z * moveSpeed;
            if (!_characterController.isGrounded) {
                _moveDirection.y -= Gravity * Time.deltaTime;
            }
            _characterController.Move(_moveDirection * Time.deltaTime);
            
            CoolDownDash();
        }
        public bool IsJumped(float force) {
            if (!_characterController.isGrounded) return false;
            _moveDirection.y = force;
            return true;
        }
        
        public bool Dashing() {
            if (!(DashCoolDown >= 3)) return false;
            var vector = _moveDirection;
            _characterController.Move(vector);
            DashCoolDown = 0;
            return true;
        }
        
        private void CoolDownDash() {
            if (DashCoolDown < 3) DashCoolDown += Time.deltaTime;
        }
    }
}
