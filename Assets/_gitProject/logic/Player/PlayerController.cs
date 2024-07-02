using _gitProject.logic.ServiceLocator;
using UnityEngine;

namespace _gitProject.logic.Player {
    
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, IService {

        private MouseLook _mouseLook;
        private Movement _movement;
        private ShootController _shootController;
        private InputHandler _inputHandler;
        
        [SerializeField] private float _rotateSpeed = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        
        public void Init() {
            var controller = GetComponent<CharacterController>();
            
            
            _mouseLook = new MouseLook(transform, _rotateSpeed);
            _movement = new Movement(controller, _moveSpeed);
            _inputHandler = new InputHandler(transform, Camera.main, _movement, _mouseLook);
        }

        private void Update() {
            _inputHandler.ManualUpdate();
        }
    }
}
