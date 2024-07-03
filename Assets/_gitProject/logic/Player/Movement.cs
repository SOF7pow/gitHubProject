using UnityEngine;

namespace _gitProject.logic.Player {
    public class Movement {

        private Vector3 _moveDirection;
        private float _moveSpeed;
        private readonly CharacterController _characterController;
        private const float Gravity = 15f;
        
        
        public Movement(CharacterController characterController, float moveSpeed) {
            _characterController = characterController;
            _moveSpeed = moveSpeed;
        }
        public void Move(Vector3 direction) {
            _moveDirection.x = direction.x * _moveSpeed;
            _moveDirection.z = direction.z * _moveSpeed;
            _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move(_moveDirection * Time.deltaTime);
        }
        public void Jump(float force) {
            if (_characterController.isGrounded)
                _moveDirection.y = force;
        }
    }
}
