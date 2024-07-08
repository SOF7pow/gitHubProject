using System;
using UnityEngine;

namespace _gitProject.logic.Player {
    public class Movement {

        private bool _isLand;
        private const float Gravity = 30f;
        private float _dashCoolDown = 3f;
        private readonly CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        
        public Action OnLanded;
        public Movement(CharacterController characterController) {
            _characterController = characterController;
        }
        public void Move(Vector3 direction, float moveSpeed) {
            CoolDownDash();
            _moveDirection.x = direction.x * moveSpeed;
            _moveDirection.z = direction.z * moveSpeed;
            
            _moveDirection.y -= Gravity * Time.deltaTime;
            _characterController.Move(_moveDirection * Time.deltaTime);
            
            if(IsLanded()) OnLanded?.Invoke();
        }
        public bool IsJumped(float force) {
            if (!_characterController.isGrounded) return false;
            _moveDirection.y = force;
            return true;
        }
        public bool IsDashed() {
            if (!(_dashCoolDown >= 3)) return false;
            var vector = _moveDirection;
            _characterController.Move(vector);
            _dashCoolDown = 0;
            return true;
        }

        private bool IsLanded() {
            if (_characterController.isGrounded) {
                if (_isLand) return false;
                _isLand = true;
                return true;
            }
            _isLand = false;
            return false;
        }
        
        private void CoolDownDash() {
            if (_dashCoolDown < 3) 
                _dashCoolDown += Time.deltaTime;
        }
    }
}
