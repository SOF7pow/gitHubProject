using _gitProject.logic.Components.Labels;
using _gitProject.logic.Services;
using UnityEngine;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService {

        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private Shoot _shoot;

        public MeshRenderer Laser { get; private set; }
        public CharacterController Controller { get; private set; }
        public Transform Muzzle { get; private set; }

        public float JumpForce => 12f;
        public float RotateSpeed => 10f;
        public float MoveSpeed => 5f;

        public void Initialize() {
            Muzzle = GetComponentInChildren<MuzzleComponent>().transform;
            Controller = GetComponent<CharacterController>();
            Laser = GetComponentInChildren<LaserComponet>().gameObject.GetComponent<MeshRenderer>();
            Laser.enabled = false;
            
            _mouseLook = new MouseLook(transform, RotateSpeed);
            _movement = new Movement(Controller, MoveSpeed);
            _inputHandler = new InputHandler(transform, Camera.main);
            _shoot = new Shoot(Muzzle, Laser, 1, 0.25f);
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
