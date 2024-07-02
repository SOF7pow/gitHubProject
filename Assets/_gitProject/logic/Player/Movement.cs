using UnityEngine;

namespace _gitProject.logic.Player {
    public class Movement {

        private readonly CharacterController _characterController;
        private readonly float _moveSpeed;
        public Movement(CharacterController characterController, float moveSpeed) {
            _characterController = characterController;
            _moveSpeed = moveSpeed;
        }
        public void Move(Vector3 direction) {
            _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
        }
    }
}
