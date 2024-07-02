using _gitProject.logic.ServiceLocator;
using UnityEngine;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService {

        private Movement _movement;
        private MouseLook _mouseLook;
        private InputHandler _inputHandler;
        private Shoot _shoot;

        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 8f;
        
        public void Init() {
            var controller = GetComponent<CharacterController>();
            
            _mouseLook = new MouseLook(transform, _rotateSpeed);
            _movement = new Movement(controller, _moveSpeed);
            _inputHandler = new InputHandler(transform, Camera.main);
            _shoot = new Shoot(_muzzle,1,0.25f);
        }

        private void Update() {
            _movement.Move(_inputHandler.CalculateMoveDirection());
            _mouseLook.RotateToLookDirection(_inputHandler.CalculateLookDirection());
            if (_inputHandler.IsJump()) _movement.Jump(_jumpForce);
            
            if (_inputHandler.IsShoot()) _shoot.Attack();
            _shoot.AttackCoolDown();
        }
    }
}
