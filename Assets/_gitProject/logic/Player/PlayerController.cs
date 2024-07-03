using _gitProject.logic.Components;
using _gitProject.logic.Services;
using UnityEngine;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService {

        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private Shoot _shoot;

        public CharacterController Controller { get; private set; }
        public Transform Muzzle { get; private set; }
        
        public float JumpForce { get; } = 8f;
        public float RotateSpeed { get; } = 5f;
        public float MoveSpeed { get; } = 5f;

        private void Awake() {
            Muzzle = GetComponentInChildren<MuzzleComponent>().transform;
            Controller = GetComponent<CharacterController>();
        }

        public void Initialize
        (
            MouseLook mouseLook, 
            Movement movement, 
            InputHandler inputHandler, 
            Shoot shoot
        ) 
        {
            _mouseLook = mouseLook;
            _movement = movement;
            _inputHandler = inputHandler;
            _shoot = shoot;
        }

        private void Update() {
            var moveDirection = _inputHandler.CalculateMoveDirection();
            _movement.Move(moveDirection);
            var lookDirection = _inputHandler.CalculateLookDirection();
            _mouseLook.RotateToLookDirection(lookDirection);
            
            if (_inputHandler.IsJump()) _movement.Jump(JumpForce);
            if (_inputHandler.IsShoot()) _shoot.Attack();
            
            _shoot.AttackCoolDown();
        }
    }
}
